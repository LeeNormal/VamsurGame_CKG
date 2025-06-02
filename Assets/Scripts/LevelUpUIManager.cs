using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelUpUIManager : MonoBehaviour
{
    public GameObject levelUpUI;
    public Button optionButton1;
    public Button optionButton2;
    public TMP_Text optionText1;
    public TMP_Text optionText2;

    private LevelUpOption option1;
    private LevelUpOption option2;

    public void Start()
    {
        levelUpUI.SetActive(false);
    }

    public void OpenLevelUpUI()
    {
        levelUpUI.SetActive(true);
        Time.timeScale = 0f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            player.GetComponent<PlayerController>().canMove = false;

        GenerateOptions();
    }

    public void CloseLevelUpUI()
    {
        levelUpUI.SetActive(false);
        Time.timeScale = 1f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            player.GetComponent<PlayerController>().canMove = true;
    }

    private void GenerateOptions()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        WeaponManager weaponManager = player.GetComponentInChildren<WeaponManager>();

        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager를 찾을 수 없습니다!");
            return;
        }

        if (weaponManager.CanAddWeapon() && weaponManager.HasAvailableWeapons())
        {
            GameObject weaponPrefabA = weaponManager.GetRandomAvailableWeapon();
            GameObject weaponPrefabB = null;

            bool onlyOneChoice = !weaponManager.HasAvailableWeapons();

            if (!onlyOneChoice)
                weaponPrefabB = weaponManager.GetRandomAvailableWeapon();

            option1 = new LevelUpOption(weaponPrefabA);
            optionText1.text = $"새 무기: {weaponPrefabA.name}";
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() => ChooseNewWeapon(option1));
            optionButton1.gameObject.SetActive(true);

            if (!onlyOneChoice && weaponPrefabB != null)
            {
                option2 = new LevelUpOption(weaponPrefabB);
                optionText2.text = $"새 무기: {weaponPrefabB.name}";
                optionButton2.onClick.RemoveAllListeners();
                optionButton2.onClick.AddListener(() => ChooseNewWeapon(option2));
                optionButton2.gameObject.SetActive(true);
                ResetButtonPositions();
            }
            else
            {
                optionButton2.gameObject.SetActive(false);
                CenterButton(optionButton1);
            }
        }
        else
        {
            List<WeaponBase> weaponList = weaponManager.equippedWeapons;
            if (weaponList.Count == 0)
            {
                Debug.LogWarning("장착한 무기가 없습니다!");
                return;
            }

            WeaponBase weaponA = weaponList[Random.Range(0, weaponList.Count)];
            UpgradeType upgradeA = GetRandomUpgradeType();
            option1 = new LevelUpOption(weaponA, upgradeA);

            WeaponBase weaponB;
            UpgradeType upgradeB;
            int tryCount = 0;
            do
            {
                weaponB = weaponList[Random.Range(0, weaponList.Count)];
                upgradeB = GetRandomUpgradeType();
                tryCount++;
            } while (weaponB == weaponA && upgradeB == upgradeA && tryCount < 30);

            option2 = new LevelUpOption(weaponB, upgradeB);

            optionText1.text = GetUpgradeText(weaponA, upgradeA);
            optionText2.text = GetUpgradeText(weaponB, upgradeB);

            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() => ChooseUpgrade(option1));
            optionButton2.onClick.RemoveAllListeners();
            optionButton2.onClick.AddListener(() => ChooseUpgrade(option2));

            optionButton1.gameObject.SetActive(true);
            optionButton2.gameObject.SetActive(true);
            ResetButtonPositions();
        }
    }

    private void ChooseNewWeapon(LevelUpOption selectedOption)
    {
        if (selectedOption == null) return;

        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        WeaponManager weaponManager = player.GetComponentInChildren<WeaponManager>();

        weaponManager?.AddWeapon(selectedOption.weaponPrefab);
        CloseLevelUpUI();
    }

    private void ChooseUpgrade(LevelUpOption selectedOption)
    {
        if (selectedOption == null) return;

        WeaponBase weapon = selectedOption.weapon;

        switch (selectedOption.upgradeType)
        {
            case UpgradeType.Damage:
                weapon.UpgradeDamage();
                break;
            case UpgradeType.Speed:
                weapon.UpgradeSpeed();
                break;
            case UpgradeType.Count:
                if (weapon is OrbitSwordWeapon orbitSword)
                    orbitSword.UpgradeCount();
                break;
        }

        CloseLevelUpUI();
    }

    private UpgradeType GetRandomUpgradeType()
    {
        float rand = Random.value;
        if (rand < 0.33f) return UpgradeType.Count;
        else if (rand < 0.66f) return UpgradeType.Count;
        else return UpgradeType.Count;
    }

    private string GetUpgradeText(WeaponBase weapon, UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Damage => $"{weapon.weaponName} 데미지 업",
            UpgradeType.Speed => $"{weapon.weaponName} 속도 업",
            UpgradeType.Count => $"{weapon.weaponName} 칼 개수 업",
            _ => "???"
        };
    }

    private void CenterButton(Button button)
    {
        RectTransform rect = button.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
    }

    private void ResetButtonPositions()
    {
        RectTransform rect1 = optionButton1.GetComponent<RectTransform>();
        RectTransform rect2 = optionButton2.GetComponent<RectTransform>();
        rect1.anchoredPosition = new Vector2(-200, 0);
        rect2.anchoredPosition = new Vector2(200, 0);
    }
}

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

        option1 = GenerateRandomOption(weaponManager);
        option2 = GenerateRandomOption(weaponManager, option1); // 중복 피하기 위한 비교 포함

        SetupOptionUI(optionButton1, optionText1, option1);
        SetupOptionUI(optionButton2, optionText2, option2);

        ResetButtonPositions();
    }

    private LevelUpOption GenerateRandomOption(WeaponManager weaponManager, LevelUpOption avoidDuplicate = null)
    {
        bool canAdd = weaponManager.CanAddWeapon();
        bool hasAvailable = weaponManager.HasAvailableWeapons();
        bool tryNewWeapon = canAdd && hasAvailable && Random.value < 0.5f; // 50% 확률로 무기 or 업그레이드

        if (tryNewWeapon)
        {
            GameObject prefab = weaponManager.GetRandomAvailableWeapon();
            if (avoidDuplicate != null && avoidDuplicate.weaponPrefab == prefab)
                prefab = weaponManager.GetRandomAvailableWeapon(); // 중복 피하기
            return new LevelUpOption(prefab);
        }
        else
        {
            List<WeaponBase> equipped = weaponManager.equippedWeapons;
            if (equipped.Count == 0)
            {
                // fallback: 무조건 무기 추가
                GameObject fallbackWeapon = weaponManager.GetRandomAvailableWeapon();
                return new LevelUpOption(fallbackWeapon);
            }

            WeaponBase weapon = equipped[Random.Range(0, equipped.Count)];
            UpgradeType upgrade = GetRandomUpgradeType(weapon);

            if (avoidDuplicate != null && avoidDuplicate.weapon == weapon && avoidDuplicate.upgradeType == upgrade)
            {
                weapon = equipped[Random.Range(0, equipped.Count)];
                upgrade = GetRandomUpgradeType(weapon);
            }

            return new LevelUpOption(weapon, upgrade);
        }
    }

    private void SetupOptionUI(Button button, TMP_Text text, LevelUpOption option)
    {
        button.onClick.RemoveAllListeners();
        button.gameObject.SetActive(true);

        if (option.isNewWeapon)
        {
            text.text = $"새 무기: {option.weaponDisplayName}";
            button.onClick.AddListener(() => ChooseNewWeapon(option));
        }
        else
        {
            text.text = option.weapon.GetUpgradeText(option.upgradeType);
            button.onClick.AddListener(() => ChooseUpgrade(option));
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
                weapon.UpgradeCount();
                break;
        }

        CloseLevelUpUI();
    }

    private UpgradeType GetRandomUpgradeType(WeaponBase weapon)
    {
        List<UpgradeType> validTypes = new();

        validTypes.Add(UpgradeType.Damage);
        validTypes.Add(UpgradeType.Speed);

        if (weapon.CanUpgradeCount())
            validTypes.Add(UpgradeType.Count);

        if (validTypes.Count == 0)
            return UpgradeType.Damage;

        return validTypes[Random.Range(0, validTypes.Count)];
    }

    private void ResetButtonPositions()
    {
        RectTransform rect1 = optionButton1.GetComponent<RectTransform>();
        RectTransform rect2 = optionButton2.GetComponent<RectTransform>();
        rect1.anchoredPosition = new Vector2(0, 200);
        rect2.anchoredPosition = new Vector2(0, -200);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class OrbitSwordWeapon : WeaponBase
{
    [Header("Orbit Weapon Settings")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float radius = 2f;

    private List<GameObject> swords = new();
    private int swordCount = 1;
    private int countUpgradeLevel = 0;

    public override void Initialize()
    {
        UpdateSwords();
    }

    public override void Attack() { /* 회전형 무기라 Attack 생략 가능 */ }

    private void UpdateSwords()
    {
        // 기존 칼 제거
        foreach (var sword in swords)
        {
            if (sword != null) Destroy(sword);
        }
        swords.Clear();

        for (int i = 0; i < swordCount; i++)
        {
            float angle = 360f * i / swordCount;
            GameObject sword = Instantiate(swordPrefab, transform.position, Quaternion.identity);

            sword.transform.SetParent(this.transform);

            OrbitingSword swordScript = sword.GetComponent<OrbitingSword>();
            swordScript.Initialize(rotateSpeed, damage, radius, angle);
            swords.Add(sword);
        }
    }
    public override string GetUpgradeText(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Damage => $"{weaponName} 데미지 업!!",
            UpgradeType.Speed => $"{weaponName} 회전 속도 증가!!",
            UpgradeType.Count => $"{weaponName} 개수 증가!!",
            _ => "???"
        };
    }

    public override void UpgradeDamage()
    {
        damage += 5f;
        level++;

        foreach (var sword in swords)
            sword.GetComponent<OrbitingSword>().SetDamage(damage);
    }

    public override void UpgradeSpeed()
    {
        rotateSpeed += 30f;
        level++;

        foreach (var sword in swords)
            sword.GetComponent<OrbitingSword>().SetSpeed(rotateSpeed);
    }

    public override void UpgradeCount()
    {
        if (!CanUpgradeCount()) return;

        swordCount++;
        countUpgradeLevel++;
        level++;

        UpdateSwords();
    }

    public override bool CanUpgradeCount()
    {
        return countUpgradeLevel < 4;
    }
}

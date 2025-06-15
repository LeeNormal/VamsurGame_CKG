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
        weaponName = "회전하는 칼";
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

    public override void UpgradeDamage()
    {
        damage += 10f;
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

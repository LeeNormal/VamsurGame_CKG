using UnityEngine;
using System.Collections.Generic;

public class OrbitSwordWeapon : WeaponBase
{
    [Header("Orbit Weapon Settings")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float radius = 2f;

    private List<GameObject> swords = new List<GameObject>();
    private int swordCount = 1;

    public override void Initialize()
    {
        weaponName = "OrbitSword";
        UpdateSwords();
    }

    public override void Attack() { /* Sword가 자체적으로 회전하므로 생략 */ }

    private void UpdateSwords()
    {
        foreach (var sword in swords)
        {
            if (sword != null) Destroy(sword);
        }
        swords.Clear();

        for (int i = 0; i < swordCount; i++)
        {
            float angle = 360f * i / swordCount; // 시작 위치만 다르게, 도는 방향은 동일
            GameObject sword = Instantiate(swordPrefab, transform.position, Quaternion.identity);
            sword.transform.SetParent(null);
            OrbitingSword swordScript = sword.GetComponent<OrbitingSword>();
            swordScript.Initialize(transform, rotateSpeed, damage, radius, angle);
            swords.Add(sword);
        }
    }

    public override void UpgradeDamage()
    {
        damage += 10f;
        level++;
        foreach (var sword in swords)
        {
            sword.GetComponent<OrbitingSword>().SetDamage(damage);
        }
        Debug.Log($"{weaponName} 데미지 업! 현재: {damage}");
    }

    public override void UpgradeSpeed()
    {
        rotateSpeed += 30f;
        level++;
        foreach (var sword in swords)
        {
            sword.GetComponent<OrbitingSword>().SetSpeed(rotateSpeed);
        }
        Debug.Log($"{weaponName} 속도 업! 현재: {rotateSpeed}");
    }

    public void UpgradeCount()
    {
        swordCount++;
        level++;
        UpdateSwords();
        Debug.Log($"{weaponName} 개수 업! 현재: {swordCount}개");
    }
}

using UnityEngine;

public class LevelUpOption
{
    public WeaponBase weapon;
    public UpgradeType upgradeType;
    public GameObject weaponPrefab;

    // 새 무기 추가용
    public LevelUpOption(GameObject prefab)
    {
        weaponPrefab = prefab;
    }

    // 기존 무기 업그레이드용
    public LevelUpOption(WeaponBase weapon, UpgradeType upgradeType)
    {
        this.weapon = weapon;
        this.upgradeType = upgradeType;
    }
}


public enum UpgradeType
{
    Damage,
    Speed,
    Count
}

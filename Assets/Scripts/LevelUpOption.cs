using UnityEngine;

public class LevelUpOption
{
    public bool isNewWeapon;
    public GameObject weaponPrefab;
    public WeaponBase weapon;
    public UpgradeType upgradeType;

    public string weaponDisplayName;

    public LevelUpOption(GameObject prefab)
    {
        isNewWeapon = true;
        weaponPrefab = prefab;

        WeaponBase wb = prefab.GetComponentInChildren<WeaponBase>();
        weaponDisplayName = wb != null ? wb.weaponName : prefab.name;
    }

    public LevelUpOption(WeaponBase weapon, UpgradeType upgrade)
    {
        isNewWeapon = false;
        this.weapon = weapon;
        upgradeType = upgrade;
        weaponDisplayName = weapon.weaponName;
    }
}

public enum UpgradeType
{
    Damage,
    Speed,
    Count
}

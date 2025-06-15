using UnityEngine;

// 무기 업그레이드 또는 신규 무기 선택 정보를 담는 클래스
public class LevelUpOption
{
    public WeaponBase weapon;               // 기존 무기 (업그레이드용)
    public UpgradeType upgradeType;         // 업그레이드 종류
    public GameObject weaponPrefab;         // 신규 무기 프리팹

    public bool isNewWeapon { get; private set; }

    // 생성자: 새 무기 선택용
    public LevelUpOption(GameObject prefab)
    {
        weaponPrefab = prefab;
        isNewWeapon = true;
    }

    // 생성자: 기존 무기 업그레이드 선택용
    public LevelUpOption(WeaponBase weapon, UpgradeType upgradeType)
    {
        this.weapon = weapon;
        this.upgradeType = upgradeType;
        isNewWeapon = false;
    }
}

// 업그레이드 종류 정의
public enum UpgradeType
{
    Damage,
    Speed,
    Count
}

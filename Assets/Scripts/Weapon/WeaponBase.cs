using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("공통 무기 정보")]
    public string weaponName;
    public int level = 1;
    public float damage = 10f;
    public float attackInterval = 1f;

    protected float lastAttackTime = 0f;

    // 무기 초기화 및 공격 실행
    public abstract void Initialize();
    public abstract void Attack();

    // --- 업그레이드 기능 ---
    public virtual void UpgradeDamage()
    {
        damage += 5f;
    }

    public virtual void UpgradeSpeed()
    {
        attackInterval = Mathf.Max(0.1f, attackInterval - 0.1f);
    }

    public virtual void Upgrade()
    {
        level++;
        UpgradeDamage();
    }

    public virtual void UpgradeProjectileCount() { }

    public virtual void UpgradeCount() { }

    public virtual bool CanUpgradeCount() => true;

    // --- 업그레이드 텍스트 반환 (UI 용도) ---
    public virtual string GetUpgradeText(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Damage => $"{weaponName} 데미지 업",
            UpgradeType.Speed => $"{weaponName} 속도 업",
            UpgradeType.Count => $"{weaponName} 개수 업",
            _ => "???"
        };
    }
}

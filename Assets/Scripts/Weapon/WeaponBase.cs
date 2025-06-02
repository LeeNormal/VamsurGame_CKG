using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;

    public int level = 1;
    public float damage = 10f;
    public float attackInterval = 1f;
    protected float lastAttackTime = 0f;

    public abstract void Initialize(); // 무기 초기화
    public abstract void Attack();     // 무기 공격

    public virtual void UpgradeDamage()
    {
        damage += 5f; // 기본 데미지 업그레이드 방식
    }

    public virtual void UpgradeSpeed()
    { 
        // 공격속도 감소 (속도 증가 효과)
        attackInterval = Mathf.Max(0.1f, attackInterval - 0.1f);
    }

    public virtual void Upgrade()
    {
        // 필요 시 기본 업그레이드 로직
        level++;
        UpgradeDamage();
    }
}

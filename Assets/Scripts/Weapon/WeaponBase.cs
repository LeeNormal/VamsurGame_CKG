using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;

    public int level = 1;
    public float damage = 10f;
    public float attackInterval = 1f;
    protected float lastAttackTime = 0f;

    public abstract void Initialize();
    public abstract void Attack();

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
}

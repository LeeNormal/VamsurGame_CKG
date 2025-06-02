using UnityEngine;

public class EnergyBallWeapon : WeaponBase
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public override void Initialize()
    {
        weaponName = "EnergyBall";
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackInterval)
            return;

        GameObject target = FindNearestEnemy();
        if (target != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.SetTarget(target.transform);
            projectile.damage = this.damage;
        }

        lastAttackTime = Time.time;
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemys");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                nearest = enemy;
                minDist = dist;
            }
        }

        return nearest;
    }

    public override void UpgradeDamage()
    {
        damage += 10f;
        level++;
        Debug.Log($"{weaponName} 데미지 업! 현재 데미지: {damage}");
    }

    public override void UpgradeSpeed()
    {
        attackInterval = Mathf.Max(0.2f, attackInterval - 0.1f);
        level++;
        Debug.Log($"{weaponName} 발사 속도 업! 현재 발사 간격: {attackInterval}");
    }
}

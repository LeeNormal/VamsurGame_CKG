using System.Collections;
using UnityEngine;

public class EnergyBallWeapon : WeaponBase
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private int projectileCount = 1;
    private int countUpgradeLevel = 0;

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
            Vector3 direction = (target.transform.position - firePoint.position).normalized;
            StartCoroutine(FireProjectiles(direction));
        }

        lastAttackTime = Time.time;
    }

    private IEnumerator FireProjectiles(Vector3 direction)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.SetDirection(direction); // 추적 대신 방향만 기억
            projectile.damage = damage;

            yield return new WaitForSeconds(0.07f); // 발사 딜레이
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    public override void UpgradeDamage()
    {
        damage += 10f;
        level++;
    }

    public override void UpgradeSpeed()
    {
        attackInterval = Mathf.Max(0.2f, attackInterval - 0.1f);
        level++;
    }

    public override void UpgradeCount()
    {
        if (!CanUpgradeCount()) return;

        projectileCount++;
        countUpgradeLevel++;
        level++;
    }

    public override bool CanUpgradeCount()
    {
        return countUpgradeLevel < 4;
    }
}

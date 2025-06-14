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

        GameObject[] targets = FindNearestEnemies(projectileCount);
        foreach (GameObject target in targets)
        {
            if (target == null) continue;

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.SetTarget(target.transform);
            projectile.damage = damage;
        }

        lastAttackTime = Time.time;
    }

    private GameObject[] FindNearestEnemies(int count)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] selected = new GameObject[count];
        float[] minDists = new float[count];
        for (int i = 0; i < count; i++) minDists[i] = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            for (int i = 0; i < count; i++)
            {
                if (dist < minDists[i])
                {
                    for (int j = count - 1; j > i; j--)
                    {
                        minDists[j] = minDists[j - 1];
                        selected[j] = selected[j - 1];
                    }
                    minDists[i] = dist;
                    selected[i] = enemy;
                    break;
                }
            }
        }

        return selected;
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

    public override void UpgradeProjectileCount()
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

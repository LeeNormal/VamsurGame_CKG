using Enemys.EnemyScript;
using System.Collections.Generic;
using UnityEngine;

public class LingWeapon : WeaponBase
{
    [Header("링 회전용")]
    public Transform inside;
    public Transform outside;
    public float insideRotationSpeed = 100f;
    public float outsideRotationSpeed = -100f;

    [Header("공격 설정")]
    private float timer;

    [Header("범위 처리")]
    public CircleCollider2D attackRange;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    //  초기 기준값 저장용
    private float initialRadius;
    private Vector3 initialInsideScale;
    private Vector3 initialOutsideScale;

    private void Start()
    {
        // 초기 반지름과 스케일 기록
        if (attackRange != null)
            initialRadius = attackRange.radius;

        if (inside != null)
            initialInsideScale = inside.localScale;
        if (outside != null)
            initialOutsideScale = outside.localScale;
    }

    private void Update()
    {
        RotateRings();

        timer += Time.deltaTime;
        if (timer >= attackInterval)
        {
            AttackEnemies();
            timer = 0f;
        }
    }

    private void RotateRings()
    {
        if (inside != null)
            inside.Rotate(Vector3.forward * insideRotationSpeed * Time.deltaTime);
        if (outside != null)
            outside.Rotate(Vector3.forward * outsideRotationSpeed * Time.deltaTime);
    }

    private void AttackEnemies()
    {
        foreach (Enemy enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"[LingWeapon] {enemy.name}에게 {damage} 데미지");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();
            if (e != null && !enemiesInRange.Contains(e))
            {
                enemiesInRange.Add(e);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();
            if (e != null && enemiesInRange.Contains(e))
            {
                enemiesInRange.Remove(e);
            }
        }
    }

    // --- 업그레이드 기능 ---

    public override void UpgradeDamage()
    {
        damage += 4f;
    }

    public override void UpgradeSpeed()
    {
        attackInterval = Mathf.Max(0.1f, attackInterval - 0.2f);
    }

    public override bool CanUpgradeCount()
    {
        return true; // 범위 증가가 Count 업 효과
    }

    public override void UpgradeCount()
    {
        if (attackRange == null || initialRadius == 0f)
            return;

        attackRange.radius += 0.3f;

        float scaleFactor = attackRange.radius / initialRadius;

        if (inside != null)
            inside.localScale = initialInsideScale * scaleFactor;
        if (outside != null)
            outside.localScale = initialOutsideScale * scaleFactor;
    }

    public override string GetUpgradeText(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Damage => $"{weaponName} 데미지 업!!",
            UpgradeType.Speed => $"{weaponName} 딜레이 감소!!",
            UpgradeType.Count => $"{weaponName} 범위 증가!!",
            _ => "???"
        };
    }

    public override void Initialize()
    {
        // 초기화 시 필요한 동작이 있다면 여기에 작성
    }

    public override void Attack()
    {
        // 명시적 공격 호출이 필요하면 여기에 구현
    }
}

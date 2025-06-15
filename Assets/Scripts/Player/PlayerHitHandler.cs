using Enemys.EnemyScript;
using UnityEngine;

[RequireComponent(typeof(PlayerData))]
public class PlayerHitHandler : MonoBehaviour
{
    //public float damage = 10f;                 // ����� �� �Դ� ���ط�
    public float invincibleDuration = 0.5f;    // ���� �ð�

    private Enemy enemy;
    private PlayerData playerData;
    private bool isInvincible = false;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (isInvincible || playerData.IsDead()) return;

        playerData.TakeDamage(enemy._damage);

        if (!playerData.IsDead())
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }
}

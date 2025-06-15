using UnityEngine;

[RequireComponent(typeof(PlayerData))]
public class PlayerHitHandler : MonoBehaviour
{
    public float damage = 10f;                 // 닿았을 때 입는 피해량
    public float invincibleDuration = 0.5f;    // 무적 시간

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

        playerData.TakeDamage(damage);

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

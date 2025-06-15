using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;
    
    public PlayerHealthBar healthBar;

    public GameOverUI gameOverUI;               // 추가 됨
    
    public event Action<float> OnHpChanged;

    private bool isDead = false;

    private void Awake()
    {
        currentHp = maxHp;

        if (healthBar != null)
        {
            healthBar.playerData = this;
            OnHpChanged += HandleHpChanged;
            OnHpChanged?.Invoke(1f);
        }
    }

    private void HandleHpChanged(float ratio)
    {
        healthBar?.UpdateBar(ratio);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHp = Mathf.Clamp(currentHp - amount, 0f, maxHp);
        OnHpChanged?.Invoke(currentHp / maxHp);

        if (currentHp <= 0f && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        var controller = GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = false;

        Transform weaponManager = transform.Find("WeaponManager");
        if (weaponManager != null)
        {
            foreach (Transform weapon in weaponManager)
            {
                if (weapon.CompareTag("Weapon"))
                    Destroy(weapon.gameObject);
            }
        }

        var animator = GetComponentInChildren<Animator>();
        if (animator != null)
            animator.enabled = false;
        gameOverUI.ShowGameOver();
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHp = Mathf.Clamp(currentHp + amount, 0f, maxHp);
        OnHpChanged?.Invoke(currentHp / maxHp);
    }

    public void ResetHp()
    {
        currentHp = maxHp;
        isDead = false;
        OnHpChanged?.Invoke(1f);
    }

    public bool IsDead() => isDead;
}

using Enemys.EnemyScript;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 0f;

    private Vector3 moveDirection;
    private bool initialized = false;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
        initialized = true;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Update()
    {
        if (!initialized) return;

        transform.position += moveDirection * speed * Time.deltaTime;

        // 화면 밖이면 삭제
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPos.x < -0.1f || screenPos.x > 1.1f || screenPos.y < -0.1f || screenPos.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // 적에게 맞으면 소멸
        }
    }
}

using Enemys.EnemyScript;
using UnityEngine;

public class OrbitSwordCollider : MonoBehaviour
{
    public float damage = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}

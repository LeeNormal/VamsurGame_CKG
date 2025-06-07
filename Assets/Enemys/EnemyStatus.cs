using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float maxHp;
    private float currentHp;
    private bool isDead = false;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHp -= damage;
        Debug.Log(this.name + " : " + currentHp);
        if (currentHp <= 0)
        {
            isDead = true;
        }
    }
}

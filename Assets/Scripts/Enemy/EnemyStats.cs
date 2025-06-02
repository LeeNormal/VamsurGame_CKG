using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHp = 100f;
    private float currentHp;
    private bool isDead = false;

    private EnemyController enemyController;

    void Start()
    {
        currentHp = maxHp;
        enemyController = GetComponent<EnemyController>();
    }

    public void TakeDamage(float dmg)
    {
        if (isDead) return;

        currentHp -= dmg;
        Debug.Log(this.name + " : " + currentHp);
        if (currentHp <= 0)
        {
            isDead = true;
            enemyController.Die(); // 죽음 처리
        }
    }
}

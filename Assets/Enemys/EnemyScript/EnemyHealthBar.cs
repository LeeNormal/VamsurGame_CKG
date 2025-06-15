using Enemys.EnemyScript;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage;
    private Enemy targetEnemy;
    public Vector3 offset = new Vector3(0, 1.5f, 0); // 적 머리 위 위치 조정

    public void Bind(Enemy enemy)
    {
        targetEnemy = enemy;
        UpdateBar(enemy._curHp / enemy.enemyData.maxHp);
    }

    public void UpdateBar(float fill)
    {
        fillImage.fillAmount = fill;
    }
}

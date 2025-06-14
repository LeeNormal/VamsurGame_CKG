using Enemys.EnemyScript;
using UnityEngine;

public class MagicJing : MonoBehaviour
{
    private Enemy _enemy;

    private float _jingDamage = 20f;

    private void Start()
    {
        if(!_enemy)
        {
            _enemy = GetComponent<Enemy>();
        }
        else
        {
            Debug.LogError("적 스크립트를 찾을수가 없습니다.");
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        _enemy = other.GetComponent<Enemy>();
        if (!_enemy) return;
        _enemy.TakeDamage(_jingDamage);
        Debug.Log("데미지를 입혔습니다." + _enemy._curHp);
    }

    

}

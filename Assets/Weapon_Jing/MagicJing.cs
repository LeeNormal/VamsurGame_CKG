using UnityEngine;

public class MagicJing : MonoBehaviour
{
    

    NomalEnemy normalEnemy;
    TankEnemy tankEnemy;

    float Jing_Damage = 1.0f;

    void Start()
    {
        if(normalEnemy == null)
        {
            normalEnemy = GetComponent<NomalEnemy>();
        }
        else { }
        if(tankEnemy == null)
        {
            tankEnemy = GetComponent<TankEnemy>();
        }
    }

    void Update()
    {
        
    }

    public void Tank_Damage(float nHit)
    {
        tankEnemy.Tank_CurHp -= nHit;
        Debug.Log("몬스터 데미지 받는중 : " + tankEnemy.Tank_CurHp);
        Debug.Log("받은 공격 데미지 : " + nHit);
    }

    public void Normal_Damage(float nHit)
    {
        normalEnemy.Normal_CurHp -= nHit;
        Debug.Log("몬스터 데미지 받는중 : " + normalEnemy.Normal_CurHp);
        Debug.Log("받은 공격 데미지 : " + nHit);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            tankEnemy = collision.gameObject.GetComponent<TankEnemy>();
            normalEnemy = collision.gameObject.GetComponent<NomalEnemy>();
            if (tankEnemy != null)
            {
                Tank_Damage(Jing_Damage);
            }
            else { }
            if (normalEnemy != null)
            {
                Normal_Damage(Jing_Damage);
            }
            else { }
        }
    }

    

}

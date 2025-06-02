using SmallScaleInc.TopDownPixelCharactersPack1;
using UnityEngine;

public class NomalEnemy : MonoBehaviour
{
    float Normal_MaxHp = 100;           // 최대 체력
    public float Normal_CurHp = 0;      // 현재 체력

    float fSpeed = 2.5f;                // 적 속도

    SpriteRenderer sprite;              //스프라이트렌더러

    public Transform playerTransform;   // 플레이어 위치
    public GameObject expOrbPrefab;     // 인스펙터에 프리팹 연결
    bool isDead = false;

    void Start()
    {
        
        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        else { }
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
        }
        else { }
        Normal_CurHp = Normal_MaxHp;
    }
    void Update()
    {
        if (transform.position.x < playerTransform.position.x)
            FlipByScale(true);
        else
            FlipByScale(false);
        Die();
        EnemySpeedUp();
        playerRunAfter();
    }

    // 플레이어 따라가기
    void playerRunAfter()
    {
        Vector2 targetPosition = playerTransform.position;
        Vector2 currentPosition = transform.position;

        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition,
            fSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    public void Die()
    {
        if(Normal_CurHp <= 0)
        {
            isDead = true;

            Instantiate(expOrbPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else { }
    }

    void FlipByScale(bool faceLeft) // 좌우 회전
    {
        Vector3 scale = transform.localScale;
        // 왼쪽 보기(faceLeft == true)면 x는 음수, 오른쪽 보기면 양수
        scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1f : 1f);
        transform.localScale = scale;
    }

    void EnemySpeedUp() // 시간 지날때마다 적의 속도 증가
    {
        int nTime = (int)Time.deltaTime;
        if (nTime == 1125)
        {
            fSpeed = 3.0f;
        }
        else if (nTime == 2250)
        {
            fSpeed = 4.0f;
        }
        else { }
    }
}

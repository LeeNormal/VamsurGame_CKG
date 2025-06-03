using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    float Tank_MaxHp = 150;             // 최대 체력
    public float Tank_CurHp = 0;        // 현재 체력

    float fSpeed = 1.0f;                // 적 속도

    SpriteRenderer sprite;              //스프라이트렌더러

    public Transform playerTransform;   // 플레이어 위치
    public GameObject expOrbPrefab;     // 인스펙터에 프리팹 연결

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
        Tank_CurHp = Tank_MaxHp;
    }

    void Update()
    {
        if (transform.position.x > playerTransform.position.x)
            FlipByScale(true);
        else
            FlipByScale(false);
        Die();
        EnemySpeedUp();
        playerRunAfter();
    }

    public void Die()
    {
        if (Tank_CurHp <= 0)
        {
            Instantiate(expOrbPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else { }
    }

    void playerRunAfter() // 플레이어 따라가기
    {
        Vector2 targetPosition = playerTransform.position;
        Vector2 currentPosition = transform.position;

        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition,
            fSpeed * Time.deltaTime);
        transform.position = newPosition;
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
        if (nTime == 4500)
        {
            fSpeed = 1.5f;
        }
        else if (nTime == 9000)
        {
            fSpeed = 2f;
        }
        else { }
    }

    
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.5f;                      // 이동 속도
    private Transform player;                           // 플레이어 Transform
    private Rigidbody2D rb;                             // Rigidbody2D
    private Vector2 moveDir;                            // 이동 방향

    public SPUM_Prefabs spumPrefab;                     // SPUM 프리팹 참조
    private bool isDead = false;                        // 죽음 여부
    public GameObject expOrbPrefab;                     // 인스펙터에 프리팹 연결
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 플레이어 태그로 찾기
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (spumPrefab == null)
        {
            spumPrefab = GetComponentInChildren<SPUM_Prefabs>();
        }

        if (spumPrefab != null)
        {
            if (!spumPrefab.allListsHaveItemsExist())
                spumPrefab.PopulateAnimationLists();

            spumPrefab.OverrideControllerInit();
        }

        if (player == null)
            Debug.LogWarning("Player를 찾을 수 없습니다.");
        if (spumPrefab == null)
            Debug.LogWarning("SPUM_Prefabs가 존재하지 않습니다.");
    }

    void Update()
    {
        if (player == null || spumPrefab == null || isDead) return;

        // 방향 계산
        Vector2 direction = player.position - transform.position;
        moveDir = direction.normalized;

        // 스프라이트 반전
        if (direction.x < 0.01f)
        {
            spumPrefab.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x > -0.01f)
        {
            spumPrefab.transform.localScale = new Vector3(-1, 1, 1);
        }

        // 항상 MOVE 애니메이션 재생
        if (spumPrefab.StateAnimationPairs.ContainsKey("MOVE"))
        {
            spumPrefab.PlayAnimation(PlayerState.MOVE, 0);
        }
    }

    void FixedUpdate()
    {
        if (player == null || isDead) return;

        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        Instantiate(expOrbPrefab, transform.position, Quaternion.identity);

        if (spumPrefab != null && spumPrefab.StateAnimationPairs.ContainsKey("DEATH"))
        {
            spumPrefab.PlayAnimation(PlayerState.DEATH, 0);
            Destroy(gameObject, 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}

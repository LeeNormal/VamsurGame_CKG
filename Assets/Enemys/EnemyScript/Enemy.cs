using UnityEngine;

namespace Enemys.EnemyScript
{
    public class Enemy : MonoBehaviour
    {
        private System.Action<Enemy> _onReturn; // 적이 Pool로 돌아갈때 호출 할 콜백 함수

        [Header("기본 설정")]
        public EnemyData enemyData;             // 스크립터블오브젝트에서 가져온 적 데이터
        public GameObject expOrbPrefab;         // 경험치 오브젝트 프리팹

        [Header("체력 관련")]
        public float _curHp;                    // 현재 체력
        public float _damage;                  // 공격력
        private float _speed;                   // 이동 속도
        private bool _isDead;                   // 사망 여부
        public EnemyHealthBar healthBar;        // 이미 계층에 있는 체력바 오브젝트 연결 (drag & drop)

        [Header("기타")]
        private Transform _player;              // 플레이어 위치 참조
        private SpriteRenderer _sprite;         // 스프라이트

        // 풀로 반환될 때 사용할 함수 등록 역할
        public void Init(System.Action<Enemy> returnAction)
        {
            _onReturn = returnAction;
        }

        private void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (!_player)
            {
                Debug.LogError("플레이어를 찾을 수 없습니다.");
            }

            _curHp = enemyData.maxHp;
            _speed = enemyData.baseSpeed;
            _damage = enemyData.baseDamage;

            // 체력바가 연결되어 있다면 초기화
            if (healthBar != null)
            {
                healthBar.Bind(this); // EnemyHealthBar에 연결
                healthBar.UpdateBar(_curHp / enemyData.maxHp);
            }
        }

        private void Update()
        {
            // 화면 왼쪽에서 벗어나면 풀로 반환
            if (transform.position.x < -10f)
            {
                _onReturn?.Invoke(this);
            }

            FlipByScale(transform.position.x < _player.position.x);
            Die();
            EnemyStateUp();
            PlayerRunAfter();
        }

        // 사망 처리
        private void Die()
        {
            if (!(_curHp <= 0)) return;

            _isDead = true;
            Instantiate(expOrbPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // 플레이어 추적 이동
        private void PlayerRunAfter()
        {
            Vector2 target = _player.position;
            Vector2 current = transform.position;
            transform.position = Vector2.MoveTowards(current, target, _speed * Time.deltaTime);
        }

        // 플레이어 방향에 따라 스프라이트 반전
        private void FlipByScale(bool faceLeft)
        {
            if (_sprite == null || enemyData == null) return;
            bool shouldFlip = enemyData.faceLeftByDefault ? faceLeft : !faceLeft;
            _sprite.flipX = shouldFlip;
        }

        // 경과 시간에 따라 체력 및 속도 조정
        private void EnemyStateUp()
        {
            var time = (int)Time.time;
            foreach (var level in enemyData.speedLevels)
            {
                if (time >= level.time)
                {
                    _speed = level.speed;
                    _curHp = level.hp;
                    _damage = level.damage;
                }
            }
        }

        // 외부에서 데미지를 받을 때 호출
        public void TakeDamage(float damage)
        {
            _curHp -= damage;
            _curHp = Mathf.Clamp(_curHp, 0, enemyData.maxHp);

            if (healthBar != null)
                healthBar.UpdateBar(_curHp / enemyData.maxHp);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerData playerdata = other.GetComponent<PlayerData>();
                if (playerdata != null)
                {
                    playerdata.TakeDamage(_damage);
                }
            }
        }
    }
}

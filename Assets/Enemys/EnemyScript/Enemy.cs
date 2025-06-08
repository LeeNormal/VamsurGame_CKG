using UnityEngine;

namespace Enemys.EnemyScript
{
    public class Enemy : MonoBehaviour
    {
        private System.Action<Enemy> _onReturn; // 적이 Pool로 돌아갈때 호출 할 콜백 함수
        
        public EnemyData enemyData;             // 스크립터블오브젝트에서 가져온 적 데이터
        public GameObject expOrbPrefab;         // 경험치 오브젝트 프리팹
    
        public float _curHp;                    // 현재 체력
        private float _speed;                   // 현재 이동 속도
        private Transform _player;              // 플레이어 위치 참조
        private SpriteRenderer _sprite;         // 스프라이트
        private bool _isDead;                   // 죽는 여부
        
                                                // 풀로 반환될 때 사용할 함수 등록 역할
        public void Init(System.Action<Enemy> returnAction)
        {
            _onReturn = returnAction;
        }
        
        private void Start()                    // 시작 시 컴포넌트들 초기화하고 EnemyData에 가져오는 설정들
        {
            _sprite = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (!_player)
            {
                Debug.LogError("플레이어를 찾을 수 없습니다.");
            }

            _curHp = enemyData.maxHp;
            _speed = enemyData.baseSpeed;
        }

        private void Update()
        {
            // 화면 왼쪽에서 10보다 작으면 풀로 돌아감
            if (transform.position.x < -10f)
            {
                _onReturn?.Invoke(this);
            }
            FlipByScale(transform.position.x > _player.position.x);
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
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
        // 플레이어를 향해 추적 이동
        private void PlayerRunAfter()
        {
            Vector2 target = _player.position;
            Vector2 current = transform.position;
            transform.position = Vector2.MoveTowards(current, target, _speed * Time.deltaTime);
        }
        
        // 플레이어 방향 바라보게 스프라이트 반전
        private void FlipByScale(bool faceLeft)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1 : 1);
            transform.localScale = scale;
        }
        
        // 게임 시간에 따라 속도 증가
        private void EnemyStateUp()
        {
            var time = (int)Time.time;
            foreach (var level in enemyData.speedLevels)
            {
                if (time >= level.time)
                {
                    _speed = level.speed;
                    _curHp = level.hp;
                }
            }
        }
        
        
        
        // 다른 스크립트에서(무기) 이 함수를 호출해서 적에게 데미지를 주는 함수
        public void TakeDamage(float damage)
        {
            _curHp -= damage;
        }
    }
}

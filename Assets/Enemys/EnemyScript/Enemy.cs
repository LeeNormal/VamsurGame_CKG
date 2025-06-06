using UnityEngine;

namespace Enemys.EnemyScript
{
    public class Enemy : MonoBehaviour
    {
        public EnemyData enemyData;
        public GameObject expOrbPrefab;
    
        public float _curHp;
        private float _speed;
        private Transform _player;
        private SpriteRenderer _sprite;
        private bool _isDead;
        
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
        }

        private void Update()
        {
            FlipByScale(transform.position.x > _player.position.x);
            Die();
            EnemySpeedUp();
            PlayerRunAfter();
        }
        
        private void Die()
        {
            if (!(_curHp <= 0)) return;
            _isDead = true;
            Instantiate(expOrbPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        private void PlayerRunAfter()
        {
            Vector2 target = _player.position;
            Vector2 current = transform.position;
            transform.position = Vector2.MoveTowards(current, target, _speed * Time.deltaTime);
        }
        
        private void FlipByScale(bool faceLeft)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1 : 1);
            transform.localScale = scale;
        }
        
        private void EnemySpeedUp()
        {
            var time = (int)Time.time;
            foreach (var level in enemyData.speedLevels)
            {
                if (time >= level.time)
                {
                    _speed = level.speed;
                }
            }
        }
        
        public void TakeDamage(float damage)
        {
            _curHp -= damage;
        }
    }
}

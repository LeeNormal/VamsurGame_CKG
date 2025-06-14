using UnityEngine;

namespace Enemys.EnemyScript
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public float maxHp;
        public float baseSpeed;
        public float baseDamage;

        [System.Serializable]
        public struct StatusLevelUp
        {
            public int time;
            public float speed;
            public int hp;
            public int damage;
        }

        public StatusLevelUp[] speedLevels;
    
    }
}

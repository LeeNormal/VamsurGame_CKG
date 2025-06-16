using NUnit.Framework.Constraints;
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
        public int expValue = 50; // ���� ���� �� ����� ����ġ��

        public bool faceLeftByDefault = true;

        [System.Serializable]
        public struct StatusLevelUp
        {
            public int time;
            public int hp;
        }

        public StatusLevelUp[] speedLevels;
    
    }
}

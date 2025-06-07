using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public ObjectPool<GameObject> _pool;
    public Queue<GameObject> _usedEnemy = new Queue<GameObject>();

    int random;

    List<GameObject> m_listEnemies = new List<GameObject>();
    int nEnemyCount = 5;

    private void Awake()
    {
        
    }

    void Start()
    {

    }
    void Update()
    {
        
    }
    void Spawn()
    {
        if(_pool != null)
        {
            for(int i = m_listEnemies.Count - 1; i >=0;i--)
            {
                if (m_listEnemies[i] == null)
                {
                    m_listEnemies.RemoveAt(i);
                }
                else { }
            }
            if(m_listEnemies.Count < nEnemyCount)
            {

            }
        }
    }
}

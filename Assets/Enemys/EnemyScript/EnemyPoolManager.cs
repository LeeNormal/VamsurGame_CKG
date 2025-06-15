using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using Enemys.EnemyScript;
using Random = UnityEngine.Random;

public class EnemyPoolManager : MonoBehaviour
{
    public GameObject normalEnemyPrefab;
    public GameObject tankEnemyPrefab;

    private ObjectPool<Enemy> _normalPool;
    private ObjectPool<Enemy> _tankPool;

    public static EnemyPoolManager Instance;
    
    private void Awake()
    {
        Instance = this;

        _normalPool = new ObjectPool<Enemy>(
            createFunc: () =>
            {
                var go = Instantiate(normalEnemyPrefab);
                var e = go.GetComponentInChildren<Enemy>();
                e.Init(ReturnNormalEnemy);
                return e;
            },
            actionOnGet: e => e.gameObject.SetActive(true),
            actionOnRelease: e => e.gameObject.SetActive(false),
            //Destroy말고 e.gameObject.SetActive(false)로 해보면 하이어라키창에 계속 생겨 메모리 낭비가 있음
            actionOnDestroy: e => Destroy(e.gameObject),
            defaultCapacity: 3,
            maxSize: 100
        );
        
        _tankPool = new ObjectPool<Enemy>(
            createFunc: () => {
                var go = Instantiate(tankEnemyPrefab);
                var e = go.GetComponentInChildren<Enemy>();
                e.Init(ReturnTankEnemy);
                return e;
            },
            actionOnGet: e => e.gameObject.SetActive(true),
            actionOnRelease: e => e.gameObject.SetActive(false),
            actionOnDestroy: e => Destroy(e.gameObject),
            defaultCapacity: 3,
            maxSize: 100
        );
    }

    public Enemy GetRandomEnemy()
    {
        var roll = Random.Range(0, 2);
        return roll == 0 ? _normalPool.Get() : _tankPool.Get();
    }

    private void ReturnNormalEnemy(Enemy enemy)
    {
        _normalPool.Release(enemy);
    }

    private void ReturnTankEnemy(Enemy enemy)
    {
        _tankPool.Release(enemy);
    }
    
}

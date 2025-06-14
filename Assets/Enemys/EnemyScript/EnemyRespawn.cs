using System.Collections.Generic;
using Enemys.EnemyScript;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class EnemyRespawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _normalEnemy;
    [SerializeField]
    private GameObject _tankEnemy;

    // private ObjectPool<Enemy> _normalEnemyPool;
    // private ObjectPool<Enemy> _tankEnemyPool;
    
    List<GameObject> NormalEnemys = new List<GameObject>(10);
    List<GameObject> TankEnemys = new List<GameObject>(10);

    public enum ObjectType
    {
        NormalEnemy,
        TankEnemy,
    };

    private GameObject GetPrefabForType(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.NormalEnemy:
                return _normalEnemy;
            case ObjectType.TankEnemy:
                return _tankEnemy;
        }

        return null;
    }

    private List<GameObject> GetListForType(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.NormalEnemy:
                return NormalEnemys;
            case ObjectType.TankEnemy:
                return TankEnemys;
        }

        return null;
    }

    public GameObject RequestEnemyObject(ObjectType type)
    {
        GameObject obj = null;
        
        List<GameObject> list = GetListForType(type);
        GameObject prefab = GetPrefabForType(type);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].activeInHierarchy)
            {
                obj = list[i];
                obj.SetActive(true);
                break;
            }
        }

        if (!obj)
        {
            obj = Instantiate(prefab);
            obj.name = prefab.name;
            obj.transform.SetParent(transform);
            list.Add(obj);
        }

        return obj;
    }
}

using UnityEngine;
using UnityEngine.Pool;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform player;

    private ObjectPool<GameObject> _enemyPool;
    
    private void Start()
    {
        _enemyPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var idx = Random.Range(0, enemyPrefabs.Length);
                var go = Instantiate(enemyPrefabs[idx]);
                //go.GetComponent<NomalEnemy>().in;
                return go;
            },
            actionOnGet: go =>
            {
                go.SetActive(true);
                //go.transform.position = GetRandomSpawnPosition();
            },
            actionOnRelease: go =>
            {
                go.SetActive(false);
            },
            actionOnDestroy: Destroy,
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 테스트용 소환
        {
            //enemyPool.Get();
        }
    }
}

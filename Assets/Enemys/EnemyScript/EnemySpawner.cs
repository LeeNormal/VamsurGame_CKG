using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    //public float spawnRadius = 7f; 23번줄에 곱하면 생성하는 거리를 더 늘릴수있다

    public GameObject EndPrefab;
    private bool endSpawned = false;
    
    private float timer = 0f;
    private float endtimer = 0f;
    private float interval = 2f;

    private void Update()
    {
        timer += Time.deltaTime;
        endtimer += Time.deltaTime;
        if (!(timer >= interval)) return;
        SpawnEnemy();
        timer = 0f;
    }

    private void SpawnEnemy()
    {
        var dir = Random.insideUnitCircle.normalized;
        var pos = player.position + new Vector3(dir.x * 10, dir.y * 5, 0);

        var enemy = EnemyPoolManager.Instance.GetRandomEnemy();
        enemy.transform.position = pos;
        
        if (!endSpawned && endtimer >= 300f)
        {
            Vector3 randomPos = player.position + (Vector3)(Random.insideUnitCircle.normalized * 10f);
            
            Instantiate(EndPrefab, randomPos, Quaternion.identity);
            endSpawned = true;
        }
    }
    
}

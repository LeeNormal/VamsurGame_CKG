using Enemys.EnemyScript;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    //public float spawnRadius = 7f; 23번줄에 곱하면 생성하는 거리를 더 늘릴수있다

    public GameObject EndPrefab;
    private bool endSpawned = false;
    
    private float timer = 0f;
    private float wavetimer = 0f;
    private float endtimer = 0f;
    
    private float interval = 2f;
    private float waveInterval = 30f;
    private int waveCounter = 20;
    private void Update()
    {
        timer += Time.deltaTime;
        endtimer += Time.deltaTime;
        wavetimer += Time.deltaTime;
        
        if (!(timer >= interval)) return;
        SpawnEnemy();
        timer = 0f;
        if (wavetimer >= waveInterval)
        {
            SpawnCircleEnemies(waveCounter, 8f);
            wavetimer = 0f;
            waveCounter += 10;
        }
    }

    private void SpawnEnemy()
    {
        int spawnCount = 3;

        for (int i = 0; i < spawnCount; i++)
        {
            var dir = Random.insideUnitCircle.normalized;
            var pos = player.position + new Vector3(dir.x * 10, dir.y * 5, 0);

            var enemy = EnemyPoolManager.Instance.GetRandomEnemy();
            enemy.transform.position = pos;
            enemy.gameObject.SetActive(true); // 풀 사용 시 필요
        }

        if (!endSpawned && endtimer >= 300f)
        {
            Vector3 randomPos = player.position + (Vector3)(Random.insideUnitCircle.normalized * 10f);
            Instantiate(EndPrefab, randomPos, Quaternion.identity);
            endSpawned = true;
        }
    }

    private void SpawnCircleEnemies(int count, float radius)
    {
        Vector3 playerPos = player.position;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            Vector3 spawnPos = new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0
            ) + playerPos;

            Enemy enemy = EnemyPoolManager.Instance.GetRandomEnemy();
            enemy.transform.position = spawnPos;
            enemy.gameObject.SetActive(true);
        }
    }
    
}

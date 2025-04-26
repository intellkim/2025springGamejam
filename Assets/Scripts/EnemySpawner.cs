using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public PlayerController2D player;
    public float spawnInterval = 2f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int randomSide = Random.Range(0, 4); // 0~3 중 하나 랜덤

        Vector2 spawnPos = Vector2.zero;

        switch (randomSide)
        {
            case 0: // 오른쪽
                spawnPos = new Vector2(10f, Random.Range(-4f, 4f));
                break;
            case 1: // 왼쪽
                spawnPos = new Vector2(-10f, Random.Range(-4f, 4f));
                break;
            case 2: // 위쪽
                spawnPos = new Vector2(Random.Range(-8f, 8f), 6f);
                break;
            case 3: // 아래쪽
                spawnPos = new Vector2(Random.Range(-8f, 8f), -6f);
                break;
        }
        GameObject prefabToSpawn = GetRandomEnemyPrefab();
        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        Enemy2D enemyScript = newEnemy.GetComponent<Enemy2D>();
        if (enemyScript != null)
        {
            enemyScript.player = player;
            enemyScript.SetMoveDirectionTowards(player.transform.position);
        }
    }
    GameObject GetRandomEnemyPrefab()
    {
        float rand = Random.value; // 0.0 ~ 1.0 사이 랜덤값

        if (rand < 0.7f) // 70% 확률: 5만원 enemy
            return enemyPrefabs[0];
        else if (rand < 0.9f) // 다음 20% 확률: 10만원 enemy
            return enemyPrefabs[1];
        else // 나머지 10% 확률: 20만원 enemy
            return enemyPrefabs[2];
    }
}

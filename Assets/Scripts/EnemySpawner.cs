using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
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
        float randomY = Random.Range(-3f, 3f); // y축 위치 랜덤
        Vector2 spawnPos = new Vector2(10f, randomY);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        Enemy2D enemyScript = newEnemy.GetComponent<Enemy2D>();
        if (enemyScript != null)
        {
            enemyScript.player = player;
        }
    }
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float initialSpawnInterval = 2f;
    public float spawnIntervalDecrease = 0.1f;
    public float minSpawnInterval = 0.5f;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Adjust this to fit your map size

    private float currentSpawnInterval;
    private float timer;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        timer = currentSpawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
            currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecrease, minSpawnInterval);
            timer = currentSpawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float y = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        return new Vector2(x, y) + (Vector2)transform.position;
    }
}

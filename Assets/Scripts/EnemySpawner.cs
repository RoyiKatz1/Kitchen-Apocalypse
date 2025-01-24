using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public float spawnWeight = 1f;
    }

    public List<EnemyType> enemyTypes = new List<EnemyType>();
    public float initialSpawnInterval = 4f;
    public float spawnIntervalDecrease = 0.1f;
    public float minSpawnInterval = 0.5f;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);

    public float statUpdateInterval = 30f;
    private float lastStatUpdateTime = 0f;

    private float currentSpawnInterval;
    private float timer;
    private float totalWeight;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        timer = currentSpawnInterval;
        CalculateTotalWeight();
    }

    void CalculateTotalWeight()
    {
        totalWeight = 0f;
        foreach (var enemyType in enemyTypes)
        {
            totalWeight += enemyType.spawnWeight;
        }
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

        // Update enemy stats periodically
        if (Time.time - lastStatUpdateTime >= statUpdateInterval)
        {
            UpdateAllEnemyStats();
            lastStatUpdateTime = Time.time;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject enemyToSpawn = ChooseEnemyToSpawn();
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        // Update stats for the newly spawned enemy
        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.UpdateStats();
        }
    }

    GameObject ChooseEnemyToSpawn()
    {
        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var enemyType in enemyTypes)
        {
            cumulativeWeight += enemyType.spawnWeight;
            if (randomValue <= cumulativeWeight)
            {
                return enemyType.enemyPrefab;
            }
        }

        // Fallback to the first enemy type if something goes wrong
        return enemyTypes[0].enemyPrefab;
    }

    Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-spawnAreaSize.x, spawnAreaSize.x);
        float y = Random.Range(-spawnAreaSize.y, spawnAreaSize.y - 2);
        return new Vector2(x, y) + (Vector2)transform.position;
    }

    void UpdateAllEnemyStats()
    {
        // Using FindObjectsByType instead of FindObjectsOfType
        Enemy[] allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in allEnemies)
        {
            enemy.UpdateStats();
        }
    }
}

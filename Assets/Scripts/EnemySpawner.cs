using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 20;
    public float spawnInterval = 2f;

    private List<GameObject> enemyPool;
    private float timer;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = GetPooledEnemy();
        if (enemy != null)
        {
            enemy.transform.position = GetRandomSpawnPosition();
            enemy.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No available enemies in pool");
        }
    }

    GameObject GetPooledEnemy()
    {
        return enemyPool.Find(e => !e.activeInHierarchy);
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Implement your spawn position logic here
        return Vector3.zero;
    }
}

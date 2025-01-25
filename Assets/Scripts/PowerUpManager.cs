using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 60f;
    public Vector2 spawnArea = new Vector2(10f, 10f);

    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
        );

        Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
    }
}

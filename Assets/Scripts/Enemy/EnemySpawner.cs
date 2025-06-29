using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Settings")]
    public MMMultipleObjectPooler Pooler;
    public List<Transform> spawnPoints;
    public float baseDelayBetweenSpawns = 0.2f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnWave(int wave, int totalEnemies)
    {
        float delay = Mathf.Clamp(baseDelayBetweenSpawns - (wave * 0.01f), 0.005f, baseDelayBetweenSpawns);
        StartCoroutine(SpawnEnemies(totalEnemies, delay));
    }

    private IEnumerator SpawnEnemies(int totalEnemies, float delay)
    {
        int spawnIndex = 0;

        for (int i = 0; i < totalEnemies; i++)
        {
            GameObject enemy = Pooler.GetPooledGameObject();
            if (enemy != null)
            {
                Vector3 basePos = spawnPoints[spawnIndex].position;
                Vector2 offset = Random.insideUnitCircle * 1.5f;
                Vector3 spawnPos = basePos + new Vector3(offset.x, 0f, offset.y);

                enemy.transform.position = spawnPos;
                enemy.SetActive(true);
            }

            spawnIndex = (spawnIndex + 1) % spawnPoints.Count;
            yield return new WaitForSeconds(delay);
        }
    }
}

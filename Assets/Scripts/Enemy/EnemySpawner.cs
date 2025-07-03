using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Pool & Spawning")]
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
        float delay = Mathf.Clamp(baseDelayBetweenSpawns - (wave * 0.01f), 0.05f, baseDelayBetweenSpawns);
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
                Vector3 spawnPos = spawnPoints[spawnIndex].transform.position;
                enemy.SetActive(true);
                enemy.transform.position = spawnPos;
            }

            spawnIndex = (spawnIndex + 1) % spawnPoints.Count;
            yield return new WaitForSeconds(delay);
        }
    }
}

using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWaves : MonoBehaviour
{
    public static GameManagerWaves Instance;

    [Header("Waves Settings")]
    public int currentWave = 1;
    public float timeBetweenWaves = 10f;
    public int enemiesPerWave = 5;

    [Header("Score")]
    public int score = 0;
    public int scorePerKill = 10;

    [Header("References")]
    public EnemySpawner enemySpawner;
    public WaveUIManager waveUI;

    [HideInInspector] public int aliveEnemies = 0;

    [Header("Life Pickups")]
    public GameObject healthPickupPrefab;
    public Transform[] healthSpawnPoints; // puntos fijos definidos desde el editor

    [Header("Player")]
    public Health playerHealth; // asignás el componente Health del jugador

    private List<GameObject> spawnedHealthPickups = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());
        StartCoroutine(FindPlayerHealth());
    }

    public IEnumerator StartNextWave()
    {
        waveUI?.UpdateWave(currentWave);
        waveUI?.UpdateScore(score);
        waveUI?.UpdateNextWaveTimer(timeBetweenWaves);

        float timer = timeBetweenWaves;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            waveUI?.UpdateNextWaveTimer(timer);
            yield return null;
        }



        aliveEnemies = enemiesPerWave;
        enemySpawner.SpawnWave(currentWave, enemiesPerWave);
    }


    public void EnemyDied()
    {
        aliveEnemies--;
        AddScore(scorePerKill);
        if (aliveEnemies <= 0)
        {
            currentWave++;
            enemiesPerWave += 2;
            TriggerUpgradePhase(); // acá se pausa y espera decisión

        }
    }

    public void TriggerUpgradePhase()
    {
        UpgradeManager.Instance.ShowUpgradeOptions();
    }


    public void AddScore(int amount)
    {
        score += amount;
        waveUI?.UpdateScore(score);
    }

    public void ResetGame()
    {
        score = 0;
        currentWave = 1;
        enemiesPerWave = 5;
        StartCoroutine(StartNextWave());
    }

    public void SpawnHealthPickups()
    {
        if (healthPickupPrefab == null || healthSpawnPoints.Length == 0 || playerHealth == null) return;

        float healthPercent = playerHealth.CurrentHealth / playerHealth.MaximumHealth;
        int totalPoints = healthSpawnPoints.Length;
        int pickupsToSpawn;

        if (healthPercent > 0.75f)
        {
            pickupsToSpawn = 1;
        }
        else if (healthPercent > 0.5f)
        {
            pickupsToSpawn = Mathf.CeilToInt(totalPoints * 0.25f);
        }
        else if (healthPercent > 0.15f)
        {
            pickupsToSpawn = Mathf.CeilToInt(totalPoints * 0.5f);
        }
        else
        {
            pickupsToSpawn = totalPoints;
        }

        CleanupHealthPickups(); // Limpiar anteriores

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < totalPoints; i++) availableIndices.Add(i);

        for (int i = 0; i < pickupsToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int spawnIndex = availableIndices[randomIndex];
            availableIndices.RemoveAt(randomIndex);

            GameObject pickup = Instantiate(healthPickupPrefab, healthSpawnPoints[spawnIndex].position, Quaternion.identity);
            spawnedHealthPickups.Add(pickup);
        }
    }

    public void CleanupHealthPickups()
    {
        foreach (var pickup in spawnedHealthPickups)
        {
            if (pickup != null)
            {
                Destroy(pickup);
            }
        }

        spawnedHealthPickups.Clear();
    }



    private IEnumerator FindPlayerHealth()
    {
        // Esperamos un frame para asegurarnos que el LevelManager ya instanció al jugador
        yield return null;

        // Buscamos al jugador por tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }

        if (playerHealth == null)
        {
            Debug.LogWarning("No se pudo encontrar el componente Health del jugador.");
        }
    }


}


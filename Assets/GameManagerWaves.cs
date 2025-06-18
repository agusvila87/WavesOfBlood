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

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());
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


}


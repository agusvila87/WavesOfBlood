using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUIManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI nextWaveText;
    public TextMeshProUGUI scoreText;

    public void UpdateWave(int currentWave)
    {
        waveText.text = $"Wave: {currentWave}";
    }


    public void UpdateNextWaveTimer(float seconds)
    {
        nextWaveText.text = $"{Mathf.CeilToInt(seconds)}s";
    }


    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

}


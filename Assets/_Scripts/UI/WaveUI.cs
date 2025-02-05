using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WaveUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI enemyCountText;

    void OnEnable()
    {
        WaveSpawner.CurrentWaveInfo += InitWaveUI;
        WaveManager.CurrentWaveNumber += UpdateWaveNumber;
        WaveManager.CurrentWaveCount += UpdateWaveCount;
        WaveManager.UpdateWaveCountdown += UpdateCountdown;
    }

    void OnDisable()
    {
        WaveSpawner.CurrentWaveInfo -= InitWaveUI;
        WaveManager.CurrentWaveNumber -= UpdateWaveNumber;
        WaveManager.CurrentWaveCount -= UpdateWaveCount;
        WaveManager.UpdateWaveCountdown -= UpdateCountdown;
    }

    void InitWaveUI(Wave currentWave) => enemyCountText.text = $"{currentWave.waveAmount}";
    void UpdateWaveNumber(int currentWave) => waveText.text = $"Wave {currentWave}";
    void UpdateWaveCount(int newAmount) => enemyCountText.text = $"{Mathf.Clamp(newAmount, 0f, Mathf.Infinity)}";
    void UpdateCountdown(int countDown) => waveText.text = $"New Wave In {countDown}...";
}

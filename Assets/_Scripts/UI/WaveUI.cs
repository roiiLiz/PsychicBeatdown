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
        WaveManager.CurrentWaveCount += UpdateWaveCount;
        WaveManager.UpdateWaveCountdown += UpdateCountdown;
    }

    void OnDisable()
    {
        WaveSpawner.CurrentWaveInfo -= InitWaveUI;
        WaveManager.CurrentWaveCount -= UpdateWaveCount;
        WaveManager.UpdateWaveCountdown -= UpdateCountdown;
    }

    void InitWaveUI(Wave wave)
    {
        waveText.text = wave.waveName;
        enemyCountText.text = $"{wave.waveAmount}";
    }

    void UpdateWaveCount(int newAmount)
    {
        enemyCountText.text = $"{newAmount}";
    }

    void UpdateCountdown(int countDown)
    {
        waveText.text = $"New Wave In {countDown}...";
    }
}

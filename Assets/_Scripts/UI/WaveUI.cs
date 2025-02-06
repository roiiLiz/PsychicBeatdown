using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WaveUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI loopText;
    [SerializeField] TextMeshProUGUI enemyCountText;

    float currentLoop;

    void OnEnable()
    {
        WaveSpawner.WaveAmount += InitWaveUI;
        WaveManager.CurrentWaveNumber += UpdateWaveNumber;
        WaveManager.CurrentWaveCount += UpdateWaveCount;
        WaveManager.UpdateWaveCountdown += UpdateCountdown;
        WaveManager.UpdateLoopCount += UpdateLoopCount;
        LoopAnimator.LoopIndicate += UpdateLoopText;
    }

    void OnDisable()
    {
        WaveSpawner.WaveAmount -= InitWaveUI;
        WaveManager.CurrentWaveNumber -= UpdateWaveNumber;
        WaveManager.CurrentWaveCount -= UpdateWaveCount;
        WaveManager.UpdateWaveCountdown -= UpdateCountdown;
        WaveManager.UpdateLoopCount -= UpdateLoopCount;
        LoopAnimator.LoopIndicate -= UpdateLoopText;
    }

    void Start()
    {
        UpdateLoopText();
    }

    void InitWaveUI(int currentWave) => enemyCountText.text = $"{currentWave}";
    void UpdateWaveNumber(int currentWave) => waveText.text = $"Wave {currentWave}";
    void UpdateWaveCount(int newAmount) => enemyCountText.text = $"{Mathf.Clamp(newAmount, 0f, Mathf.Infinity)}";
    void UpdateCountdown(int countDown) => waveText.text = $"New Wave In {countDown}...";
    void UpdateLoopCount(int loopNumber) => currentLoop = loopNumber;
    void UpdateLoopText() => loopText.text = $"{currentLoop}";
}

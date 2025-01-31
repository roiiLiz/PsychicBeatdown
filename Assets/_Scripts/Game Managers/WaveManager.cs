using System;
using System.Collections;
using UnityEngine;

// Responsible for starting and stopping waves, as well as cooldowns between waves

// public enum WaveState
// {
//     IN_PROGRESS,
//     SPAWNING,
//     COOLDOWN
// }

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveSpawner spawner;
    [SerializeField] float waveCooldown = 5f;

    int currentEnemyCount = 0;
    int currentWave = 0;
    float currentCountDownValue;

    public static event Action<int> CurrentWaveCount;
    public static event Action<float, int> RequestNextWave;
    public static event Action<int> UpdateWaveCountdown;

    void OnEnable()
    {
        WaveSpawner.CurrentWaveInfo += InitWaveInfo;
        EnemyDeathComponent.OnEnemyDeath += UpdateEnemyCount;
    }

    void OnDisable()
    {
        WaveSpawner.CurrentWaveInfo += InitWaveInfo;
        EnemyDeathComponent.OnEnemyDeath += UpdateEnemyCount;
    }

    void Start()
    {
        StartCoroutine(spawner.SpawnWave(currentWave));
    }

    void UpdateEnemyCount()
    {
        currentEnemyCount--;
        CurrentWaveCount?.Invoke(currentEnemyCount);

        if (currentEnemyCount <= 0)
        {
            // Debug.Log($"Spawning wave {currentWave++}");
            currentWave += 1;
            StartCoroutine(SpawnNextWave(waveCooldown, currentWave));
        }
    }

    void InitWaveInfo(Wave wave)
    {
        currentEnemyCount = wave.waveAmount;
    }

    IEnumerator SpawnNextWave(float cooldownDuration, int waveToSpawn)
    {
        currentCountDownValue = cooldownDuration;
        UpdateWaveCountdown?.Invoke(Mathf.RoundToInt(currentCountDownValue));

        while (currentCountDownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentCountDownValue--;
            UpdateWaveCountdown?.Invoke(Mathf.RoundToInt(currentCountDownValue));
        }

        StartCoroutine(spawner.SpawnWave(waveToSpawn));
    }
}


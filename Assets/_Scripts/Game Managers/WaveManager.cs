using System;
using System.Collections;
using UnityEngine;

// Responsible for starting and stopping waves, as well as cooldowns between waves

public enum WaveState
{
    IN_PROGRESS,
    SPAWNING,
    COOLDOWN
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveSpawner spawner;
    [SerializeField] float waveCooldown = 5f;

    int currentEnemyCount = 0;
    int currentWave = 0;

    public static event Action<int> CurrentWaveCount;
    public static event Action<float, int> RequestNextWave;

    void OnEnable()
    {
        WaveSpawner.CurrentWaveInfo += InitWaveInfo; EnemyDeathComponent.OnEnemyDeath += UpdateEnemyCount;
    }

    void OnDisable()
    {
        WaveSpawner.CurrentWaveInfo += InitWaveInfo; EnemyDeathComponent.OnEnemyDeath += UpdateEnemyCount;
    }

    void Start()
    {
        StartCoroutine(spawner.SpawnWave(0));
    }

    void UpdateEnemyCount()
    {
        currentEnemyCount--;
        CurrentWaveCount?.Invoke(currentEnemyCount);

        if (currentEnemyCount <= 0)
        {
            Debug.Log($"Spawning wave {currentWave++}");
            RequestNextWave?.Invoke(waveCooldown, currentWave++);
        }
    }

    void InitWaveInfo(Wave wave)
    {
        currentEnemyCount = wave.waveAmount;
    }
}


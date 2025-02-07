using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

// Responsible for starting and stopping waves, as well as cooldowns between waves
// Also responsible for keeping track of loops

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveSpawner spawner;
    [SerializeField] float waveCooldown = 5.5f;
    [SerializeField] float waveOffset = 0.5f;

    int currentEnemyCount = 0;
    int currentWave = 0;
    int loopCount = 0;
    float waveCountdown = 0f;
    bool allowWaveSpawns = true;

    bool suppressUpdates;
    bool waveCoolingDown;

    public static event Action<int> CurrentWaveCount;
    public static event Action<int> UpdateWaveCountdown;
    public static event Action<int> CurrentWaveNumber;
    public static event Action<int> UpdateLoopCount;
    public static event Action OnLoopBegin;
    public static event Action<bool> WaveCoolingDown;

    void OnEnable()
    {
        WaveSpawner.WaveAmount += InitWaveInfo;
        EnemyDeathComponent.OnEnemyDeath += UpdateEnemyCount;
        LoopAnimator.LoopInProgress += SuppressUpdates;
    }

    void OnDisable()
    {
        WaveSpawner.WaveAmount -= InitWaveInfo;
        EnemyDeathComponent.OnEnemyDeath -= UpdateEnemyCount;
        LoopAnimator.LoopInProgress -= SuppressUpdates;
    }

    void Start()
    {
        StartCoroutine(spawner.SpawnWave(currentWave, loopCount));
        CurrentWaveNumber?.Invoke(currentWave + 1);
        UpdateLoopCount?.Invoke(loopCount);
    }

    void Update()
    {
        if (suppressUpdates) { return; }

        if (waveCountdown > 0f)
        {
            waveCountdown -= Time.deltaTime;
            UpdateWaveCountdown?.Invoke(Mathf.RoundToInt(waveCountdown));
        } else
        {
            if (waveCoolingDown)
            {
                waveCoolingDown = false;
                WaveCoolingDown?.Invoke(waveCoolingDown);
            }
        }
    }

    void UpdateEnemyCount()
    {
        currentEnemyCount--;
        CurrentWaveCount?.Invoke(currentEnemyCount);

        if (currentEnemyCount <= 0 && allowWaveSpawns)
        {
            allowWaveSpawns = false;
            // Debug.Log($"Spawning wave {currentWave++}");
            currentWave += 1;
            CheckForLoop(currentWave);
            StartCoroutine(SpawnNextWave(waveCooldown + waveOffset, currentWave));
        }
    }

    void SuppressUpdates(bool isLoopInProgress) => suppressUpdates = isLoopInProgress;
    void InitWaveInfo(int waveAmount) => currentEnemyCount = waveAmount;

    void CheckForLoop(int currentWave)
    {
        if (currentWave % spawner.uniqueWaveCount == 0)
        {
            loopCount += 1;
            UpdateLoopCount?.Invoke(loopCount);
            OnLoopBegin?.Invoke();
        }
    }

    IEnumerator SpawnNextWave(float cooldownDuration, int waveToSpawn)
    {
        waveCoolingDown = true;
        waveCountdown = cooldownDuration;
        WaveCoolingDown?.Invoke(waveCoolingDown);
        UpdateWaveCountdown?.Invoke(Mathf.RoundToInt(waveCountdown));
        
        yield return new WaitUntil(() => { return waveCountdown <= 0; });

        // Debug.Log("Hello");
        StartCoroutine(spawner.SpawnWave(waveToSpawn, loopCount));

        CurrentWaveNumber?.Invoke(waveToSpawn + 1);
        allowWaveSpawns = true;
    }

    [ContextMenu("Force Next Loop")]
    void NextLoop()
    {
        loopCount++;
        UpdateLoopCount?.Invoke(loopCount);
        OnLoopBegin?.Invoke();
    }
}



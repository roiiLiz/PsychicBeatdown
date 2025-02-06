using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for actually spawning waves, as well as relaying the amount of spawned enemies to the manager / anyone who would like it (i.e ui)

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] List<Wave> waves = new List<Wave>();
    [SerializeField] float timeBetweenSpawns = 0.15f;
    [SerializeField] float waveRadius = 10f;
    [SerializeField, Range(0, 1f)] float perLoopEnemyChance = 0.1f;

    float loopEnemyChance;

    public static event Action<int> WaveAmount; 

    public int uniqueWaveCount { get; private set; }

    void OnEnable() => LoopAnimator.LoopIndicate += IncrementChance;
    void OnDisable() => LoopAnimator.LoopIndicate -= IncrementChance;

    void IncrementChance() => loopEnemyChance += perLoopEnemyChance;

    void Start()
    {
        uniqueWaveCount = waves.Count;
    }

#region Spawning Logic

    public IEnumerator SpawnWave(int waveNumber, int loopNumber)
    {
        if (waveNumber > waves.Count - 1)
        {
            waveNumber = waveNumber % waves.Count;
        }

        int modifiedWaveAmount = waves[waveNumber].waveAmount;
        if (loopNumber > 0)
        {
            modifiedWaveAmount *= Mathf.RoundToInt(loopNumber * waves[waveNumber].loopMultiplier);
        }

        WaveAmount?.Invoke(modifiedWaveAmount);

        for (int i = 0; i < modifiedWaveAmount; i++)
        {
            ChooseWeightedEnemy(waveNumber);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void ChooseWeightedEnemy(int index)
    {
        // Debug.Log(index);
        float totalSpawnChance = 0.0f;
        foreach (WaveSpawn spawn in waves[index].spawns)
        {
            totalSpawnChance += spawn.weight;
        }

        float rand = UnityEngine.Random.Range(0, totalSpawnChance);
        float cumulativeChance = 0.0f;

        foreach (WaveSpawn spawn in waves[index].spawns)
        {
            cumulativeChance += spawn.weight;

            if (rand <= cumulativeChance)
            {
                float isLoop = UnityEngine.Random.Range(0, 1f);
                if (isLoop <= loopEnemyChance)
                {
                    SpawnEnemy(spawn.loopSpawn);
                } else
                {
                    SpawnEnemy(spawn.defaultSpawn);
                }

                return;
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        PlaceEnemy(enemy);
    }

#endregion

#region Placement Logic

    void PlaceEnemy(GameObject enemy)
    {
        Vector3 spawnPoint = RandomPointAtCircleEdge(waveRadius);

        enemy.transform.position = spawnPoint + transform.position;
        enemy.transform.rotation = Quaternion.identity;
    }

    Vector3 RandomPointAtCircleEdge(float radius)
    {
        Vector3 spawnDirection = UnityEngine.Random.insideUnitSphere;
        spawnDirection.z = 0.0f;

        spawnDirection.Normalize();

        var spawnPoint = spawnDirection * radius;

        return new Vector3(spawnPoint.x, spawnPoint.y, 0.0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, waveRadius);
    }

#endregion

}
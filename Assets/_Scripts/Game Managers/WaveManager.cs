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

    void Start()
    {
        StartCoroutine(spawner.SpawnWave(0));
    }
}


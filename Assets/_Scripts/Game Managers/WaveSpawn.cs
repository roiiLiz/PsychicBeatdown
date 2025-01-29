using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WaveSpawn
{
    public GameObject enemyToSpawn;
    [Range(0f, 1f)]
    public float weight = 0.1f;
}


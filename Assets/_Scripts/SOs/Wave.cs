using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Wave", fileName = "New Wave")]
public class Wave : ScriptableObject
{
    public float loopMultiplier;
    public string waveName;
    public List<WaveSpawn> spawns = new List<WaveSpawn>();
    public int waveAmount;
}


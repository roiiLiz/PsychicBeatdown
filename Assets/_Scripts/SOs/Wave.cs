using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Wave", fileName = "New Wave")]
public class Wave : ScriptableObject
{
    public List<WaveSpawn> spawns = new List<WaveSpawn>();
    public int waveAmount;
    public bool isBoss;
}


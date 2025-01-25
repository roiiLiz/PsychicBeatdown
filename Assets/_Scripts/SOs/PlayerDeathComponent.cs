using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Death Types/Player Death", fileName = "Player Death")]
public class PlayerDeathComponent : DeathComponent
{
    public static event Action OnPlayerDeath;

    public override void Die(MonoBehaviour context)
    {
        throw new System.NotImplementedException();
    }
}

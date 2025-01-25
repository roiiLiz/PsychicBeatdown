using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Death Types/Enemy Death", fileName = "Enemy Death")]
public class EnemyDeathComponent : DeathComponent
{
    public static event Action OnEnemyDeath;

    public override void Die(MonoBehaviour context)
    {
        throw new System.NotImplementedException();
    }
}

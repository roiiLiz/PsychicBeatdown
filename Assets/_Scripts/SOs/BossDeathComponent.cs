using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Death Types/Boss Death", fileName = "Boss Death")]
public class BossDeathComponent : DeathComponent
{
    public static event Action OnBossDeath;

    public override void Die(MonoBehaviour context)
    {
        throw new System.NotImplementedException();
    }
}
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Death Types/Player Death", fileName = "Player Death")]
public class PlayerDeathComponent : DeathComponent
{
    [SerializeField] AudioClip defeatSound;
    [SerializeField] float defeatVolume;
    public static event Action OnPlayerDeath;

    public override void Die(MonoBehaviour context)
    {
        AudioManager.instance.PlaySFX(defeatSound, context.transform, defeatVolume);
        OnPlayerDeath?.Invoke();
    }
}

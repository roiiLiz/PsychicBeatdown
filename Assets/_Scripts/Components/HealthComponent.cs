using System;
using Cinemachine;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] DeathComponent deathComponent;
    [SerializeField] AudioClip hurtSound;
    [SerializeField, Range(0, 1f)] float hurtSoundVolume = 0.1f;

    // this refers only to enemies; the player can get hit multiple times, and therefore doesn't need to have this value read
    public int maxHits { get; set; } = 1;

    int previousHealth;
    int currentHealth;

    public int MaxHealth { get { return maxHealth; } set { SetHealth(value); } }
    public int CurrentHealth { get { return currentHealth; } }

    // Used to create damage numbers in world at the location of damage taken.
    public static event Action<int, MonoBehaviour> OnDamageTaken;
    public static event Action<int, MonoBehaviour> OnHeal;

    void Start()
    {
        previousHealth = currentHealth;
        currentHealth = maxHealth;
    }

    public void SetHealth(int incomingHealth) => maxHealth = incomingHealth;

    public void AddHealth(int incomingHealth)
    {
        previousHealth = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth + incomingHealth, 0, maxHealth);

        if (previousHealth == currentHealth) { return; }

        OnHeal?.Invoke(incomingHealth, this);
    }

    public void Damage(int incomingDamage)
    {
        currentHealth -= incomingDamage;
        OnDamageTaken?.Invoke(incomingDamage, this);

        AudioManager.instance.PlaySFX(hurtSound, transform, hurtSoundVolume);

        CinemachineImpulseSource screenShake = GetComponent<CinemachineImpulseSource>();

        if (screenShake != null && gameObject.CompareTag("Player"))
        {
            ScreenShakeManager.instance.CameraShake(screenShake, incomingDamage);
        }

        if (currentHealth <= 0)
        {
            deathComponent.Die(this);
        }
    }

    public void Die() => deathComponent?.Die(this);
}

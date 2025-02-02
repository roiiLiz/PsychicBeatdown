using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] DeathComponent deathComponent;

    int maxHealth;
    int currentHealth;

    public int MaxHealth { get { return maxHealth; } set { SetHealth(value); } }
    public int CurrentHealth { get { return currentHealth; } }

    // Used to create damage numbers in world at the location of damage taken.
    public static event Action<int, MonoBehaviour> OnDamageTaken;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetHealth(int incomingHealth)
    {
        maxHealth = incomingHealth;
    }

    public void Damage(int incomingDamage)
    {
        currentHealth -= incomingDamage;
        OnDamageTaken?.Invoke(incomingDamage, this);

        if (currentHealth <= 0)
        {
            deathComponent.Die(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ThrownObjects") || collision.gameObject.layer == LayerMask.NameToLayer("HeldObjects"))
        {
            HealthComponent thrownObjectHealth = collision.GetComponent<HealthComponent>();
            if (thrownObjectHealth != null)
            {
                thrownObjectHealth.Damage(thrownObjectHealth.MaxHealth);
                Damage(thrownObjectHealth.MaxHealth);
            }
        }
    }
}

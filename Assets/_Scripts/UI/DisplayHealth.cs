using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] Animator hurtIndicator;
    [SerializeField] Image hurtImage;
    [SerializeField] HealthComponent playerHealth;
    [SerializeField] Sprite healthyImage;
    [SerializeField] Sprite damagedImage;
    [SerializeField] Image[] hearts;

    int currentHealth;

    void OnEnable()
    {
        HealthComponent.OnDamageTaken += SubtractHealth;
        HealthComponent.OnHeal += AddHealth;
    }

    void OnDisable()
    {
        HealthComponent.OnDamageTaken -= SubtractHealth;
        HealthComponent.OnHeal -= AddHealth;
    }

    void Start()
    {
        Color initColor = hurtImage.color;
        initColor.a = 0f;
        hurtImage.color = initColor;

        currentHealth = playerHealth.MaxHealth;
        UpdateUI(true);
    }

    void AddHealth(int healthHealed, MonoBehaviour context)
    {
        Debug.Log("Adding health~!");
        if (context.gameObject.CompareTag("Player"))
        {
            currentHealth = Mathf.Clamp(currentHealth + healthHealed, 0, playerHealth.MaxHealth);
            UpdateUI(true);
        }
    }

    void SubtractHealth(int damageTaken, MonoBehaviour context)
    {
        if (context.gameObject.CompareTag("Player"))
        {
            currentHealth = Mathf.Clamp(currentHealth - damageTaken, 0, playerHealth.MaxHealth);
            UpdateUI(false);
        }
    }

    void UpdateUI(bool noDamageFlash)
    {
        foreach (Image image in hearts)
        {
            image.sprite = damagedImage;
        }

        for (int i = 0; i < currentHealth; i++)
        {
            hearts[i].sprite = healthyImage;
        }

        if (noDamageFlash) { return ; }
        
        hurtIndicator.Play("hurt", -1, 0f);
    }
}

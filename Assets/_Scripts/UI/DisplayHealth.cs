using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] HealthComponent playerHealth;
    [SerializeField] Sprite healthyImage;
    [SerializeField] Sprite damagedImage;
    [SerializeField] Image[] hearts;

    int currentHealth;

    void OnEnable() => HealthComponent.OnDamageTaken += UpdateHealth;
    void OnDisable() => HealthComponent.OnDamageTaken -= UpdateHealth;

    void Start()
    {
        currentHealth = playerHealth.MaxHealth;
        UpdateUI();
    }

    void UpdateHealth(int damageTaken, MonoBehaviour context)
    {
        if (context.gameObject.CompareTag("Player"))
        {
            currentHealth -= damageTaken;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Image image in hearts)
        {
            image.sprite = damagedImage;
        }

        for (int i = 0; i < currentHealth; i++)
        {
            hearts[i].sprite = healthyImage;
            // Debug.Log("hello");
        }
    }
}

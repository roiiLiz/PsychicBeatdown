using System;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    const int MAX_MANA = 100;
    [Tooltip("The amount of mana regenerated over one second")]
    [SerializeField] float manaRegenAmount = 5f;

    float currentMana = 0f;

    public static event Action<float, float> InitUI; 
    public static event Action<float> UpdateUI;

    void Start()
    {
        currentMana = MAX_MANA;

        InitUI?.Invoke(currentMana / MAX_MANA, 1f);
    }
    
    void Update()
    {
        currentMana += manaRegenAmount * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0.0f, MAX_MANA);

        UpdateUI?.Invoke(currentMana / MAX_MANA);
    }

    public void SpendMana(int value)
    {
        if (CanAffordSpell(value))
        {
            currentMana -= value;
        }
    }

    public bool CanAffordSpell(int value) => value <= currentMana;
}
using System;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    const int MAX_MANA = 100;
    [Tooltip("The amount of mana regenerated over one second")]
    [SerializeField] float manaRegenAmount = 5f;

    float currentMana;

    public float CurrentMana { get => currentMana; }
    public float MaxMana { get => MAX_MANA; }

    public static event Action<float, float> InitUI; 
    public static event Action<float> UpdateUI;

    void Start()
    {
        currentMana = MAX_MANA;

        InitUI?.Invoke(currentMana, MAX_MANA);
    }
    
    void Update()
    {
        currentMana += manaRegenAmount * Time.deltaTime;
        Mathf.Clamp(currentMana, 0, MAX_MANA);

        UpdateUI?.Invoke(currentMana);
    }

    public void SpendMana(int value)
    {
        if (value <= currentMana)
        {
            currentMana -= value;
        }
    }

    public bool CanAffordSpell(int value)
    {
        return value >= currentMana;
    }
}
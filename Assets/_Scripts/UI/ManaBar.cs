using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] Slider manaSlider;

    void OnEnable () { ManaManager.UpdateUI += UpdateSlider; ManaManager.InitUI += InitSlider; }
    void OnDisable () { ManaManager.UpdateUI -= UpdateSlider; ManaManager.InitUI -= InitSlider; }

    void InitSlider(float currentValue, float maxValue)
    {
        manaSlider.maxValue = maxValue;
        manaSlider.value = currentValue;
    }

    void UpdateSlider(float value)
    {
        manaSlider.value = value;
    }
}
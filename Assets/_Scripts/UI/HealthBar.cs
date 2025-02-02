using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void InitBar(float currentValue, float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    public void UpdateBar(float currentValue)
    {
        slider.value = currentValue;
    }
}

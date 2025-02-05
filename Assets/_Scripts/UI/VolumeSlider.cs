using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    void OnEnable()
    {
        masterSlider.value = AudioSettings.instance.masterVolume;
        musicSlider.value = AudioSettings.instance.musicVolume;
        sfxSlider.value = AudioSettings.instance.sfxVolume;
    }

}

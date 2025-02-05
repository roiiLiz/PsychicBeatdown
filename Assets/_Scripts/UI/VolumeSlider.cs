using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            LoadVolume();
        } else
        {
            SetVolume();
        }
    }

    void LoadVolume()
    {
        masterSlider.value = AudioSettings.instance.masterVolume;
        musicSlider.value = AudioSettings.instance.musicVolume;
        sfxSlider.value = AudioSettings.instance.sfxVolume;
    }

    void SetVolume()
    {
        float masterVolume = masterSlider.value;
        float musicVolume = musicSlider.value;
        float sfxVolume = sfxSlider.value;

        AudioSettings.instance.masterVolume = masterVolume;
        AudioSettings.instance.musicVolume = musicVolume;
        AudioSettings.instance.sfxVolume = sfxVolume;
    }
}

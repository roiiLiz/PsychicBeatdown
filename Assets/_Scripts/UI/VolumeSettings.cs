using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float defaultVolume = 0.5f;
    [SerializeField] Slider slider;
    [SerializeField] string mixerType;
    [SerializeField] string settingsKey;

    void Start()
    {
        InitOptions();
    }

    public void InitOptions()
    {
        if (PlayerPrefs.HasKey(settingsKey))
        {
            LoadValue();
        }
        else
        {
            SetVolume();
        }
    }

    public void SetVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat(mixerType, Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat(settingsKey, volume);
    }

    void LoadValue()
    {
        slider.value = PlayerPrefs.GetFloat(settingsKey, defaultVolume);
        SetVolume();
    }
}

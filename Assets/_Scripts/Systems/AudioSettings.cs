using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance;

    [SerializeField] float defaultValue = 0.5f;

    public float masterVolume { get; set; }
    public float musicVolume { get; set; }
    public float sfxVolume { get; set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start() => LoadSettings();

    public void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", defaultValue);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", defaultValue);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", defaultValue);

        // Debug.Log($"Loading Settings- \n Master Volume: {masterVolume} \n Music Volume: {musicVolume} \n SFX Volume: {sfxVolume}");
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        PlayerPrefs.Save();

        // Debug.Log($"Saving Settings- \n Master Volume: {masterVolume} \n Music Volume: {musicVolume} \n SFX Volume: {sfxVolume}");
    }
}

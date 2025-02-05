using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        AudioSettings.instance.masterVolume = level;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSFXVolume(float level)
    {
        AudioSettings.instance.masterVolume = level;
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        AudioSettings.instance.masterVolume = level;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }
}

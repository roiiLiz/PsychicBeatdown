using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource SFXPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(AudioClip audioClip, Transform spawnPoint, float volume = 1f)
    {
        AudioSource audioSource = Instantiate(SFXPrefab, spawnPoint.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float soundLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, soundLength);
    }
}

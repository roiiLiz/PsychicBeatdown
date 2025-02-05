using UnityEngine;

public class IntensityManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] float pitchIntensityIncrement = 0.1f;
    [SerializeField] float timeIntensityIncrement = 0.25f;

    void OnEnable() => WaveManager.UpdateLoopCount += IncreaseIntensity;
    void OnDisable() => WaveManager.UpdateLoopCount -= IncreaseIntensity;

    void IncreaseIntensity(int loopCount)
    {
        music.pitch = music.pitch + (pitchIntensityIncrement * loopCount);
        Time.timeScale = Time.timeScale + (timeIntensityIncrement * loopCount);
    }
}
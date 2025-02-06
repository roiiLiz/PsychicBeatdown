using UnityEngine;

public class IntensityManager : MonoBehaviour
{
    [Header("Intensity Indicator Variables")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioClip loopDing;
    [SerializeField] float pitchIntensityIncrement = 0.25f;
    [SerializeField] float timeIntensityIncrement = 0.5f;
    [SerializeField] float defaultPitch, defaultTime = 1f;
    [SerializeField] int healthPerLoop = 1;

    float loopCount;
    GameObject player => GameObject.FindGameObjectWithTag("Player");

    void UpdateLoopCount(int currentLoop) => loopCount = currentLoop;

    void OnEnable()
    {
        WaveManager.UpdateLoopCount += UpdateLoopCount;
        LoopAnimator.LoopIndicate += IncreaseIntensity;
    }

    void OnDisable()
    {
        WaveManager.UpdateLoopCount -= UpdateLoopCount;
        LoopAnimator.LoopIndicate -= IncreaseIntensity;
    }

    void IncreaseIntensity()
    {
        player.GetComponent<HealthComponent>().AddHealth(healthPerLoop);

        music.pitch = defaultPitch + (pitchIntensityIncrement * loopCount);
        Time.timeScale = defaultTime + (timeIntensityIncrement * loopCount);
        
        AudioManager.instance.PlaySFX(loopDing, transform, 1f);
    }
}
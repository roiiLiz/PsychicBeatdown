using System;
using System.Collections;
using UnityEngine;

public class LoopAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CanvasGroup loopSection;
    [SerializeField] float indiciationLength = 0.5f;

    float initLoopTime;
    float loopStartedTime;
    float loopFinishTime;
    bool firstLoop = true;

    public static event Action LoopIndicate;
    public static event Action<bool> LoopInProgress;


    void OnEnable() => WaveManager.OnLoopBegin += TriggerLoopAnimation;
    void OnDisable() => WaveManager.OnLoopBegin -= TriggerLoopAnimation;

    void Start()
    {
        loopSection.alpha = 0.0f;

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "InitialLoop":
                    initLoopTime = clip.length;
                    break;
                case "LoopStarted":
                    loopStartedTime = clip.length;
                    break;
                case "LoopFinished":
                    loopFinishTime = clip.length;
                    break;
                default:
                    break;
            }
        }
    }

    void TriggerLoopAnimation()
    {
        LoopInProgress?.Invoke(true);
        StartCoroutine(StartAnimation(firstLoop));
    }

    IEnumerator StartAnimation(bool isFirstLoop)
    {
        if (isFirstLoop)
        {
            animator.Play("InitialLoop");
            firstLoop = false;
            yield return new WaitForSeconds(initLoopTime);
        } else
        {
            animator.Play("LoopStarted");
            yield return new WaitForSeconds(loopStartedTime);
        }

        LoopIndicate?.Invoke();

        yield return new WaitForSeconds(indiciationLength);

        animator.Play("LoopFinished");

        yield return new WaitForSeconds(loopFinishTime);

        LoopInProgress?.Invoke(false);
    }
}



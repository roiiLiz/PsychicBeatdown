using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource, float forceMultiplier)
    {
        impulseSource.GenerateImpulseWithVelocity(new Vector3(UnityEngine.Random.Range(-1f, 1f) * forceMultiplier, UnityEngine.Random.Range(-1f, 1f) * forceMultiplier, 0f));
    }
}



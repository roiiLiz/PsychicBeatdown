using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CallAudio(string name, float voume, float pitch)
    {

    }
}

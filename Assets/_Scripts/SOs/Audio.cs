using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Audio", fileName = "Audio")]
public class Audio : ScriptableObject
{
    public float masterVolume;
    public float sfxVolume;
    public float musicVolume;
}

using UnityEngine;

public class InitSettings : MonoBehaviour
{
    [SerializeField] VolumeSettings[] volumeSettings;

    void Start()
    {
        foreach (VolumeSettings volume in volumeSettings)
        {
            volume.InitOptions();
        }
    }
}

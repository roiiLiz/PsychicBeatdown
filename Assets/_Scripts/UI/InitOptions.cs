using UnityEngine;

public class InitOptions : MonoBehaviour
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

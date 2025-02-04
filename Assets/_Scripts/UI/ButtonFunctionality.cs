using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour
{
    public void PlayButton(string levelName)
    {
        LevelLoader.instance.LoadLevel(levelName);
    }

    public void ExitButton()
    {
        LevelLoader.instance.QuitGame();
    }

    public void RetryLevel()
    {
        LevelLoader.instance.RetryLevel();
    }
}

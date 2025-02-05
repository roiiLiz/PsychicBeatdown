using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 

    public static event Action LevelRetry;

    public void LoadLevel(string levelName)
    {
        AudioSettings.instance.SaveSettings();
        SceneManager.LoadScene(levelName);
        AudioSettings.instance.LoadSettings();
    }

    public void QuitGame()
    {
        AudioSettings.instance.SaveSettings();
        Debug.Log("Exiting game");
        Application.Quit();
    }

    public void RetryLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);

        LevelRetry?.Invoke();
    }
}

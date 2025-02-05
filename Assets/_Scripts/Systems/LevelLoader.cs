using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 0.5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadScene(levelName));
    }

    public void QuitGame()
    {
        StartCoroutine(LoadScene("Quit"));
    }

    IEnumerator LoadScene(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        if (levelName == "Quit")
        {
            Debug.Log("Exiting game");
            Application.Quit();
        } else
        {
            SceneManager.LoadScene(levelName);

            transition.SetTrigger("EnterLevel");
        }
    }

    public void RetryLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadScene(currentScene));
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PauseManager : MonoBehaviour 
{
    [SerializeField] InputReader input;
    [SerializeField] CanvasGroup pauseCanvas;
    [SerializeField] GameObject initialPauseScreen;
    [SerializeField] GameObject pauseUI;

    public static bool isPaused = false;

    float currentTimeScale = 1f;

    void OnEnable() => input.PauseEvent += TogglePause;
    void OnDisable()
    {
        input.PauseEvent -= TogglePause;
        isPaused = false;
        HandlePause(isPaused);
    }

    void Start()
    {
        InitPause();
    }

    void InitPause()
    {
        isPaused = false;
        HandlePause(isPaused);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        HandlePause(isPaused);
    }

    void HandlePause(bool isPaused)
    {
        if (isPaused)
        {
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = currentTimeScale;
        }

        pauseCanvas.alpha = isPaused ? 1.0f : 0.0f;
        pauseCanvas.interactable = isPaused ? true : false;
        pauseCanvas.blocksRaycasts = isPaused ? true : false;
        initialPauseScreen.SetActive(isPaused);

        if (!isPaused)
        {
            foreach (Transform child in pauseUI.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

    }
}
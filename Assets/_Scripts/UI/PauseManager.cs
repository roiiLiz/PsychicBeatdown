using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PauseManager : MonoBehaviour 
{
    [SerializeField] InputReader input;
    [SerializeField] CanvasGroup pauseCanvas;

    public static bool isPaused = false;

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
        Time.timeScale = isPaused ? 0.0f : 1.0f;
        pauseCanvas.alpha = isPaused ? 1.0f : 0.0f;
        pauseCanvas.interactable = isPaused ? true : false;
        pauseCanvas.blocksRaycasts = isPaused ? true : false;
    }
}
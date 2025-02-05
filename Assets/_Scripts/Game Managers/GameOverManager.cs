using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] CanvasGroup gameOverCanvas;
    [SerializeField] AudioSource music;
    [SerializeField] float defeatMusicPitch;
    [SerializeField] AnimationCurve lerpCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] float lerpRate = 1f;

    bool allowGameOver = true;

    void OnEnable () => PlayerDeathComponent.OnPlayerDeath += HandleGameOver;
    void OnDisable () => PlayerDeathComponent.OnPlayerDeath -= HandleGameOver;

    void Start()
    {
        gameOverCanvas.alpha = 0.0f;
        gameOverCanvas.interactable = false;
        gameOverCanvas.blocksRaycasts = false;
    }

    void HandleGameOver()
    {
        if (allowGameOver)
        {
            allowGameOver = !allowGameOver;
            gameOverCanvas.interactable = true;
            gameOverCanvas.blocksRaycasts = true;

            StartCoroutine(LerpAlpha(gameOverCanvas.alpha, 1f));
            StartCoroutine(LerpTime(Time.timeScale, 0f));
            StartCoroutine(LerpPitch(music.pitch, defeatMusicPitch));
        }
    }

    IEnumerator LerpAlpha(float from, float to)
    {
        float t = 0f;
        float rate = 1.0f / lerpRate;

        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime * rate;
            gameOverCanvas.alpha = Mathf.Lerp(from, to, lerpCurve.Evaluate(t));
            yield return null;
        }
    }

    IEnumerator LerpTime(float from, float to)
    {
        float t = 0f;
        float rate = 1.0f / lerpRate;

        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime * rate;
            Time.timeScale = Mathf.Lerp(from, to, lerpCurve.Evaluate(t));
            yield return null;
        }
    }

    IEnumerator LerpPitch(float from, float to)
    {
        float t = 0f;
        float rate = 1.0f / lerpRate;

        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime * rate;
            music.pitch = Mathf.Lerp(from, to, lerpCurve.Evaluate(t));
            yield return null;
        }
    }

    [ContextMenu("Start Game Over")]
    void GameOver()
    {
        HandleGameOver();
    }
}

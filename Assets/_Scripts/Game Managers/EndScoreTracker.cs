using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScoreTracker : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] TextMeshProUGUI endScoreText, playScoreText, loopText;
    [Header("Animation Variables")]
    [SerializeField] AnimationCurve lerpCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] float lerpRate = 0.5f;

    int currentScore;
    int displayScore;

    void OnEnable()
    {
        EnemyDeathComponent.OnEnemyDeath += IncrementScore;
        WaveManager.UpdateLoopCount += UpdateLoopText;
        ComboManager.ComboScore += AddComboScore;
    }

    void OnDisable()
    {
        EnemyDeathComponent.OnEnemyDeath -= IncrementScore;
        WaveManager.UpdateLoopCount += UpdateLoopText;
        ComboManager.ComboScore += AddComboScore;
    }

    void IncrementScore() => currentScore++;
    void AddComboScore(int incomingScore) => currentScore += incomingScore;

    void Start()
    {
        currentScore = 0;
        displayScore = 0;

        StartCoroutine(UpdateScore());

        UpdateScoreText();
        UpdateLoopText(0);
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            if (displayScore < currentScore)
            {
                float t = 0f;
                float rate = 1f / lerpRate;

                while (t < 1f)
                {
                    t += Time.deltaTime * rate;
                    displayScore = (int) Mathf.Lerp(displayScore, currentScore, lerpCurve.Evaluate(t));
                    playScoreText.text = $"{currentScore}";
                }
            }
        }
    }

    void UpdateLoopText(int loopCount)
    {
        if (loopCount == 0)
        {
            loopText.text = $"";
        } else
        {
            loopText.text = $"You made it to loop {loopCount}!";
        }
    }

    void UpdateScoreText() => endScoreText.text = $"You earned {currentScore} points!";
}
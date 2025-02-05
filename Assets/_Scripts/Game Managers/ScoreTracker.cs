using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI loopText;

    float currentScore;

    void OnEnable()
    {
        EnemyDeathComponent.OnEnemyDeath += IncrementScore;
        WaveManager.UpdateLoopCount += UpdateLoopText;
    }

    void OnDisable()
    {
        EnemyDeathComponent.OnEnemyDeath -= IncrementScore;
        WaveManager.UpdateLoopCount += UpdateLoopText;
    }

    void Start()
    {
        currentScore = 0;

        UpdateLoopText(0);
        UpdateScoreText();
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

    void IncrementScore()
    {
        currentScore += 1;
        UpdateScoreText();
    }

    void UpdateScoreText() => scoreText.text = $"You earned {currentScore} points!";
}

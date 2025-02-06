using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScoreTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endScoreText, playScoreText, loopText;
    [SerializeField] GameObject highScoreText;

    int currentScore;

    void OnEnable()
    {
        PlayerDeathComponent.OnPlayerDeath += SavePlayerScore;
        EnemyDeathComponent.OnEnemyDeath += IncrementScore;
        WaveManager.UpdateLoopCount += UpdateLoopText;
        ComboManager.ComboScore += AddComboScore;
    }

    void OnDisable()
    {
        PlayerDeathComponent.OnPlayerDeath -= SavePlayerScore;
        EnemyDeathComponent.OnEnemyDeath -= IncrementScore;
        WaveManager.UpdateLoopCount -= UpdateLoopText;
        ComboManager.ComboScore -= AddComboScore;
    }

    void IncrementScore()
    {
        currentScore++;
        UpdateScoreText();
    }

    void AddComboScore(int incomingScore)
    {
        currentScore += incomingScore;
        UpdateScoreText();
    }

    void Start()
    {
        currentScore = 0;

        UpdateScoreText();
        UpdateLoopText(0);
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

    void UpdateScoreText()
    {
        playScoreText.text = $"{currentScore}";
        endScoreText.text = $"You earned {currentScore} points!";
    }

    void SavePlayerScore()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < currentScore)
        {
            highScoreText.SetActive(true);
            PlayerPrefs.SetInt("HighScore", currentScore);
        } else
        {
            highScoreText.SetActive(false);
        }
    }
}
using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    [SerializeField] GameObject highScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float maxRandomTilt = 10f;

    int highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScore <= 0)
        {
            highScoreText.SetActive(false);
            scoreText.text = "";
        } else
        {
            highScoreText.SetActive(true);
            scoreText.gameObject.transform.Rotate(0, 0, UnityEngine.Random.Range(-maxRandomTilt, maxRandomTilt));
            scoreText.text = $"{highScore}";
        }
    }
}

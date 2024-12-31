using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI HighScoreText;

    GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindFirstObjectByType<GameController>();

        if( gameController != null)
        {
            PlayButton.OnButtonPressed += UpdateScore;
        }
    }

    private void OnDestroy()
    {
        PlayButton.OnButtonPressed -= UpdateScore;
    }

    private void UpdateScore() {
        if (gameController != null)
        {
            scoreText.text = gameController.score.ToString();
            CheckHighScore();
        }
    }

    void CheckHighScore()
    {
        if (gameController.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", gameController.score);
            PlayerPrefs.Save();
            HighScoreText.gameObject.SetActive(true);
        }
    }
}

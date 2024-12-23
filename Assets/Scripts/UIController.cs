using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

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
        }
    }
}

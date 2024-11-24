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
    }
    public void UpdateScore() { 
        scoreText.text = gameController.score.ToString();
    }
}

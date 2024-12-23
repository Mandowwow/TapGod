using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score { get; private set; }

    private void OnEnable()
    {
        PlayButton.OnButtonPressed += IncrementScore;
    }

    private void OnDisable()
    {
        PlayButton.OnButtonPressed -= IncrementScore;
    }

    public void IncrementScore()
    {
        score++;
    }
}

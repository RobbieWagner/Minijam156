using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using RobbieWagnerGames.Plinko;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText.text = $"Highscore: {StaticGameStats.HighScore}";
        StaticGameStats.OnHighScoreChanged += UpdateText;   
    }

    private void UpdateText(int newValue)
    {
        scoreText.text = $"Highscore: {StaticGameStats.HighScore}";
    }
}

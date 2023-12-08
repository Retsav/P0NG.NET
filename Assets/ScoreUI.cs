using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreBar oppositeScoreBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (oppositeScoreBar != null)
        {
            oppositeScoreBar.GetBarScore().OnValueChanged += ChangeTextToScore;
        }
        else
        {
            Debug.LogError("ScoreBar in ScoreUI is null!");
        }
    }

    private void ChangeTextToScore(int previousvalue, int newvalue)
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText in scoreUI is null!");
            return;
        }
        scoreText.text = newvalue.ToString();
    }
}

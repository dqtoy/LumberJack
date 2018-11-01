using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreUpdateText : MonoBehaviour
{

    TextMeshProUGUI scoreText;


    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = GameSession.CurrentScore.ToString();
    }

    public void UpdateScore()
    {
        scoreText.text = GameSession.CurrentScore.ToString();
    }
}

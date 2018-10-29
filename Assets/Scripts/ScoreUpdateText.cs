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
        scoreText.text = FindObjectOfType<GameSession>().GetCurrentScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore()
    {
        scoreText.text = FindObjectOfType<GameSession>().GetCurrentScore().ToString();
    }
}

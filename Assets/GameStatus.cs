using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // configuration parameters
    [Range(0, 2)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestoryed = 10;
    TextMeshProUGUI scoreText;


    // state variable
    [SerializeField]  int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        scoreText = Transform.FindObjectOfType<TextMeshProUGUI>();
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;            // 1f - regular time scale
    }

    public void AddPointsToScore()
    {
        currentScore += pointsPerBlockDestoryed;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}

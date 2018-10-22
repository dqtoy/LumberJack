using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    // configuration parameters
    [Range(0, 2)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestoryed = 10;
    [SerializeField] TextMeshProUGUI scoreText;


    // state variable
    [SerializeField]  int currentScore = 0;

    private static GameStatus gameStatus = null;

    private void Awake()
    {
        if (gameStatus == null)
        {
            gameStatus = this;
        }
        else if (gameStatus != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
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

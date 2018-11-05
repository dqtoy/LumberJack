using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] int breakableBlocks;
    ScoreUpdateText scoreText;
    [SerializeField] GameObject gameCanvas;

    private void Start()
    {
        scoreText = FindObjectOfType<ScoreUpdateText>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
        scoreText.UpdateScore(breakableBlocks);
    }

    public void DestoryedBreakableBlock()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            CheckBlocksInPlay();
            LevelClear();
        }
        scoreText.UpdateScore(breakableBlocks);
    }

    private void CheckBlocksInPlay()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        if (blocks.Length - 1 != breakableBlocks)
        {
            breakableBlocks = blocks.Length;
        }
    }

    private void LevelClear()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (var ball in balls)
        {
            ball.FreezBall();
        }
        gameCanvas.GetComponent<Animator>().SetTrigger("stageClear");
        IfWinUnlockNextLevel();
    }

    private void IfWinUnlockNextLevel()
    {
        int numberOfCurrentLevel = SceneManager.GetActiveScene().buildIndex;
        if (numberOfCurrentLevel < SceneManager.sceneCountInBuildSettings - 1)
        {
            int nextLevel = numberOfCurrentLevel + 1;
            PlayerPreferenceController.UnlockLevelNumber(nextLevel);
        }
    }

    public void PauseMenu()
    {
        gameCanvas.GetComponent<Animator>().SetBool("isPaused", true);
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (var ball in balls)
        {
            ball.FreezBall();
        }
    }

    public void UnPauseMenu()
    {
        gameCanvas.GetComponent<Animator>().SetBool("isPaused", false);
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (var ball in balls)
        {
            ball.UnFreezeBall();
        }
    }

}

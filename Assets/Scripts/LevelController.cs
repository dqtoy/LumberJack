using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        CheckBlocksInPlay();
        scoreText.UpdateScore(breakableBlocks);
        if (breakableBlocks <= 0)
        {
            LevelClear();
        }
    }

    private void CheckBlocksInPlay()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        if (blocks.Length != breakableBlocks)
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

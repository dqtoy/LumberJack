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
    }

    public void DestoryedBreakableBlock()
    {
        breakableBlocks--;
        scoreText.UpdateScore();
        if (breakableBlocks <= 0)
        {
            LevelClear();
        }
    }

    private void LevelClear()
    {
        FindObjectOfType<Ball>().FreezBall();
        gameCanvas.GetComponent<Animator>().SetTrigger("stageClear");
    }

    public void PauseMenu()
    {
        gameCanvas.GetComponent<Animator>().SetBool("isPaused", true);
        FindObjectOfType<Ball>().FreezBall();
    }

    public void UnPauseMenu()
    {
        gameCanvas.GetComponent<Animator>().SetBool("isPaused", false);
        FindObjectOfType<Ball>().UnFreezeBall();
    }

}

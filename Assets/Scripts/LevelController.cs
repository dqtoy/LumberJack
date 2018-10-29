using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] int breakableBlocks;       // for debugging purposes

    //cached reference
    ScoreUpdateText scoreText;
    [SerializeField] GameObject gameCanvas;

    private void Start()
    {
        scoreText = FindObjectOfType<ScoreUpdateText>();
    }

    public void countBreakableBlocks()          // use this public function to automatyccly count block by calling method in Block.Start()
    {
        breakableBlocks++;
    }

    public void destroyedBreakableBlock()
    {
        breakableBlocks--;
        scoreText.UpdateScore();

        LevelClear();

    }

    private void LevelClear()
    {
        if (breakableBlocks <= 0)
        {
            FindObjectOfType<Ball>().FreezBall();
            gameCanvas.GetComponent<Animator>().SetTrigger("stageClear");
        }
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

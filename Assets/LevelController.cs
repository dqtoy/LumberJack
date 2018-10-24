using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] int breakableBlocks;       // for debugging purposes

    //cached reference
    SceneLoader sceneLoader;
    ScoreUpdateText scoreText;
    [SerializeField] GameObject gameCanvas;

    private void Start()
    {
        scoreText = FindObjectOfType<ScoreUpdateText>();
        sceneLoader = FindObjectOfType<SceneLoader>();
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
}

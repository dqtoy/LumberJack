using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField] int breakableBlocks;       // for debugging purposes

    //cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void countBreakableBlocks()          // use this public function to automatyccly count block by calling method in Block.Start()
    {
        breakableBlocks++;
    }

    public void destroyedBreakableBlock()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}

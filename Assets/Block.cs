using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] AudioClip destoryBlockSound;
    [Range(0, 1)] [SerializeField] float volumeLevel = 1f;

    LevelController levelController;
    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        levelController = FindObjectOfType<LevelController>();
        levelController.countBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();
        gameStatus.AddPointsToScore();
    }

    private void DestroyBlock()
    {
        levelController.destroyedBreakableBlock();
        AudioSource.PlayClipAtPoint(destoryBlockSound, Camera.main.transform.position, volumeLevel);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // audio elements
    [SerializeField] AudioClip destoryBlockSound;
    [Range(0, 1)] [SerializeField] float volumeLevel = 1f;
    // visual efect elements
    [SerializeField] GameObject blockVFX;
    [SerializeField] float vfxLiveTime = 2f;

    // damage processing - block dmg, what sprite etc.
    [SerializeField] int maxHitsForBlock = 1;
    [SerializeField] int timesHitAlready = 0; // serialized for debug


    // backend elements - scores, counting etc.
    LevelController levelController;
    GameSession gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
        levelController = FindObjectOfType<LevelController>();
        levelController.countBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpawnVFX();
        timesHitAlready++;
        HandleHit();

    }

    private void HandleHit()
    {
        if (timesHitAlready >= maxHitsForBlock)
        {
            AudioSource.PlayClipAtPoint(destoryBlockSound, Camera.main.transform.position, volumeLevel);
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        levelController.destroyedBreakableBlock();
        Destroy(gameObject);
        gameStatus.AddPointsToScore();
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxLiveTime);
    }
}

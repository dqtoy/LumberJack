using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // visual efect elements
    [SerializeField] GameObject blockVFX;
    [SerializeField] float vfxLiveTime = 2f;

    // damage processing - block dmg, what sprite etc.
    [SerializeField] int timesHitAlready = 0; // serialized for debug
    [SerializeField] Sprite[] hitSprites;

    //spawning powerUps
    [SerializeField] GameObject[] powerUps = new GameObject[1];
    [Range(0, 10)] [SerializeField] int tresholdForPowerUpSpawn = 7;


    // backend elements - scores, counting etc.
    [SerializeField] int pointsPerBlockDestoryed = 10;
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.countBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpawnVFX();
        if (FindObjectOfType<Ball>().GetIsPowerUpChainsawActive())
        {
            DestroyBlock();
        }
        else
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHitAlready++;
        int maxHitsForBlock = hitSprites.Length;
        if (timesHitAlready >= maxHitsForBlock)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        if (hitSprites[timesHitAlready] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHitAlready];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array!" + gameObject);
        }
    }

    private void DestroyBlock()
    {
        FindObjectOfType<BallAudio>().PlayDestroyBlock();
        FindObjectOfType<GameSession>().AddPointsToScore(pointsPerBlockDestoryed);
        SpawnPowerUp();
        levelController.destroyedBreakableBlock();
        Destroy(gameObject);
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxLiveTime);
    }

    private void SpawnPowerUp()
    {
        int chance = UnityEngine.Random.Range(0, 11);
        Debug.Log(chance);
        int index = UnityEngine.Random.Range(0, powerUps.Length);
        if (chance >= tresholdForPowerUpSpawn)
        {
            Instantiate(powerUps[index], transform.position, transform.rotation);
        }
    }
}

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


    // backend elements - scores, counting etc.
    [SerializeField] int pointsPerBlockDestoryed = 10;
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpawnVFX();

        if (collision.gameObject.GetComponent<Ball>().IsThisBallWithChainsaw)
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
        levelController.DestoryedBreakableBlock();
        FindObjectOfType<BallAudio>().PlayDestroyBlock();
        GameSession.CurrentScore += pointsPerBlockDestoryed;
        FindObjectOfType<PowerUpHandler>().SpawnPowerUp(transform.position);
        Destroy(gameObject);
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxLiveTime);
    }
}

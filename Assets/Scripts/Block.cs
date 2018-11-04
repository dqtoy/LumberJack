using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject blockVFX;
    [SerializeField] float vfxLiveTime;

    [SerializeField] int timesHitAlready;
    [SerializeField] Sprite[] hitSprites;

    [SerializeField] int pointsPerBlockDestoryed;
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpawnVFX();
        if (FindObjectOfType<Ball>().IsThisBallWithChainsaw)
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
        InfromOtherScriptsBeforDestroing();
        FindObjectOfType<SFXController>().PlayDestroyBlock();
        Destroy(gameObject);
    }

    private void InfromOtherScriptsBeforDestroing()
    {
        levelController.DestoryedBreakableBlock();
        FindObjectOfType<PowerUpHandler>().SpawnPowerUp(transform.position);
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxLiveTime);
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject blockVFX;
    [SerializeField] float vfxLiveTime;

    [SerializeField] int timesHitAlready;
    [SerializeField] Sprite[] hitSprites;

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
        SubstractBlocksInGameAndAddPoints();
        TryToSpawnPowerUp();
        PlaySFXForDestructionOfBlock();
        Destroy(gameObject);
    }

    private void TryToSpawnPowerUp()
    {
        FindObjectOfType<PowerUpHandler>().SpawnPowerUp(transform.position);
    }

    private static void PlaySFXForDestructionOfBlock()
    {
        FindObjectOfType<BallAudio>().PlayDestroyBlock();
    }

    private void SubstractBlocksInGameAndAddPoints()
    {
        levelController.DestoryedBreakableBlock();
        GameSession.CurrentScore += pointsPerBlockDestoryed;
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxLiveTime);
    }
}

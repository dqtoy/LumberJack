using System;
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
    [SerializeField] int timesHitAlready = 0; // serialized for debug
    [SerializeField] Sprite[] hitSprites;


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

        HandleHit();

    }

    private void HandleHit()
    {
        timesHitAlready++;
        int maxHitsForBlock = hitSprites.Length;
        if (timesHitAlready >= maxHitsForBlock)
        {
            AudioSource.PlayClipAtPoint(destoryBlockSound, Camera.main.transform.position, volumeLevel);
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
        FindObjectOfType<GameSession>().AddPointsToScore(pointsPerBlockDestoryed);
        levelController.destroyedBreakableBlock();
        Destroy(gameObject);
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxLiveTime);
    }
}

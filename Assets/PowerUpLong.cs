﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLong : MonoBehaviour
{
    [SerializeField] GameObject VFXforPickUp;
    [SerializeField] float vfxLifeTime = 1f;

    [SerializeField] Vector3 lenghtMultipier = new Vector3(0.5f, 0, 0);
    [SerializeField] float timeOfPowerUp = 5f;

    [SerializeField] float maxLifeTime = 30f;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            StartCoroutine (PickUp(collision));
        }
       
    }

    IEnumerator PickUp(Collider2D player)
    {
        // spawn VFX at pickup
        PlayEffects();

        // Apply effect
        player.transform.localScale += lenghtMultipier;

        //turn off rendering and collider
        DisabeVisualOfPowerUp();

        // wait x time as coroutine
        yield return new WaitForSeconds(timeOfPowerUp);

        // revers
        player.transform.localScale -= lenghtMultipier;

        //remove object
        Destroy(gameObject);
    }

    private void DisabeVisualOfPowerUp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void PlayEffects()
    {
        GameObject vfx = Instantiate(VFXforPickUp, transform.position, transform.rotation);
        Destroy(vfx, vfxLifeTime);
    }
}

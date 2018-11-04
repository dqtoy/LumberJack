﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLife : MonoBehaviour
{
    [SerializeField] GameObject VFXforPickUp;
    [SerializeField] float vfxLifeTime;
    [SerializeField] float maxLifeTime;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            PlayEffects();
            FindObjectOfType<PowerUpHandler>().PowerUpLife();
            Destroy(gameObject, vfxLifeTime);
        }

    }

    private void PlayEffects()
    {
        GameObject vfx = Instantiate(VFXforPickUp, transform.position, transform.rotation);
        Destroy(vfx, vfxLifeTime);
    }
}
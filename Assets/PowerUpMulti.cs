using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMulti : MonoBehaviour
{

    [SerializeField] GameObject VFXforPickUp;
    [SerializeField] float vfxLifeTime = 1f;
    [SerializeField] float timeOfPowerUp = 30f;
    [SerializeField] float maxLifeTime = 30f;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            PickUp(collision);
        }

    }

    public void PickUp(Collider2D player)
    {
        // spawn VFX at pickup
        PlayEffects();

        // Apply effect
        if (FindObjectOfType<LoseCollider>().IsMultiPowerUp())
        {
            DisabeVisualOfPowerUp();
            return;
        }
        else
        {
            FindObjectOfType<BallMulti>().Clone();
            FindObjectOfType<LoseCollider>().SetIsMultiPowerUp(true);
        }
        //turn off rendering and collider
        DisabeVisualOfPowerUp();

        // wait x time as coroutine

        // revers
        //remove object
    }

    private void DisabeVisualOfPowerUp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void PlayEffects()
    {
        GetComponent<AudioSource>().Play();
        GameObject vfx = Instantiate(VFXforPickUp, transform.position, transform.rotation);
        Destroy(vfx, vfxLifeTime);
    }
}


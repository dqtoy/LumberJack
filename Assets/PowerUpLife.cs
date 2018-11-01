using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLife : MonoBehaviour
{
    [SerializeField] GameObject VFXforPickUp;
    [SerializeField] float vfxLifeTime = 1f;
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
            StartCoroutine(PickUp(collision));
        }

    }

    IEnumerator PickUp(Collider2D player)
    {
        // spawn VFX at pickup
        PlayEffects();

        // Apply effect
        FindObjectOfType<GameSession>().AddLifePoint();
        //turn off rendering and collider
        DisabeVisualOfPowerUp();

        // wait x time as coroutine
        yield return new WaitForSeconds(timeOfPowerUp);

        // revers

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
        GetComponent<AudioSource>().Play();
        GameObject vfx = Instantiate(VFXforPickUp, transform.position, transform.rotation);
        Destroy(vfx, vfxLifeTime);
    }
}
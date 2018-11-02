using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpExplosion : MonoBehaviour
{

    [SerializeField] GameObject VFXforPickUp;
    [SerializeField] float vfxLifeTime = 1f;
    [SerializeField] float maxLifeTime = 30f;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            PickUp();
        }

    }

    private void PickUp()
    {
        // spawn VFX at pickup
        PlayEffects();
        // Apply effect
        if (FindObjectOfType<DestroyStone>() == null) { return; }

        FindObjectOfType<DestroyStone>().GetComponent<Animator>().SetTrigger("DestroyStone");
        //turn off rendering and collider

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

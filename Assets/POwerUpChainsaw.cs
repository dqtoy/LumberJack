using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POwerUpChainsaw : MonoBehaviour
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
            FindObjectOfType<PowerUpHandler>().ActiveChainsawInHandler(collision);
            Destroy(gameObject);
        }
    }

    private void PlayEffects()
    {
        if (VFXforPickUp == null) { return; }
        GameObject vfx = Instantiate(VFXforPickUp, transform.position, transform.rotation);
        Destroy(vfx, vfxLifeTime);
    }
}

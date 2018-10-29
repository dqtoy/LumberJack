using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSound : MonoBehaviour {
    [SerializeField] AudioClip stoneClip;
    [SerializeField] float sfxLevel = 0.5f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(stoneClip, Camera.main.transform.position, sfxLevel);
    }

}

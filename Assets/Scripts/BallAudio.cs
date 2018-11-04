using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{

    [SerializeField] AudioClip[] crackSound;
    [Range(0, 1)] [SerializeField] float crackVolume;
    [SerializeField] AudioClip blockDestory;
    [Range(0, 1)] [SerializeField] float destoryVolume;
    [SerializeField] AudioClip bounceSound;
    [Range(0, 1)] [SerializeField] float bounceVolume;
    [SerializeField] AudioClip stoneSound;
    [Range(0, 1)] [SerializeField] float stoneVolume;
    [SerializeField] AudioClip chainsawSound;
    [Range(0, 1)] [SerializeField] float chainsawVolume;


    AudioSource ballAudio;

    void Start()
    {
        ballAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Brick")
        {
            int index = Random.Range(0, crackSound.Length);
            ballAudio.PlayOneShot(crackSound[index], crackVolume);
        }
        else if (collision.gameObject.tag == "Stone")
        {
            ballAudio.PlayOneShot(stoneSound, stoneVolume);
        }
        else
        {
            ballAudio.PlayOneShot(bounceSound, bounceVolume);
        }
    }

    //public void PlayChainsawAudio()
    //{
    //    ballAudio.PlayOneShot(chainsawSound, chainsawVolume);
    //}

    //public void PlayDestroyBlock()
    //{
    //    ballAudio.PlayOneShot(blockDestory, destoryVolume);
    //}

}

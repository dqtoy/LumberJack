using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{

    [SerializeField] AudioClip[] crackSound = new AudioClip[4];
    [Range(0, 1)] [SerializeField] float crackVolume = 1f;
    [SerializeField] AudioClip blockDestory;
    [Range(0, 1)] [SerializeField] float destoryVolume = 1f;
    [SerializeField] AudioClip bounceSound;
    [Range(0, 1)] [SerializeField] float bounceVolume = 0.3f;
    [SerializeField] AudioClip stoneSound;
    [Range(0, 1)] [SerializeField] float stoneVolume = 0.3f;

    AudioSource ballAudio;


    // Use this for initialization
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
        else if(collision.gameObject.tag == "Stone")
        {
            ballAudio.PlayOneShot(stoneSound, stoneVolume);
        }
        else
        {
            ballAudio.PlayOneShot(bounceSound, bounceVolume);
        }
    }

    public void PlayDestroyBlock()
    {
        ballAudio.PlayOneShot(blockDestory, destoryVolume);
    }

}

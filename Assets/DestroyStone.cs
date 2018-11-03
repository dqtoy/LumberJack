using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStone : MonoBehaviour {

    [SerializeField] AudioClip explosion;
    [Range(0, 1)] [SerializeField] float volume = 1f;

	public void Explosion()
    {
        if(explosion == null) { return; }
        GetComponent<AudioSource>().PlayOneShot(explosion, volume);
        Destroy(gameObject, explosion.length);
    }
}

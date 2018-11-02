using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{

    //spawning powerUps
    [SerializeField] AudioClip sfxForPowerUps;
    [Range(0, 1)] [SerializeField] float sfxVolume;
    [SerializeField] GameObject[] powerUps = new GameObject[1];
    [Range(0, 10)] [SerializeField] int tresholdForPowerUpSpawn = 7;


    public void SpawnPowerUp(Vector3 position)
    {
        int chance = UnityEngine.Random.Range(0, 11);
        int index = UnityEngine.Random.Range(0, powerUps.Length);
        if (chance >= tresholdForPowerUpSpawn)
        {
            Instantiate(powerUps[index], position, transform.rotation);
            GetComponent<AudioSource>().PlayOneShot(sfxForPowerUps, sfxVolume);
        }
    }
}

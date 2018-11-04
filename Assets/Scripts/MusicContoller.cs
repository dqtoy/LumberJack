using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicContoller : MonoBehaviour {

    [SerializeField] AudioMixer audioMixer;
    // singleton patter implementation
    private static MusicContoller gameStatus = null;

    private void Awake()
    {
        if (gameStatus == null)
        {
            gameStatus = this;
        }
        else if (gameStatus != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start () {
        audioMixer.SetFloat("musicLevel", PlayerPreferenceController.GetMusicVolume());
        audioMixer.SetFloat("SFXLevel", PlayerPreferenceController.GetSFXVolume());
	}
}

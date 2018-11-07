using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeIn : MonoBehaviour {

    Image image;

    [SerializeField] float timeToFadeIn = 2f;
    Color currentColor = Color.black;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad < timeToFadeIn)
        {
            float alphaChange = Time.deltaTime / timeToFadeIn;
            currentColor.a -= alphaChange;
            image.color = currentColor;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

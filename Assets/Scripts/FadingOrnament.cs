using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingOrnament : MonoBehaviour
{

    Image image;

    [SerializeField] float timeToFadeIn = 2f;
    Color currentColor = Color.white;


    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        currentColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        FadeIn();
    }

    private void FadeIn()
    {
        if (Time.timeSinceLevelLoad > 1.5f)
        {
            float alphaChange = Time.deltaTime / timeToFadeIn;
            currentColor.a += alphaChange;
            image.color = currentColor; 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour {

    [SerializeField] float screenWidthInUnits = 16;
    [SerializeField] Slider moveSlider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;        // creating float for actual mouse position
        float sliderInWorldUnits = moveSlider.value * screenWidthInUnits;
        Vector2 paddlePos = new Vector2(sliderInWorldUnits, transform.position.y);
        transform.position = paddlePos;
	}
}

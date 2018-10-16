using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float min = 0.2f;
    [SerializeField] float max = 6f;
    [SerializeField] float screenWidthInUnits = 6.25f;
    [SerializeField] Slider moveSlider;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;        // creating float for actual mouse position
        float sliderInWorldUnits = moveSlider.value * screenWidthInUnits;
        Vector2 paddlePos = new Vector2(sliderInWorldUnits, transform.position.y);
        paddlePos.x = Mathf.Clamp(sliderInWorldUnits, min, max);
        transform.position = paddlePos;
    }
}

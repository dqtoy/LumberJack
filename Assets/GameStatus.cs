using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [Range(0,2)][SerializeField] float gameSpeed = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;            // 1f - regular time scale
    }
}

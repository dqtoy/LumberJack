using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour {

    SceneLoader sceneLoader;
    [SerializeField] GameObject gameCanvas;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<Ball>().FreezBall();
        gameCanvas.GetComponent<Animator>().SetTrigger("looseLevel");
    }

}

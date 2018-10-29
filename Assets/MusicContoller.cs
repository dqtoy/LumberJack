using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicContoller : MonoBehaviour {

    
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
            DestroyImmediate(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour {

    SceneChanging sceneChanger;

	// Use this for initialization
	void Start () {
        sceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChanging>();
        sceneChanger.pauseMenuRestart = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

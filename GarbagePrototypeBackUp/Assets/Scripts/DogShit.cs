using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogShit : MonoBehaviour {

	float t;
	float gameStartTime;
	// Use this for initialization
	void Start () {
		gameStartTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.timeSinceLevelLoad - gameStartTime;
		if (t > 5) {
			
			Destroy (gameObject);
		}
	}
}

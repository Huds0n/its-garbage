using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LeanTween.moveY(gameObject, gameObject.transform.position.y - 20, 0.5f).setLoopPingPong();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

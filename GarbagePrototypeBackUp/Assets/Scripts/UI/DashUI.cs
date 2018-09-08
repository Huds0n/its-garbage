using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LeanTween.scale (gameObject, new Vector3(gameObject.transform.localScale.x + .02f,gameObject.transform.localScale.y + .02f, gameObject.transform.localScale.z + .02f), 1f).setLoopPingPong ();
	}
}

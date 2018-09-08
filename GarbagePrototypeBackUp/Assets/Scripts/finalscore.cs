using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalscore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, gameObject.transform.position.z), 1f).setLoopPingPong();
    }
}

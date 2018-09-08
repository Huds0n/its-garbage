using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArrow : MonoBehaviour {

	void Start () {
        LeanTween.moveY(gameObject, gameObject.transform.position.y - 20, 0.5f).setLoopPingPong();
	}
}

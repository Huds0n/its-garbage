using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

	public float turnSpeed = 2f;

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,turnSpeed * Time.deltaTime,0));
	}
}

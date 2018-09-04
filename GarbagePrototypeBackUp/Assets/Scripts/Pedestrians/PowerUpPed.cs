using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPed : Pedestrian {

	bool movement;
	public float waitTime = 15f;

	// Use this for initialization
	void Start () {
		PedestrianRefsValues ();
		speed = 1.5f;
		StartCoroutine ("WaitTime");
	}

	void Update(){
			PedestrianFacingWay ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (movement) {
			PedestrianMovement ();
		}
	}

	IEnumerator WaitTime(){
		yield return new WaitForSeconds (waitTime);
		movement = true;
		yield return new WaitForEndOfFrame ();
	}

	void OnCollisionEnter(Collision other)
	{
		///Ped Killer Wall
		if (other.gameObject.layer == 13)
		{
			gameObject.SetActive(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadHitPed : Pedestrian, IPooledObject {

    // Use this for initialization
    public void OnObjectSpawn() {
        PedestrianRefsValues();

		MaterialChange ();
	}

    public void Update()
    {
        PedestrianSpeeds();

        PedestrianFacingWay();
    }
   
    // Update is called once per frame
    public void FixedUpdate () {
        PedestrianMovement();
    }

    void OnCollisionEnter(Collision other)
    {
        ///Pedestrian Killer Wall
        if (other.gameObject.layer == 13)
        {
            gameObject.SetActive(false);
        }
    }
}

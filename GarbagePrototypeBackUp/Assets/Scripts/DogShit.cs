using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogShit : MonoBehaviour {

	float t;
	float gameStartTime;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
		gameStartTime = Time.timeSinceLevelLoad;

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.timeSinceLevelLoad - gameStartTime;
		if (t > 5) {
            rb.useGravity = false;
            rb.isKinematic = true;
            StartCoroutine(GoAway());
			//Destroy (gameObject);
		}
	}

    IEnumerator GoAway()
    {
        LeanTween.moveY(gameObject, gameObject.transform.position.y - 1, 2f);

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

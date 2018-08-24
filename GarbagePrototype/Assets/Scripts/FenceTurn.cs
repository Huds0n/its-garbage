using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceTurn : MonoBehaviour {

	BoxCollider fenceCollider;
    
	bool rotateOtherWay;

	void Start(){
		fenceCollider = GetComponent<BoxCollider> ();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Dog")
        {
            Turn();
			StartCoroutine (TurnColliderOffandOn());
        }
    }

    void Turn()
    {
        if (!rotateOtherWay)
        {
            LeanTween.rotate(this.gameObject, new Vector3(0, 0, -180f), .5f).setLoopPingPong(1);
            rotateOtherWay = true;
        }
        else
        {
            LeanTween.rotate(this.gameObject, new Vector3(0, 0, 180f), .5f).setLoopPingPong(1);
            rotateOtherWay = false;
        }
    }

	IEnumerator TurnColliderOffandOn(){
		yield return new WaitForEndOfFrame ();
		fenceCollider.enabled = false;
		yield return new WaitForSeconds (2f);
		fenceCollider.enabled = true;
		yield return new WaitForEndOfFrame ();
	}
}

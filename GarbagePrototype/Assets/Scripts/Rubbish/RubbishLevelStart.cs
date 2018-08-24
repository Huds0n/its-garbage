using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class RubbishLevelStart : MonoBehaviour {

    Rigidbody rb;

    float gameStartTime;

    [Header("Player Interaction")]
    public GameObject player;
    public float minDist = 2f;

    [Header("Rubbish Speed")]
    public float moveSpeed = 3f;

    private void Awake()
    {
        gameStartTime = 0f;
    }

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();

        LeanTween.scale(gameObject, new Vector3(.2f, .2f, .2f), .5f).setLoopPingPong().setEaseInExpo();     
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float timeSinceCreated = Time.timeSinceLevelLoad - gameStartTime;
        if (timeSinceCreated > 3.8f)
        {
            FlyToPlayer();
        }
	}

    void FlyToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= minDist)
        {
            rb.useGravity = false;
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();

            transform.position += dir * moveSpeed * Time.fixedDeltaTime;

            LeanTween.rotate(gameObject, new Vector3(359, 359, 359), .3f).setLoopPingPong();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            player.GetComponent<PlayerMain>().fuelImage.fillAmount += 0.042f;
            Destroy(gameObject);
        }
    }
}

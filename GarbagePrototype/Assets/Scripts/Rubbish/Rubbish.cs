using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubbish : MonoBehaviour {

    bool untouchable = true;

    float startTime;

    GameObject player;

    public float minDist = 3f;

    public float moveSpeed = 2f;

    Rigidbody rb;

    //Jump    
    float upForce = 7f;
    float sideForce = 4f;

    //ObjectiveCheck objScript;

    [Range(0.0f, 5.0f)]
    public float fallMultiplier = 2.5f;
    [Range(0.0f, 5.0f)]
    public float lowJumpMultiplier = 2f;
    AudioSource newspaperPickUp;

    private void Awake()
    {
        startTime = Time.timeSinceLevelLoad;
    }

    // Use this for initialization
    void Start () {
        

        //objScript = Camera.main.GetComponent<ObjectiveCheck>(); 
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();

        //LeanTween.scale(gameObject, new Vector3(.3f,.3f,.3f), .5f).setLoopPingPong().setEaseInExpo();

        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);

        Vector3 force = new Vector3(xForce, yForce, 0);
        rb.velocity = force;
        
        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), .4f).setLoopPingPong();

        newspaperPickUp = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void FixedUpdate () {
        FlyToPlayer();
	}

    void FlyToPlayer()
    {
        float timeSinceCreated = Time.timeSinceLevelLoad - startTime;
        
        if (timeSinceCreated > 1.5)
        {
            untouchable = false;
        } 

        if (timeSinceCreated > 5)
        {
            //blink
            //then destroy
            
            Destroy(gameObject);
        }

        if (untouchable == false)
        {
            if (player != null)
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= minDist)
                {
                    rb.useGravity = false;
                    Vector3 dir = player.transform.position - transform.position;
                    dir.Normalize();

                    transform.position += dir * moveSpeed * Time.fixedDeltaTime;

                    LeanTween.rotate(gameObject, new Vector3(359, 359, 359), .3f).setLoopPingPong();

                    newspaperPickUp.Play(0);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            player.GetComponent<PlayerMain>().fuelImage.fillAmount += 0.1f;
            //objScript.fuelImage.fillAmount += 0.1f;
            if (player.GetComponent<PlayerMain>().largeUI == false)
            {
                player.GetComponent<PlayerMain>().uiCount = 0;
                player.GetComponent<PlayerMain>().largeUI = true;
            }
            //newspaperPickUp.Play(0);
           player.GetComponent<PlayerMain>().canDie = false;
            Destroy(gameObject);
        }
    }
}

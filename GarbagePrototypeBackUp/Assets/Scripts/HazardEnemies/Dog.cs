using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Dog : MonoBehaviour {

    [SerializeField]
    float startPositionX;
    public float endPositionX;

    static float t = 0.0f;
    public float speed = 0.1f;

    Quaternion rotation = Quaternion.identity;

    public GameObject dogShit;

    AudioSource dogAudio;
    public AudioClip dogShitNoise;
    public AudioClip dogEnter;

    [Header("References Script")]
    public ReferencedScripts referencesScript;

    // Use this for initialization
    void Start () {
        startPositionX = -27.5837f;

        transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);

		InvokeRepeating ("DogShitDrop", 3f, 2f);

        dogAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (referencesScript.gameStartCoundownScript.gameStart == true)
        {
            DogMovement();
        }  
	}

    void DogMovement()
    {
        transform.position = new Vector3(Mathf.Lerp(startPositionX, endPositionX, t), transform.position.y, transform.position.z);

        t += speed * Time.fixedDeltaTime;

        if (t > 1.0f && referencesScript.gameStartCoundownScript.gameFinishedWin != true)
        {
			if (transform.rotation.y == 0) {
				rotation.eulerAngles = new Vector3 (0, 180, 0);
				transform.rotation = rotation;

                dogAudio.clip = dogEnter;
                dogAudio.Play();
			} else {
				rotation.eulerAngles = Vector3.zero;
                transform.rotation = rotation;

                dogAudio.clip = dogEnter;
                dogAudio.Play();
			}

            float temp = endPositionX;
            endPositionX = startPositionX;
            startPositionX = temp;
            t = 0.0f;
        }
    }

	void DogShitDrop(){
		int rand = Random.Range (0, 5);
       
		if (rand == 3) {
			Instantiate (dogShit, transform.position, transform.rotation);

            //dogAudio.clip = dogShitNoise;
           // dogAudio.Play();
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (referencesScript.gameStartCoundownScript.gameFinishedWin == false)
            {
				if (referencesScript.playerMainScript.firstEnemyHit == false) {
					referencesScript.playerMainScript.firstEnemyHit = true;
				}
                referencesScript.gameStartCoundownScript.badPedsHit++;

                referencesScript.playerMainScript.anim.SetTrigger ("hurt");

                referencesScript.playerMainScript.StartCoroutine(referencesScript.playerMainScript.HurtSpriteSwear());

                CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .15f);

                referencesScript.squashScript.currentComboState = 0;
                referencesScript.playerMainScript.fuelImage.fillAmount -= 0.05f;

                referencesScript.playerMainScript.hurt = true;
            }
        }
    }
}

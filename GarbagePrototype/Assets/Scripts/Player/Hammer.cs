using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Hammer : MonoBehaviour {

	GameStartCountdown gameStartCoundownScript;
	PlayerMain playerMainScript;

    //public int enemiesHit;

    public bool canDie;

    public bool badEnemyHit;

	public GameObject[] jumpBuildings;

    [Header("Hit Sprites")]
    //Good Sprite Hits
    public GameObject[] hitUI;
    int hitUIposition;

    //Bad Sprite Hits
    public GameObject[] badHitSprites;
    int badHitSpriteArrayPosition;

    //Power Up Hits
    public GameObject powerUpSprite;

    [Header("References Script")]
    public ReferencedScripts referencesScript;

    [Header("Audio")]
    public AudioClip hitRight;
    public AudioClip hitWrong;
    public AudioClip hitPowerUp;
    AudioSource hitAudio;

    // Use this for initialization
    void Start () {
		gameStartCoundownScript = GameObject.Find("Level Based Scripts").GetComponent<GameStartCountdown>();
		playerMainScript = GameObject.Find ("Player").GetComponent<PlayerMain> ();

        hitUI[0].SetActive(false);
        hitUI[1].SetActive(false);
        hitUI[2].SetActive(false);
        hitUI[3].SetActive(false);
        hitUI[4].SetActive(false);

        badHitSprites[0].SetActive(false);
        badHitSprites[1].SetActive(false);
        badHitSprites[2].SetActive(false);

        hitAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

	}

	void BuildingJumping(){
		LeanTween.moveLocalY(jumpBuildings[0], 2.2f, .2f).setLoopPingPong(1).setEaseInBounce();
		LeanTween.moveLocalY(jumpBuildings[1], 2f, .15f).setLoopPingPong(1).setEaseInBounce().setDelay(.1f);
		LeanTween.moveLocalY(jumpBuildings[2], 2.2f, .3f).setLoopPingPong(1).setEaseInBounce();
		LeanTween.moveLocalY(jumpBuildings[3], 2.1f, .15f).setLoopPingPong(1).setEaseInBounce().setDelay(.2f);
		LeanTween.moveLocalY(jumpBuildings[4], 2.2f, .4f).setLoopPingPong(1).setEaseInBounce();
		LeanTween.moveLocalY(jumpBuildings[5], 2.1f, .15f).setLoopPingPong(1).setEaseInBounce().setDelay(.3f);
		LeanTween.moveLocalY(jumpBuildings[6], 2.2f, .1f).setLoopPingPong(1).setEaseInBounce();
		LeanTween.moveLocalY(jumpBuildings[7], 2.1f, .15f).setLoopPingPong(1).setEaseInBounce().setDelay(.05f);
	}

    IEnumerator TimeStop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(.15f);
        Time.timeScale = 1;
    }

	void OnTriggerEnter(Collider other){
        //dont hit ped
		if (other.gameObject.layer ==  9) {
			if (gameStartCoundownScript.gameFinishedWin == false) {
				if (playerMainScript.firstEnemyHit == false) {
					playerMainScript.firstEnemyHit = true;
				}

                badHitSpriteArrayPosition = Random.Range(0, 3);
                badHitSprites[badHitSpriteArrayPosition].SetActive(true);
                badHitSprites[badHitSpriteArrayPosition].transform.position = new Vector3(other.transform.position.x, badHitSprites[badHitSpriteArrayPosition].transform.position.y, other.transform.position.z);
                StartCoroutine(BadHitIcons());

                referencesScript.playerMainScript.anim.SetTrigger ("hurt");

                referencesScript.squashScript.currentComboState = 0;
                referencesScript.squashScript.ActivateTimerToReset = false;
                referencesScript.squashScript.currentComboTimer = referencesScript.squashScript.origTimer;

                badEnemyHit = true;
				canDie = true;
				playerMainScript.hurt = true;

                referencesScript.gameStartCoundownScript.badPedsHit++;

				playerMainScript.fuelImage.fillAmount -= 0.1f;

                hitAudio.clip = hitWrong;
                hitAudio.Play();

                //StartCoroutine (referencesScript.cameraShake.Shake (.15f, .4f));
                CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .15f);
                
            }
            other.gameObject.SetActive(false);

        }
        //can hit ped
		if (other.gameObject.layer == 10) {

            hitUIposition = Random.Range(0, 5);
            hitUI[hitUIposition].SetActive(true);
            hitUI[hitUIposition].transform.position = new Vector3(other.transform.position.x, hitUI[hitUIposition].transform.position.y, other.transform.position.z);
            StartCoroutine(HitIcons());

            referencesScript.squashScript.ComboSystem();
            referencesScript.squashScript.currentComboTimer = referencesScript.squashScript.origTimer;

            playerMainScript.enemiesKilled++;

            BuildingJumping ();
			
            hitAudio.clip = hitRight;
            hitAudio.Play();
        }

        //power up ped
        if(other.gameObject.layer == 14)
        {
            playerMainScript.poweredUp = true;

            other.gameObject.SetActive(false);

            hitAudio.clip = hitPowerUp;
            hitAudio.pitch = 0.55f;
            hitAudio.Play();
        }
	}

    IEnumerator HitIcons()
    {  
        yield return new WaitForSeconds(1f);
        hitUI[hitUIposition].SetActive(false);
    }

    IEnumerator BadHitIcons()
    {
        yield return new WaitForSeconds(1f);
        badHitSprites[badHitSpriteArrayPosition].SetActive(false);
    }
}

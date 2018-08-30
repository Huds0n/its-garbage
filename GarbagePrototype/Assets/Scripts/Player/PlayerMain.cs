using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;
using EZCameraShake;

public class PlayerMain : MonoBehaviour {

    Rigidbody rb;

    #region PlayerLife

    [Header("Life System")]
    //public int lives = 3;

    public bool canDie;
    bool inLimbo;

	public bool justDied;

	//public Text numberLivesText;
    [Space(5)]

	public Text topLivesText;

    #endregion

    #region PlayerMovement

    [Header("Movement")]
    public float movementSpeed = 4f;
    public float mobileMoveSpeed = 1f;

	Vector3 movement;

    //Player Movement Boarder
    public float xBoarderLeft = -12f;
	public float xBoarderRight = 12f;

    [Space(5)]
    #endregion

    #region PlayerJump

    [Header("Jump")]
    public float jumpVelocity;
   
    [Range(0.0f, 5.0f)]
    public float fallMultiplier = 2.5f;
    [Range(0.0f, 5.0f)]
    public float lowJumpMultiplier = 2f;

    public bool grounded;

    [Space(5)]
    #endregion

    #region PlayerDash

    [Header("Dash")]
    //public int amountOfDashes = 3;
    public float dashDistance = 8f;

    public float dashTimer = 2f;
    float originalDashTimer;
    public int dashCount;
    bool ActivateTimerToReset = false;

    bool leftArrowPressed;
    bool rightArrowPressed;

	public float dashRecharger;
	float dashTimeStamp;

    [Space(5)]
    #endregion

    #region PowerUp

    [Header("Power Up")]
    public bool poweredUp;
    int i = 0;

    [Space(5)]
    #endregion

    #region Animations

    [Header("Animation Controls")]
    //public AnimationClip[] idleTakes;
    public RuntimeAnimatorController hurtingAnim;
    public RuntimeAnimatorController defaultAnim;
    public RuntimeAnimatorController confidentAnim;

    public Animator anim;

	public float timeWait = 5f;
	float timeStamp;

    public bool firstEnemyHit;
	bool hurtAnimation;

    int anInt = 0;

    [Space(5)]
    #endregion

    #region HammerSettings

    [Header("Hammer Settings")]
    public bool canPressHammer;

    [SerializeField] float goBackTime = 0.5f;
    public float hammerWait = 0.4f;

    [Space(5)]
    #endregion

    #region TutorialImages

    [Header("Tutorial Images")]
    //Tutorial Objects
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject spaceBar;
    public GameObject newspaperSpriteObject;

    [Space(5)]
    #endregion

    #region Fuel

    [Header("Fuel")]
	public GameObject fuelGameObject;
    public Image fuelImage;

    public bool largeUI;
    public int uiCount = 0;

    [Space(5)]
    #endregion

    #region Hurt

    [Header("Hurt")]
   
    public bool hurt;

    public Image damageImage;

	bool deathAnimationPlaying;

    [Space(5)]
    #endregion

    #region Audio

    [Header("Audio")]
    public AudioClip monsterSwingNormal;
    public AudioClip monsterSwingSide;
    public AudioClip monsterSwingHeavy;
    public AudioClip monsterHurt;
    public AudioClip monsterJump;
    public AudioClip monsterFall;
    public AudioClip monsterDash;
    AudioSource monsterAudio;

    [Space(5)]
    #endregion

    [Header("Enemy Stats")]
    public int enemiesKilled;
    
    [Header("References Script")]
    public ReferencedScripts referencesScript;

    // Use this for initialization
    void Start () {
        //Player Rigidbody
        rb = GetComponent<Rigidbody>();

        //Find player Animator
        //anim = GameObject.Find("combined_monster2").GetComponent<Animator>();
        anim = GameObject.Find("newWeapTest01").GetComponent<Animator>();

        //Find player Audiosource
        monsterAudio = GetComponent<AudioSource>();

        //for being hurt when hit
        damageImage.enabled = false;

        //for dash mechanic
        originalDashTimer = dashTimer;

        timeStamp = Time.time + timeWait;

        dashTimeStamp = Time.time + dashRecharger;

        //Level 1 tutorial ----- change so only level 1 (bool)
        StartCoroutine(TutorialObjects());
    }
	
	// Update is called once per frame
	void Update () {
        //if inbetween gameStart and gameFinish
        if (referencesScript.gameStartCoundownScript.gameStart == true && referencesScript.gameStartCoundownScript.gameFinishedWin == false)
        {
            JumpMechanic();
            DashMechanic();

            if(largeUI == true && uiCount == 0)
            {
                StartCoroutine(FuelUI());
            }
        }

		//PlayerAnimation ();
		if (deathAnimationPlaying == false) {
			PlayerAnimation ();
		}

        //Player health and lives
        LifeSystem();
    }

    void FixedUpdate() {
        //Won level?
        if (fuelImage.fillAmount >= 1) {
            
			referencesScript.gameStartCoundownScript.gameFinishedWin = true;
		}

        //While in-game...
		if ((referencesScript.gameStartCoundownScript.gameStart == true && referencesScript.gameStartCoundownScript.inPause == false) && deathAnimationPlaying == false)
        {
            Movement();
            LifeSystem();
        }

        //Start hurting!
        if (hurt == true)
        {
			hurtAnimation = true;

            monsterAudio.clip = monsterHurt;
            monsterAudio.pitch = Random.Range(0.9f, 1.1f);
            monsterAudio.Play(0);

            StartCoroutine(Hurting());
        }

        //Got a power-up?
        if(poweredUp == true)
        {
            StartCoroutine(PowerUp());
        }   
    }

    void PlayerAnimation()
    {
        //Get both axis from player keyboard (x,y)
        float movementAnimation = Input.GetAxisRaw("Horizontal");
        float heightAnimation = Input.GetAxisRaw("Vertical");

        //Set movement animation parameter to the float 'movementAnimation'
        anim.SetFloat("movement", movementAnimation);


        //Idle animations
        if ((fuelImage.fillAmount < .3f && firstEnemyHit == true) && movementAnimation == 0)
        {
            anim.SetBool("isIdleHurt", true);
            
            anim.SetBool("isIdleNormal", false);
        }
        else if ((fuelImage.fillAmount >= .3f && fuelImage.fillAmount < .7f) && movementAnimation == 0)
        {
            anim.SetBool("isIdleNormal", true);
            
            anim.SetBool("isIdleHurt", false);
            anim.SetBool("isIdleConfident", false);
        }
        else if ((fuelImage.fillAmount >= .7f) && movementAnimation == 0)
        {
            anim.SetBool("isIdleConfident", true);

            anim.SetBool("isIdleNormal", false);
        }

        //Look at weapon animation
		/*if(timeStamp < Time.time && movementAnimation == 0){
			anim.SetTrigger ("weaponLook");

			timeStamp += timeWait + 5f;
		}*/

        //Jump animation
        if ((heightAnimation > 0 && grounded == false) && (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow)) && anInt == 0)
        {
            anInt++;

            anim.SetBool("Jump", true);

            anim.SetBool("isIdleNormal", false);
            anim.SetBool("isIdleHurt", false);
            anim.SetBool("isIdleConfident", false);
        }
        if ((grounded == true) && (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow)))
        {
            anInt = 0;

            anim.SetBool("Jump", false);
            anim.SetBool("jumpLeft", false);
            anim.SetBool("jumpRight", false);
        }

        //Movement (horizontal) animation
        if ((heightAnimation > 0 && grounded == false) && (Input.GetKey(KeyCode.LeftArrow) && movementAnimation < 0))
        {
            anim.SetBool("jumpLeft", true);
        }
        if ((grounded == true) && (Input.GetKey(KeyCode.LeftArrow) && movementAnimation < 0))
        {
            anim.SetBool("jumpLeft", false);
        }
 
        if ((heightAnimation > 0 && grounded == false) && Input.GetKey(KeyCode.RightArrow) && movementAnimation > 0)
        {
            anim.SetBool("jumpRight", true);
        }
        if ((grounded == true) && Input.GetKey(KeyCode.RightArrow) && movementAnimation > 0)
        {
            anim.SetBool("jumpRight", false);
        }

        //Swing weapon animation
		if(Input.GetKeyDown(KeyCode.Space) && canPressHammer == true)
		{
			movementAnimation = 0;

			StartCoroutine (SwingingMotion ());
		}
    }

    IEnumerator SwingingMotion()
    {
        canPressHammer = false;
        //weaponTrail.SetActive(true);

        //If combo is 0 = normal hit animation
        if (referencesScript.squashScript.currentComboState < 1)
        {
            monsterAudio.clip = monsterSwingNormal;
            monsterAudio.pitch = Random.Range(0.9f, 1.1f);
            monsterAudio.Play();

            anim.SetTrigger("NormalSwing");
        }
        //If combo is 1 = swing hit animation
        if (referencesScript.squashScript.currentComboState == 1)
        {
            monsterAudio.clip = monsterSwingSide;
            monsterAudio.Play();

            anim.SetTrigger("SideSwing");
        }
        //If combo is 2 or more = heavy hit animation
        if (referencesScript.squashScript.currentComboState >= 2)
        {
            monsterAudio.clip = monsterSwingHeavy;
            monsterAudio.Play();
            anim.SetTrigger("HeavySwing");
        }
        
        yield return new WaitForSeconds(goBackTime);
        //weaponTrail.SetActive(false);
        yield return new WaitForSeconds(hammerWait);
        canPressHammer = true;
        yield return null;
    }

    //relocate life system to a 'DontDestroyOnLoad object' ? maybe? we'll see...
    void LifeSystem()
    {
        //If died
        if ((fuelImage.fillAmount <= 0 && referencesScript.gameStartCoundownScript.gameStart == true) && !inLimbo && !canDie)
        {
			referencesScript.livesScript.playerLives--;

            inLimbo = true;
            canDie = true;
			justDied = true;

			referencesScript.livesScript.i = 0;

            //If you have more than 0 lives in the tank
			if (referencesScript.livesScript.playerLives > 0)
			{
				fuelImage.fillAmount = 0.2f;

				StartCoroutine (deathAnimation ());

                inLimbo = false;
                canDie = false;
            }
        }

        //If died and 0 lives left
        if (inLimbo && referencesScript.livesScript.playerLives == 0)
        {
				//setActive(false) all pedestrians to reset
				Debug.Log("GAME OVER MAN!");
				//destroy/finish game
				inLimbo = false;

                //Destory player or set active more likely change
                Destroy(gameObject);
                //gameObject.SetActive(false);
        }
    }

	IEnumerator deathAnimation(){
		deathAnimationPlaying = true;

		anim.SetBool ("death", true);

		CapsuleCollider playerCollider = GetComponent<CapsuleCollider> ();
		playerCollider.enabled = false;

		rb.isKinematic = true;

		yield return new WaitForSeconds (1.8f);
		transform.position = new Vector3 (transform.position.x, 8f, transform.position.z);

		playerCollider.enabled = true;

		rb.isKinematic = false;

		anim.SetBool ("death", false);

		deathAnimationPlaying = false;
        yield return new WaitForEndOfFrame ();
	}

    void Movement()
    {
        //If not swinging weapon and the game is not over
        if (referencesScript.squashScript.canPressHammer == true && referencesScript.gameStartCoundownScript.gameFinishedWin == false)
        {
            //PC CONTROLS

            ///METHOD 1
            //var x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * movementSpeed;
            //transform.Translate(x, 0, 0);

            ///METHOD 2
            float moveHorizontal = Input.GetAxis("Horizontal");
            /*Vector3 */
            movement = new Vector3(moveHorizontal * movementSpeed, 0, 0);

            if (canPressHammer == true)
            {
                rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
            }

            //MOBILE CONTROLS

            ///METHOD 1
            //float accelerationX = Input.acceleration.x;
            //Vector3 mobileMovement = new Vector3(accelerationX * mobileMoveSpeed, 0, 0);
            //rb.MovePosition(rb.position + mobileMovement * Time.fixedDeltaTime);

            ///METHOD 2
            //transform.Translate(Input.acceleration.x * Time.fixedDeltaTime * mobileMoveSpeed, 0, 0);

            ///METHOD 3
            //transform.Translate(Input.acceleration.x, 0, 0); 

            //Player movement constraint boarder
            if (rb.position.x >= xBoarderRight)
            {
                rb.position = new Vector3(xBoarderRight, rb.position.y, rb.position.z);
            }
            else if (rb.position.x <= xBoarderLeft)
            {
                rb.position = new Vector3(xBoarderLeft, rb.position.y, rb.position.z);
            }
        }
    }
    
    void JumpMechanic()
    {
        //Jump physics
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Z))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        //Jumping action
        if ((Input.GetKeyDown(KeyCode.UpArrow) && referencesScript.squashScript.canPressHammer == true) && grounded == true)
        {
            monsterAudio.clip = monsterJump;
            monsterAudio.pitch = Random.Range(0.9f, 1.1f);
            monsterAudio.Play();

            rb.velocity = Vector3.up * jumpVelocity;

            grounded = false;
        }
    }

    #region DashMechanic
    void DashMechanic()
    {
        ResetDashState(ActivateTimerToReset);

        //First button press (get ready to dash)
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !rightArrowPressed)
        {
            leftArrowPressed = true;

            dashCount++;

            monsterAudio.clip = monsterDash;
            monsterAudio.Play();

            ActivateTimerToReset = true;

            //If button pressed again in small time window, then dash
            if (dashCount == 2)
            {
                rb.AddForce(-dashDistance, 0, 0, ForceMode.Impulse);
            }
        }
        //Reset if pressed Right Arrow key just before you pressed Left Arrow key, and now set back to 0
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && rightArrowPressed)
        {
            dashCount = 0;

            rightArrowPressed = false;
        }

        //First button press (get ready to dash)
        if (Input.GetKeyDown(KeyCode.RightArrow) && !leftArrowPressed)
        {
            rightArrowPressed = true;

            dashCount++;

            monsterAudio.clip = monsterDash;
            monsterAudio.Play();

            ActivateTimerToReset = true;

            //If button pressed again in small time window, then dash
            if (dashCount == 2)
            {
                rb.AddForce(dashDistance, 0, 0, ForceMode.Impulse);
            }
        }
        //Reset if pressed Left Arrow key just before you pressed Right Arrow key, and now set back to 0
        else if (Input.GetKeyDown(KeyCode.RightArrow) && leftArrowPressed)
        {
            dashCount = 0;

            leftArrowPressed = false;
        }

        //Reset dashCount state if pressed Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashCount = 0;
        }
    }

    //If dashCount is 1 and ready to dash, start this, only have 0.2 secs to press button again to dash, otherwise reset.
    void ResetDashState(bool resetTimer)
    {
        if (resetTimer)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                dashCount = 0;

                ActivateTimerToReset = false;
                leftArrowPressed = false;
                rightArrowPressed = false;

                dashTimer = originalDashTimer;
            }
        }
    }
    #endregion

    IEnumerator TutorialObjects()
    {
		referencesScript.gameStartCoundownScript.inTutorial = true;

        yield return new WaitForSeconds(3f);

        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

        yield return new WaitForSeconds(5f);

        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        upArrow.SetActive(true);

		referencesScript.gameStartCoundownScript.inTutorial = false;

        yield return new WaitForSeconds(3f);

        upArrow.SetActive(false);
        spaceBar.SetActive(true);

        yield return new WaitForSeconds(4f);

        spaceBar.SetActive(false);
        newspaperSpriteObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        newspaperSpriteObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }

    IEnumerator PowerUp()
    {
        if (i == 0)
        {
            i = 1;
            movementSpeed = movementSpeed * 2f;

            yield return new WaitForSeconds(10f);

            movementSpeed = 4f;

            poweredUp = false;

            i = 0;
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator Hurting()
    {	
        fuelGameObject.transform.localScale = new Vector3(fuelGameObject.transform.localScale.x - 0.1f, fuelGameObject.transform.localScale.y - 0.1f, fuelGameObject.transform.localScale.z - 0.1f);

        yield return new WaitForSeconds(0.025f);

        fuelGameObject.transform.localScale = new Vector3(fuelGameObject.transform.localScale.x + 0.1f, fuelGameObject.transform.localScale.y + 0.1f, fuelGameObject.transform.localScale.z + 0.1f);

		hurt = false;

        yield return new WaitForEndOfFrame();
    }

    IEnumerator FuelUI()
    {
        uiCount = 1;

        fuelGameObject.transform.localScale = new Vector3(fuelGameObject.transform.localScale.x + 0.1f, fuelGameObject.transform.localScale.y + 0.1f, fuelGameObject.transform.localScale.z + 0.1f);

        yield return new WaitForSeconds(0.025f);

        fuelGameObject.transform.localScale = new Vector3(fuelGameObject.transform.localScale.x - 0.1f, fuelGameObject.transform.localScale.y - 0.1f, fuelGameObject.transform.localScale.z - 0.1f);

        largeUI = false;

        yield return new WaitForEndOfFrame();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Collided with ground layer
        if (collision.gameObject.layer == 12)
        {
            grounded = true;
            justDied = false;
        }

        //Stepped in dog shit
        if (collision.gameObject.layer == 17)
        {
            if (referencesScript.gameStartCoundownScript.gameFinishedWin == false)
            {
                referencesScript.gameStartCoundownScript.badPedsHit++;

                anim.SetTrigger ("hurt");

                referencesScript.squashScript.currentComboState = 0;
                referencesScript.squashScript.ActivateTimerToReset = false;
                referencesScript.squashScript.currentComboTimer = referencesScript.squashScript.origTimer;

                canDie = true;
                hurt = true;

                fuelImage.fillAmount -= 0.05f;

                //StartCoroutine (referencesScript.cameraShake.Shake (.15f, .4f));
                CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .15f);
            }
            Destroy(collision.gameObject);
        }
    }
}

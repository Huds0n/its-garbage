using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Squash : MonoBehaviour {

	float currentLerpTime;
    
	[SerializeField] float goBackTime = 0.5f;
	public float hammerWait = 0.4f;

	public bool canPressHammer;

    Animator anim;

    //GameStartCountdown gameStartCoundownScript;

    //public GameObject weaponTrail;

    [Header("Combo Stats")]
    public float currentComboTimer = 3f;
    public int currentComboState = 0;
    public float origTimer;
    public bool ActivateTimerToReset = false;
    public Text comboText;
    public Text backgroundComboText;

    [Header("References Script")]
    public ReferencedScripts referencesScript;

    // Use this for initialization
    void Start () {
        //anim = GameObject.Find("combined_monster2").GetComponent<Animator>();
        anim = GameObject.Find("newWeapTest01").GetComponent<Animator>();
        canPressHammer = true;
        //weaponTrail.SetActive(false);

        comboText.enabled = false;
        backgroundComboText.enabled = false;
        origTimer = currentComboTimer;
    }

	// Update is called once per frame
	void Update () {
        
        if (referencesScript.gameStartCoundownScript.gameStart == true)
        {
            PcControls();
            //MobileControls();

            ResetComboState(ActivateTimerToReset);


            if(currentComboState >= 2)
            {
                comboText.enabled = true;
                backgroundComboText.enabled = true;
                comboText.text = "x" + currentComboState;
                backgroundComboText.text = comboText.text;
            } else
            {
                comboText.enabled = false;
                backgroundComboText.enabled = false;
            }
        }
	}

    void PcControls()
    {
		if (Input.GetKeyDown(KeyCode.Space) && canPressHammer == true)
        {
			//StartCoroutine (SwingingMotion ());
        }
    }

    void MobileControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //anim.SetBool("Smash1", true);
     
                    StartCoroutine(SwingingMotion());
                    break;

                case TouchPhase.Ended:
                    //anim.SetBool("Smash1", false);
                    break;
            }
        }
    }

    IEnumerator SwingingMotion(){
		canPressHammer = false;
        //weaponTrail.SetActive(true);
        anim.SetBool("HeavyHit", true);
        yield return new WaitForSeconds (goBackTime);
       //weaponTrail.SetActive(false);
        yield return new WaitForSeconds (hammerWait);
        canPressHammer = true;
        anim.SetBool("HeavyHit", false);
        yield return null;
	}

    void ResetComboState(bool resetTimer)
    {
        if (resetTimer)
        {
            currentComboTimer -= Time.deltaTime;
            if(currentComboTimer <= 0)
            {
                currentComboState = 0;
                ActivateTimerToReset = false;
                currentComboTimer = origTimer;
            }
        }
    }
   public void ComboSystem()
    {
        currentComboState++;

        ActivateTimerToReset = true;

        if(currentComboState == 1)
        {
            Debug.Log("1 hit");
        }
        if(currentComboState == 2)
        {
            Debug.Log("2 hit, The combo should Start");
            //Do your awesome stuff there and combokill the bitches
        }
        if(currentComboState >= 3)
        {
            Debug.Log("3 hits");
        }
    }
}

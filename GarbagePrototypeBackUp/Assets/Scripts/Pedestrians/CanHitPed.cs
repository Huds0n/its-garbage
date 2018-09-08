using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanHitPed : Pedestrian, IPooledObject {

    public GameObject rubbish;

	Camera cam;

    public Indicators indicatorScript;

    //for right indicator sums
    int i;
    int iTwo;
    int iThree;

    //for left indicator sums
    int o;
    int oTwo;
    int oThree;

    [Range(0,100)]
    public int pedCountRight;

    [Range(0, 100)]
    public int pedCountLeft;

    GameObject player;

    // Use this for initialization
    public void OnObjectSpawn() {
        PedestrianRefsValues();

		cam = Camera.main;

        indicatorScript = GameObject.Find("Level Based Scripts").GetComponent<Indicators>();

        //set right indicator sums
        i = 0;
        iTwo = 0;
        iThree = 0;

        //set left indicator sums
        o = 0;
        oTwo = 0;
        oThree = 0;

		MaterialChange ();
    }
	
    public void Update()
    {
        player = GameObject.Find("Player");

        PedestrianSpeeds();
        PedestrianFacingWay();
        
        PedestrianOffScreenIndicator();

    }

    // Update is called once per frame
    public void FixedUpdate () {
        PedestrianMovement();
    }

    public void SpawnRubbish()
    {
        Instantiate(rubbish, transform.position, transform.rotation);
    }

    #region Ped Offscreen Indicator

    public void PedestrianOffScreenIndicator()
	{
		Vector3 screenPos = cam.WorldToViewportPoint (transform.position);
        
        if (!opposite) {
			if (screenPos.x > 1f) {
				//Debug.Log ("Out of screen");
                //show UI indicator
                if (i == 0)
                {
                    pedCountRight = CountUp(indicatorScript.offScreenCountRight, 1);
                    indicatorScript.offScreenCountRight = pedCountRight;
                    iTwo = 0;
                    i = 1;
                }
			} else if (screenPos.x < 1f && pedCountRight != 0) {
				//Debug.Log ("take off ui indicator");
                //indicatorScript.offScreenCount -= 1;
                if (iTwo == 0)
                {
                    pedCountRight = CountDown(indicatorScript.offScreenCountRight, 1);
                    indicatorScript.offScreenCountRight = pedCountRight;
                    i = 0;
                    iTwo = 1;
                }
            }
		} else {
			if (screenPos.x < 0f) {
                if (o == 0)
                {
                    pedCountLeft = CountUp(indicatorScript.offScreenCountLeft, 1);
                    indicatorScript.offScreenCountLeft = pedCountLeft;
                    oTwo = 0;
                    o = 1;
                }
                //Debug.Log ("Out of screen");
                //show UI indicator

            } else if(screenPos.x > 0f && pedCountLeft != 0) {
                //Debug.Log ("take off ui indicator");
                if (oTwo == 0)
                {
                    pedCountLeft = CountDown(indicatorScript.offScreenCountLeft, 1);
                    indicatorScript.offScreenCountLeft = pedCountLeft;
                    o = 0;
                    oTwo = 1;
                }
            }
		}
	}

    int CountUp(int countBase,int addCount) 
    {
        int sum;
        sum = countBase + addCount;
        return sum;
    }

    int CountDown(int countBase, int addCount)
    {
        int sum;
        sum = countBase - addCount;
        return sum;
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        SpawnRubbish();
        if (other.gameObject.tag == "Hammer")
        {
            if (!opposite)
            {
                i = 0;
                iTwo = 0;
                iThree = 0;
            }
            else
            {
                o = 0;
                oTwo = 0;
                oThree = 0;
            }
            
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //Ped Killer Wall
        if (other.gameObject.layer == 13)
        {
            if (referencesScript.gameStartCoundownScript.gameFinishedWin == false)
            {
                if (!opposite && iThree == 0)
                {
                    pedCountRight = CountDown(indicatorScript.offScreenCountRight, 1);
                    indicatorScript.offScreenCountRight = pedCountRight;
                    iThree = 1; 
                } else if (opposite && oThree == 0)
                {
                    pedCountLeft = CountDown(indicatorScript.offScreenCountLeft, 1);
                    indicatorScript.offScreenCountLeft = pedCountLeft;
                    oThree = 1;
                }
                i = 0;
                iTwo = 0;
                o = 0;
                oTwo = 0;

                if (player != null)
                {
                    referencesScript.playerMainScript.fuelImage.fillAmount -= 0.03f;
                    referencesScript.playerMainScript.hurt = true;
                    //StartCoroutine(referencesScript.cameraShake.Shake(.15f, .4f));
                    //referencesScript.hammerScript.canDie = true;
                }

                if (opposite && indicatorScript.explodeLeft == false)
                {
                    indicatorScript.explodeLeft = true;
                }

                if (!opposite && indicatorScript.explodeRight == false)
                {
                    indicatorScript.explodeRight = true;
                }
            }
            
            gameObject.SetActive(false);
        }
    }
}

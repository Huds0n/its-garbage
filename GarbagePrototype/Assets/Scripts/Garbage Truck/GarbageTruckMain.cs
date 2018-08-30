using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class GarbageTruckMain : MonoBehaviour {

    int i;
    int iTwo;

    [Header("References Script")]
    public ReferencedScripts referencesScript;

    SceneChanging sceneChangingScript;

    AudioSource garbageTruckNoise;

    // Use this for initialization
    void Start () {
        i = 0;
        iTwo = 0;

        sceneChangingScript = GameObject.Find("SceneManager").GetComponent<SceneChanging>();

        garbageTruckNoise = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //if player won game level move to player position x
		if(referencesScript.gameStartCoundownScript.gameFinishedWin == true)
        {
            garbageTruckNoise.Play();

            MoveToPlayerPosition();
        }
	}

    void MoveToPlayerPosition()
    {
        if (i == 0)
        {
            LeanTween.moveX(gameObject, referencesScript.playerObject.transform.position.x + 0.5f, 5f);
            StartCoroutine(TruckOperations());
            i = 1;
        }
    }

    IEnumerator TruckOperations()
    {
        yield return new WaitForSeconds(7f);
        //set player inactive
        referencesScript.playerObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        MoveOutOfScreen();
    }

    void MoveOutOfScreen()
    {
        if (iTwo == 0)
        {
            //move truck out of scene
            LeanTween.moveX(gameObject, gameObject.transform.position.x + 30, 5f).callOnCompletes();
            sceneChangingScript.winLevelOne = true;
            iTwo = 1;
        }
    }
}

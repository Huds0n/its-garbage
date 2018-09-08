using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.SceneManagement;

public class GarbageTruckMain : MonoBehaviour {

    int i;
    int iTwo;

    //AudioSource garbageTruckNoise;

    Scene scene;

    SceneChanging sceneChangingScript;

    [Header("References Script")]
    public ReferencedScripts referencesScript;

    // Use this for initialization
    void Start () {
        i = 0;
        iTwo = 0;

        scene = SceneManager.GetActiveScene();

        sceneChangingScript = GameObject.Find("SceneManager").GetComponent<SceneChanging>();

        //garbageTruckNoise = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //If player won game level move to player position x
		if(referencesScript.gameStartCoundownScript.gameFinishedWin == true)
        {
            //garbageTruckNoise.Play();

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
            //Move truck out of scene
            LeanTween.moveX(gameObject, gameObject.transform.position.x + 30, 5f).callOnCompletes();
            if(scene.name == "Level1")
            {
                sceneChangingScript.winLevelOne = true;
            }
            if (scene.name == "Level2")
            {
                sceneChangingScript.winLevelTwo = true;
            }
            if (scene.name == "Level3")
            {
                sceneChangingScript.winLevelThree = true;
            }
            iTwo = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour {

    public Text scoreText;

    public Text perfectText;
    public Text perfectTextBack;

    SceneChanging sceneChanger;

    public int finalScore;

	// Use this for initialization
	void Start () {
        perfectText.enabled = false;
        perfectTextBack.enabled = false;

        sceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChanging>();

        finalScore = sceneChanger.levelOneScore + sceneChanger.levelTwoScore + sceneChanger.levelThreeScore;

        scoreText.text = finalScore.ToString() + "/9 bins";
    }
	
	// Update is called once per frame
	void Update () {
        if (finalScore == 9)
        {
            perfectText.enabled = true;
            perfectTextBack.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetVars();
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void ResetVars()
    {
        sceneChanger.startMenuPressedSpace = 0;
        sceneChanger.winLevelOne = false;
        sceneChanger.winLevelTwo = false;
        sceneChanger.winLevelThree = false;
        //scenechanger.winLevelFour = false;

        sceneChanger.levelOneScore = 0;
        sceneChanger.levelTwoScore = 0;
        sceneChanger.levelThreeScore = 0;
        //scenechanger.overallScore = 0;

        sceneChanger.loadingScene = false;

        sceneChanger.i = 0;
        sceneChanger.iTwo = 0;
        sceneChanger.iThree = 0;

        sceneChanger.map = 0;
    }
}

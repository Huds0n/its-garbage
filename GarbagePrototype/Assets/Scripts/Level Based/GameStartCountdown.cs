﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DentedPixel;

public class GameStartCountdown : MonoBehaviour {

	[Header("Camera Holder")]
	public GameObject cameraHolder;

    [Header("Game Conditions")]
    public bool gameStart;
	public bool gameFinishedWin;
	int i = 0;

    [Header("Text")]
    public Text gameStartText;
    public Text countdownText;
    public Text levelCompleteText;

    [Header("Fuel UI")]
    public GameObject fuelUI;
    RectTransform rt;
    Vector3 fuelRtStartPos;
    float fuelRtEndPos = 184f;

	public bool inTutorial;

	[Header("Pause Menu")]
	public bool inPause;
	public GameObject pauseMenuImage;

	public Button resumeMenuButton, optionsMenuButton, restartMenuButton, quitMenuButton;

    [Header("Game Win")]
    public GameObject gameWinImage;
    public GameObject[] binScoreFills;

    public Text binScoreText;

    public int badPedsHit;
    public int levelOneBinScore;

    int l = 0;

	public Animator anim;

    // Use this for initialization
    void Start () {
		GameStartReset ();
        StartCoroutine(Countdown());
		FuelUIStart ();
        
        pauseMenuImage.SetActive(false);

        gameWinImage.SetActive(false);

        anim = GameObject.Find("newWeapTest01").GetComponent<Animator>();
    }

    private void Update()
    {
        
		if (gameFinishedWin == true && i == 0) {
            levelCompleteText.text = "Level Complete";
			GameFinished ();
		}

		if(Input.GetKeyDown(KeyCode.Escape) && !inPause){
            StartCoroutine(PauseButtons());
			Time.timeScale = 0;
            pauseMenuImage.SetActive(true);
            inPause = true;
			PauseMenu ();
        } else if(Input.GetKeyDown(KeyCode.Escape) && inPause){
			GoBackToGame ();
        }
    }

	void GoBackToGame(){
		l = 0;
		Time.timeScale = 1;
		pauseMenuImage.SetActive(false);
		inPause = false;
	}

	void PauseMenu(){
		if (inPause) {
			Button btn1 = resumeMenuButton.GetComponent<Button> ();
			Button btn2 = optionsMenuButton.GetComponent<Button> ();
			Button btn3 = restartMenuButton.GetComponent<Button> ();
			Button btn4 = quitMenuButton.GetComponent<Button> ();

			btn1.onClick.AddListener (GoBackToGame);
		}
	}

    IEnumerator PauseButtons()
    {
		if (l == 0) {
			l = 1;
			//EventSystem es = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
			yield return new WaitForEndOfFrame();
			resumeMenuButton.Select ();
			//es.SetSelectedGameObject (null);
			//pauseMenuButton.Select(null);
			yield return new WaitForEndOfFrame();
			//es.SetSelectedGameObject (es.firstSelectedGameObject);
			//pauseMenuButton.Select();
		}
    }

	void GameStartReset(){
		LeanTween.moveZ (cameraHolder, -11.33f, 2f).setEaseInOutBack ();

		i = 0;

		if (gameStart == true)
		{
			gameStart = false;
		}

		countdownText.text = "";
		gameStartText.text = "";
		levelCompleteText.text = "";
	}

	void FuelUIStart(){
		rt = fuelUI.GetComponent<RectTransform>();
		fuelRtStartPos = new Vector3(-235f, 340f, 0);
		rt.localPosition = fuelRtStartPos;
		LeanTween.moveY(rt, fuelRtEndPos, .75f).setEaseInOutBack().setDelay(2f);
	}

	void GameFinished(){

		LeanTween.moveZ (cameraHolder, -15f, 2f).setEaseInOutBack ();
        if(badPedsHit < 3)
        {
            levelOneBinScore = 3;
        } else if(badPedsHit >=3 && badPedsHit <= 10)
        {
            levelOneBinScore = 2;
        }
        else if(badPedsHit > 10)
        {
            levelOneBinScore = 1;
        }

		anim.SetBool ("victory", true);

        StartCoroutine(GarbageBinScore());
		i = 1;
	}

    IEnumerator GarbageBinScore()
    {
        yield return new WaitForSeconds(4f);
        gameWinImage.SetActive(true);
        binScoreText.text = "0/3 Bin Score";
        yield return new WaitForSeconds(0.5f);
        if (levelOneBinScore == 3)
        {
            binScoreFills[0].GetComponent<Image>().fillAmount = 1;
            binScoreText.text = "1/3 Bin Score";
            yield return new WaitForSeconds(1f);
            binScoreFills[1].GetComponent<Image>().fillAmount = 1;
            binScoreText.text = "2/3 Bin Score!";
            yield return new WaitForSeconds(1f);
            binScoreFills[2].GetComponent<Image>().fillAmount = 1;
            binScoreText.text = "3/3 Bin Score!! PERFECT!";
        }
        else if (levelOneBinScore == 2)
        {
            binScoreFills[0].GetComponent<Image>().fillAmount = 1;
            binScoreText.text = "1/3 Bin Score";
            yield return new WaitForSeconds(1f);
            binScoreFills[1].GetComponent<Image>().fillAmount = 1;
            binScoreText.text = "2/3 Bin Score!";
        }
        else
        {
            binScoreFills[0].GetComponent<Image>().fillAmount = 1;
            binScoreText.text = "1/3 Bin Score";
        }
    }
    IEnumerator Countdown()
    {
        gameStartText.text = "Game Begins in";
        yield return new WaitForSeconds(1.0f);
        gameStartText.text = "";
        countdownText.text = "3";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.0f);   
        countdownText.text = "1";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "";
        gameStartText.text = "Go!";
        gameStart = true;
        yield return new WaitForSeconds(1.0f);
        gameStartText.text = "";
    }
}

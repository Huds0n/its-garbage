using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DentedPixel;
using UnityEngine.SceneManagement;

public class GameStartCountdown : MonoBehaviour {

	[Header("Camera Holder")]
	public GameObject cameraHolder;

    [Header("Game Conditions")]
    public bool gameStart;
	public bool gameFinishedWin;
	int i = 0;

    [Header("Text")]
    public Text gameStartText;
    public Text gameStartBackText;

    public Text countdownText;
    public Text countdownBackText;

    //public Text levelCompleteText;

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
    public Text endLoadingText;

    public int badPedsHit;
    public int endOfLevelBinScore;

    public int finalScore;

    public int l = 0;

	public Animator anim;

    Scene currentScene;

    public GameObject healthBarUI;
    public GameObject dashUI;
    public GameObject dashUIbackground;

    SceneChanging scenechanger;

    public GameObject player;

    public string goToLevelName;

    Image blackFade;

    public GameObject[] particalWinEffects;

    // Use this for initialization
    void Start () {
		GameStartReset ();
        StartCoroutine(Countdown());
		FuelUIStart ();
        
        pauseMenuImage.SetActive(false);

        gameWinImage.SetActive(false);

        anim = GameObject.Find("newWeapTest01").GetComponent<Animator>();

        currentScene = SceneManager.GetActiveScene();

        LeanTween.moveLocalY(dashUI, -210f, .5f).setDelay(.5f);
        LeanTween.moveLocalY(dashUIbackground, -210f, .5f).setDelay(.5f);

        scenechanger = GameObject.Find("SceneManager").GetComponent<SceneChanging>();

        goToLevelName = "StartMenu";

        particalWinEffects[0].SetActive(false);
        particalWinEffects[1].SetActive(false);
    }

    private void Update()
    {

		if (gameFinishedWin == true && i == 0) {
            //levelCompleteText.text = "Level Complete";
			GameFinished ();
		}

		if((Input.GetKeyDown(KeyCode.Escape) && !inPause) && player != null){
            StartCoroutine(PauseButtons());
			Time.timeScale = 0;
            pauseMenuImage.SetActive(true);
            inPause = true;
			PauseMenu ();
        } else if(Input.GetKeyDown(KeyCode.Escape) && inPause){
			GoBackToGame ();
        }
    }

    public void ResetVars()
    {
        scenechanger.startMenuPressedSpace = 0;
        scenechanger.winLevelOne = false;
        scenechanger.winLevelTwo = false;
        scenechanger.winLevelThree = false;
        scenechanger.winLevelFour = false;

        scenechanger.loadingScene = false;
        
        scenechanger.i = 0;
        scenechanger.iTwo = 0;
        scenechanger.iThree = 0;
    }

	void GoBackToGame(){
		l = 0;
		Time.timeScale = 1;
		pauseMenuImage.SetActive(false);
		inPause = false;
	}

    void GoRestart()
    {
        ResetVars();
        //scenechanger.pauseMenuRestart = true;
    }

    void QuitTheGame()
    {
        Application.Quit();
    }

	void PauseMenu(){
		if (inPause) {
			Button btn1 = resumeMenuButton.GetComponent<Button> ();
			Button btn2 = optionsMenuButton.GetComponent<Button> ();
			Button btn3 = restartMenuButton.GetComponent<Button> ();
			Button btn4 = quitMenuButton.GetComponent<Button> ();

			btn1.onClick.AddListener (GoBackToGame);
            btn3.onClick.AddListener(GoRestart);
            btn4.onClick.AddListener(QuitTheGame);
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
		//levelCompleteText.text = "";
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
            endOfLevelBinScore = 3;
        } else if(badPedsHit >=3 && badPedsHit <= 10)
        {
            endOfLevelBinScore = 2;
        }
        else if(badPedsHit > 10)
        {
            endOfLevelBinScore = 1;
        }

        //check scene name and set levelOneBinScore
        if(currentScene.name == "Level1")
        {
            if(endOfLevelBinScore == 3)
            {
                scenechanger.levelOneScore = 3;
            }
            else if(endOfLevelBinScore == 2)
            {
                scenechanger.levelOneScore = 2;
            } else
            {
                scenechanger.levelOneScore = 1;
            }
        }

        if (currentScene.name == "Level2")
        {
            if (endOfLevelBinScore == 3)
            {
                scenechanger.levelTwoScore = 3;
            }
            else if (endOfLevelBinScore == 2)
            {
                scenechanger.levelTwoScore = 2;
            }
            else
            {
                scenechanger.levelTwoScore = 1;
            }
        }

        anim.SetBool ("victory", true);

        StartCoroutine(GarbageBinScore());
		i = 1;
	}

    IEnumerator GarbageBinScore()
    {
        LeanTween.moveLocalY(healthBarUI, 350f, 1f);

        LeanTween.moveLocalY(dashUI, -300f, 1f);
        LeanTween.moveLocalY(dashUIbackground, -300f, 1f);

        yield return new WaitForSeconds(4f);

        gameWinImage.SetActive(true);

        binScoreFills[0].SetActive(false);
        binScoreFills[1].SetActive(false);
        binScoreFills[2].SetActive(false);

        binScoreText.text = "0/3 Bin Score";

        endLoadingText.enabled = false;

        particalWinEffects[0].SetActive(true);
        particalWinEffects[1].SetActive(true);

        yield return new WaitForSeconds(0.5f);
        if (endOfLevelBinScore == 3)
        {
            //binScoreFills[0].GetComponent<Image>().fillAmount = 1;
            binScoreFills[0].SetActive(true);
            binScoreText.text = "1/3 Bin Score";
            yield return new WaitForSeconds(1f);
           //binScoreFills[1].GetComponent<Image>().fillAmount = 1;
            binScoreFills[1].SetActive(true);
            binScoreText.text = "2/3 Bin Score!";
            yield return new WaitForSeconds(1f);
            //binScoreFills[2].GetComponent<Image>().fillAmount = 1;
            binScoreFills[2].SetActive(true);
            binScoreText.text = "3/3 Bin Score!! PERFECT!";
            LeanTween.scale(binScoreText.gameObject, new Vector3(1, 1, binScoreText.gameObject.transform.position.z), .5f).setLoopPingPong();

            endLoadingText.enabled = true;
        }
        else if (endOfLevelBinScore == 2)
        {
            //binScoreFills[0].GetComponent<Image>().fillAmount = 1;
            binScoreFills[0].SetActive(true);
            binScoreText.text = "1/3 Bin Score";
            yield return new WaitForSeconds(1f);
            //binScoreFills[1].GetComponent<Image>().fillAmount = 1;
            binScoreFills[1].SetActive(true);
            binScoreText.text = "2/3 Bin Score!";
            LeanTween.scale(binScoreText.gameObject, new Vector3(1, 1, binScoreText.gameObject.transform.position.z), .5f).setLoopPingPong();
            yield return new WaitForSeconds(1f);
            endLoadingText.enabled = true;

        }
        else
        {
            //binScoreFills[0].GetComponent<Image>().fillAmount = 1;
            binScoreFills[0].SetActive(true);
            binScoreText.text = "1/3 Bin Score";
            LeanTween.scale(binScoreText.gameObject, new Vector3(1, 1, binScoreText.gameObject.transform.position.z), .5f).setLoopPingPong();
            yield return new WaitForSeconds(1f);
            endLoadingText.enabled = true;
        }
    }

    IEnumerator Countdown()
    {
        gameStartText.text = "Game Begins in";
        gameStartBackText.text = "Game Begins in";
        countdownText.text = "";
        countdownBackText.text = "";
        yield return new WaitForSeconds(1.0f);
        gameStartText.text = "";
        gameStartBackText.text = "";
        countdownText.text = "3";
        countdownBackText.text = "3";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "2";
        countdownBackText.text = "2";
        yield return new WaitForSeconds(1.0f);   
        countdownText.text = "1";
        countdownBackText.text = "1";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "";
        countdownBackText.text = "";
        gameStartText.text = "Go!";
        gameStartBackText.text = "Go!";
        gameStart = true;
        yield return new WaitForSeconds(1.0f);
        gameStartText.text = "";
        gameStartBackText.text = "";
    }
}

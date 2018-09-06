using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanging : GenericSingletonClass <SceneChanging> {

	public float mapWaitTime = 5f;

    public bool winLevelOne;
    public bool winLevelTwo;
    public bool winLevelThree;
    public bool winLevelFour;

	Scene scene;

	public bool loadingScene;

	Image blackFade;
    Image loadingImage;

	public int i = 0;
    public int iTwo = 0;
    public int iThree = 0;

    public Text loadingText;

    public string goToLevelName;

    public int levelOneScore;
    public int levelTwoScore;
    public int LevelThreeScore;

    public float goBackToMapTime;

    public bool pauseMenuRestart;
    public Button restartPauseButton;

    int p = 0;


    public int map = 0;
    public int startMenuPressedSpace;
    // Use this for initialization
    void Start () {
		scene = SceneManager.GetActiveScene ();
		winLevelOne = false;
		winLevelTwo = false;
		winLevelThree = false;
		winLevelFour = false;

        Application.targetFrameRate = 60;
    }
	
	// Update is called once per frame
	void Update () {
		scene = SceneManager.GetActiveScene ();
		Debug.Log(scene.name);

        //Start Menu to Map
		if (scene.name == "StartMenu" && Input.GetKeyDown(KeyCode.Space) && startMenuPressedSpace == 0) {
            StartCoroutine(StartMenuToMap());
            startMenuPressedSpace = 1;
		}
        if(scene.name == "StartMenu" && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Map to ...
        if (scene.name == "Map")
        {
            loadingImage = GameObject.Find("LoadingPage").GetComponent<Image>();

            GameObject arrowUI = GameObject.Find("Arrow");
            GameObject arrowTwoUI = GameObject.Find("Arrow 2");
            GameObject arrowThreeUI = GameObject.Find("Arrow 3");

            GameObject levelOneUI = GameObject.Find("Level 1");
            GameObject levelTwoUI = GameObject.Find("Level 2");
            GameObject levelThreeUI = GameObject.Find("Level 3");

            
            if (map == 0)
            {
                StartCoroutine(MapTimerText());
                
            }

            //goto Level1
            if (!winLevelOne)
            {
                if (i == 0)
                {
                    arrowUI.SetActive(true);
                    arrowTwoUI.SetActive(false);
                    arrowThreeUI.SetActive(false);

                    levelOneUI.SetActive(true);
                    levelTwoUI.SetActive(false);
                    levelThreeUI.SetActive(false);

                    startMenuPressedSpace = 0;
                    goToLevelName = "Level1";
                    StartCoroutine(MapToLevel());
                }
            }
            //goto Level2
            if (winLevelOne)
            {
                if (iTwo == 0)
                {
                    arrowUI.SetActive(false);
                    arrowTwoUI.SetActive(true);
                    arrowThreeUI.SetActive(false);

                    levelOneUI.SetActive(false);
                    levelTwoUI.SetActive(true);
                    levelThreeUI.SetActive(false);

                    startMenuPressedSpace = 0;
                    goToLevelName = "Level2";
                    StartCoroutine(MapToLevel());
                }
            }
            //goto Level3
            if (winLevelOne && winLevelTwo)
            {
                if (iThree == 0)
                {
                    arrowUI.SetActive(false);
                    arrowTwoUI.SetActive(false);
                    arrowThreeUI.SetActive(true);

                    levelOneUI.SetActive(false);
                    levelTwoUI.SetActive(false);
                    levelThreeUI.SetActive(true);

                    startMenuPressedSpace = 0;
                    goToLevelName = "Level3";
                    StartCoroutine(MapToLevel());
                }
            }

            //arrow "You Are Here!" position change in map depending on which level
        }

        //Level1 win? go back to map to continue
        if (scene.name == "Level1" && winLevelOne) {
            if(levelOneScore == 3)
            {
                goBackToMapTime = 8f;
            }
            if (levelOneScore == 2)
            {
                goBackToMapTime = 6f;
            }
            if (levelOneScore == 1)
            {
                goBackToMapTime = 4f;
            }
            map = 0;
            Invoke("ToMap", goBackToMapTime);
        }

        if (scene.name == "Level2" && winLevelTwo)
        {
            if (levelTwoScore == 3)
            {
                goBackToMapTime = 8f;
            }
            if (levelTwoScore == 2)
            {
                goBackToMapTime = 6f;
            }
            if (levelTwoScore == 1)
            {
                goBackToMapTime = 4f;
            }
            map = 0;
            Invoke("ToMap", goBackToMapTime);
        }

       
	}

    IEnumerator StartMenuToMap()
    {
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        blackFade.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Map");
        CancelInvoke();
        yield return new WaitForEndOfFrame();
    }

    IEnumerator MapTimerText()
    {
        if (scene.name == "Map")
        {
            Text timerMapText = GameObject.Find("Timer").GetComponent<Text>();
            timerMapText.text = ":04";
            yield return new WaitForSeconds(1f);
            timerMapText.text = ":03";
            yield return new WaitForSeconds(1f);
            timerMapText.text = ":02";
            yield return new WaitForSeconds(1f);
            timerMapText.text = ":01";
            yield return new WaitForSeconds(1f);
            timerMapText.text = ":00";
            
            yield return new WaitForEndOfFrame();
            map = 1;
        }
        yield return new WaitForEndOfFrame();
        map = 1;

    }
    IEnumerator MapToLevel()
    {
        if(goToLevelName == "Level1")
        {
            i = 1;
        }
        if (goToLevelName == "Level2")
        {
            iTwo = 1;
        }
        if (goToLevelName == "Level3")
        {
            iThree = 1;
        }
        loadingImage.enabled = false;
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        loadingText = GameObject.Find("Loading Text").GetComponent<Text>();
        loadingText.enabled = false;
        yield return new WaitForSeconds(5f);
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        blackFade.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        loadingImage.enabled = true;
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        blackFade.GetComponent<Animator>().Play("FadeOut");
        loadingText.enabled = true;
        yield return new WaitForSeconds(5f);
        StartCoroutine(LoadAsyncScene());
        CancelInvoke();
    }

    void ToMap()
    { 
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        blackFade.GetComponent<Animator>().Play("FadeIn");
        
        SceneManager.LoadScene("Map");
        CancelInvoke();
    }

	IEnumerator LoadAsyncScene(){
        Debug.Log ("LOADING SCENE");
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (goToLevelName);
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        blackFade.GetComponent<Animator>().Play("FadeIn");
        while (!asyncLoad.isDone) {
			yield return null;
		}
    }
}

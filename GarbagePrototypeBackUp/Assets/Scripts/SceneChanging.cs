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

    
	public Text loadingText;

    public string goToLevelName;

    // Use this for initialization
    void Start () {
		scene = SceneManager.GetActiveScene ();
		winLevelOne = false;
		winLevelTwo = false;
		winLevelThree = false;
		winLevelFour = false;
	}
	
	// Update is called once per frame
	void Update () {
		scene = SceneManager.GetActiveScene ();
		Debug.Log(scene.name);

        //Start Menu to Map
		if (scene.name == "StartMenu" && Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(StartMenuToMap());
		}

        //Map to ...
        if (scene.name == "Map")
        {
            loadingImage = GameObject.Find("LoadingPage").GetComponent<Image>();

            //goto Level1
            if (!winLevelOne)
            {
                if (i == 0)
                {
                    goToLevelName = "Level1";
                    StartCoroutine(MapToLevel());
                }
            }
            //goto Level2
            if (winLevelOne)
            {
                if (iTwo == 0)
                {
                    goToLevelName = "Level3";
                    StartCoroutine(MapToLevel());
                }
            }

            //arrow "You Are Here!" position change in map depending on which level

        }

        //Level1 win? go back to map to continue
        if (scene.name == "Level1" && winLevelOne) {
            Invoke("ToMap", 10f);
            
        }
	}

    IEnumerator StartMenuToMap()
    {
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        blackFade.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Map");
        CancelInvoke();
        yield return new WaitForEndOfFrame();
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
        loadingImage.enabled = false;
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        loadingText = GameObject.Find("Loading Text").GetComponent<Text>();
        loadingText.enabled = false;
        yield return new WaitForSeconds(5f);
        blackFade = GameObject.Find("BlackFade").GetComponent<Image>();
        blackFade.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(1f);
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

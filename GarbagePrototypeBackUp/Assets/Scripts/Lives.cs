using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {

    public GameObject gameOverUIObjects;

    public Text gameOverText;
    public Text gameOverBackgroundText;

    
	public GameObject player;

	public GameObject[] lifeImage;

    public GameObject comboTextGameObject;

	public int playerLives = 3; 

	string lifeNameString; 

	public int i = 0;

    GameStartCountdown gameStartCountdownScript;

	// Use this for initialization
	void Start () {
        gameStartCountdownScript = GameObject.Find("Level Based Scripts").GetComponent<GameStartCountdown>();

        gameOverUIObjects.SetActive(false);

        LeanTween.scale(gameOverText.gameObject, new Vector3(1, 1, gameOverText.gameObject.transform.position.z), 1f).setLoopPingPong();
        LeanTween.scale(gameOverBackgroundText.gameObject, new Vector3(1, 1, gameOverBackgroundText.gameObject.transform.position.z), 1f).setLoopPingPong();

        LeanTween.scale(comboTextGameObject.GetComponent<RectTransform>(), comboTextGameObject.GetComponent<RectTransform>().localScale * 0.9f, 0.2f).setLoopPingPong();
    }
	
	// Update is called once per frame
	void Update () {
		//playerLives = player.GetComponent<PlayerMain> ().lives;
		if(player == null){
            gameOverUIObjects.SetActive(true);
            lifeImage[0].SetActive(false);
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space)){
                gameStartCountdownScript.ResetVars();
				SceneManager.LoadScene ("StartMenu");
			}

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
		}

		if (i == 0) {
			LifeScale ();
		}

		if (playerLives == 1) {
			lifeImage[2].SetActive(false);
			//lifeNameString = "LifeImage3";

			//StartCoroutine (LoseLife ());
		}
		if (playerLives == 0) {
			lifeImage[1].SetActive(false);
			//StartCoroutine (LoseLife ());
		}
	
	}

	void LifeScale(){
		LeanTween.scale (lifeImage [playerLives], new Vector3 (lifeImage [playerLives].transform.localScale.x + 0.1f, lifeImage [playerLives].transform.localScale.y + 0.1f, lifeImage [playerLives].transform.localScale.y + 0.1f), 0.5f).setLoopPingPong ();
		i = 1;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {

	public Text gameOverText;

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

		gameOverText.enabled = false;

        LeanTween.scale(comboTextGameObject.GetComponent<RectTransform>(), comboTextGameObject.GetComponent<RectTransform>().localScale * 0.9f, 0.2f).setLoopPingPong();
    }
	
	// Update is called once per frame
	void Update () {
		//playerLives = player.GetComponent<PlayerMain> ().lives;
		if(player == null){
			gameOverText.enabled = true;
            lifeImage[0].SetActive(false);
            if (Input.GetKeyDown(KeyCode.R)){
                gameStartCountdownScript.ResetVars();
				SceneManager.LoadScene ("StartMenu");
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

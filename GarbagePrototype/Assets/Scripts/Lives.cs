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
	// Use this for initialization
	void Start () {
		gameOverText.enabled = false;

        LeanTween.scale(comboTextGameObject.GetComponent<RectTransform>(), comboTextGameObject.GetComponent<RectTransform>().localScale * 0.9f, 0.2f).setLoopPingPong();
    }
	
	// Update is called once per frame
	void Update () {
		//playerLives = player.GetComponent<PlayerMain> ().lives;
		if(player == null){
			gameOverText.enabled = true;

			if(Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene ("Level1");
			}
		}

		if (i == 0 && playerLives != 0) {
			LifeScale ();
		}
			


		if (playerLives <= 2) {
			lifeImage[2].SetActive(false);
			//lifeNameString = "LifeImage3";

			//StartCoroutine (LoseLife ());
		}
		if (playerLives <= 1) {
			lifeImage[1].SetActive(false);
			//StartCoroutine (LoseLife ());
		}
		if (playerLives == 0) {
			lifeImage[0].SetActive(false);
			//StartCoroutine (LoseLife ());
		}
	}

	void LifeScale(){
		LeanTween.scale (lifeImage [playerLives - 1], new Vector3 (lifeImage [playerLives - 1].transform.localScale.x + 0.1f, lifeImage [playerLives - 1].transform.localScale.y + 0.1f, lifeImage [playerLives - 1].transform.localScale.y + 0.1f), 0.5f).setLoopPingPong ();
		i = 1;
	}
}

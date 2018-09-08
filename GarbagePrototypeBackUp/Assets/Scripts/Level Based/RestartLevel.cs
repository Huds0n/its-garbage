using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {

    Scene currentScene;
    string levelName;

    [Header("References Script")]
    public ReferencedScripts referencesScript;

    private void Start()
    {  
        currentScene = SceneManager.GetActiveScene();
        levelName = currentScene.name;
    }

    // Update is called once per frame
    void Update () {
		if (referencesScript.playerObject == null && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(levelName);
		}
	}
}

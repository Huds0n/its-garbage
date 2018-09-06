using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextChooser : MonoBehaviour {

    public string[] hints;

    Text hintText;

	void Start () {
        StartCoroutine(ShowText());
	}
	
	IEnumerator ShowText()
    {
        int randomHint = Random.Range(0, hints.Length);

        hintText = GetComponent<Text>();
        hintText.text = "";
       
        yield return new WaitForSeconds(5.5f);

        hintText.text = hints[randomHint].ToString();

        yield return new WaitForEndOfFrame();
    }
}

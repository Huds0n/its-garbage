using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTimerText : MonoBehaviour {

    Text timerMapText;

	void Start () {

        timerMapText = GetComponent<Text>();

        timerMapText.text = "";

        StartCoroutine(StartTimer());
	}

    IEnumerator StartTimer()
    {
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
    }
}

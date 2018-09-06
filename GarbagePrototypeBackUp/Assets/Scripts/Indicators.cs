using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour {
	 
	public Image[] offScreenIndicator;

    public int offScreenCountRight;
    public int offScreenCountLeft;


    public GameObject[] indicatorExplosion;
    public bool explodeLeft;
    public bool explodeRight;

	[Header("References Script")]
	public ReferencedScripts referencesScript;

	// Use this for initialization
	void Start () {
		IndicatorPositionsandMovement ();

        indicatorExplosion[0].SetActive(false);
        indicatorExplosion[1].SetActive(false);
    }
		
	// Update is called once per frame
	void Update () {
		IndicatorMechanic ();

        if (explodeLeft)
        {
            StartCoroutine(ExplosionLeft());
        }

        if (explodeRight)
        {
            StartCoroutine(ExplosionRight());
        }
    }

	void IndicatorPositionsandMovement(){
		offScreenIndicator[0].rectTransform.anchoredPosition = new Vector2(-320, -10);
		offScreenIndicator[1].rectTransform.anchoredPosition = new Vector2(320, -10);

		LeanTween.scale(offScreenIndicator[0].GetComponent<RectTransform>(), offScreenIndicator[0].GetComponent<RectTransform>().localScale * 0.5f, 0.15f).setLoopPingPong();
		LeanTween.scale(offScreenIndicator[1].GetComponent<RectTransform>(), offScreenIndicator[1].GetComponent<RectTransform>().localScale * 0.5f, 0.15f).setLoopPingPong();
	}
		
	void IndicatorMechanic(){
		if (!referencesScript.gameStartCoundownScript.gameFinishedWin) {
			//Right Screen Indicator
			if (offScreenCountRight < 0) {
				offScreenCountRight = 0;
			}
			if (offScreenCountRight > 0) {
				offScreenIndicator [1].enabled = true;
			} else {
				offScreenIndicator [1].enabled = false;
			}

			//Left Screen Indicator
			if (offScreenCountLeft < 0) {
				offScreenCountLeft = 0;
			}
			if (offScreenCountLeft > 0) {
				offScreenIndicator [0].enabled = true;
			} else {
				offScreenIndicator [0].enabled = false;
			}
		} else {
			offScreenIndicator [0].enabled = false;
			offScreenIndicator [1].enabled = false;
		}
	}

    IEnumerator ExplosionLeft()
    {
        if (indicatorExplosion[0].activeInHierarchy == false)
        {
            indicatorExplosion[0].SetActive(true);
            yield return new WaitForSeconds(.5f);
            indicatorExplosion[0].SetActive(false);
            explodeLeft = false;
        }
        explodeLeft = false;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator ExplosionRight()
    {
        if (indicatorExplosion[1].activeInHierarchy == false)
        {
            indicatorExplosion[1].SetActive(true);
            yield return new WaitForSeconds(.5f);
            indicatorExplosion[1].SetActive(false);
            explodeRight = false;
        }
        explodeRight = false;
        yield return new WaitForEndOfFrame();
    }
}

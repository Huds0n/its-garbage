using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFuelDrop : MonoBehaviour {

    Hammer hammerScript;

    public GameObject rubbishUI;

    float position;
    public GameObject test;
  
	// Use this for initialization
	void Start () {
        hammerScript = GameObject.Find("Club").GetComponent<Hammer>();

	}

    // Update is called once per frame
    void Update() {
        position = GetComponent<RectTransform>().sizeDelta.y * GetComponent<Image>().fillAmount;
           
        test.transform.position = new Vector3(position + 2, gameObject.transform.position.y, gameObject.transform.position.z);


        if (hammerScript.badEnemyHit == true)
        {
          
            GameObject rubbish = Instantiate(rubbishUI, gameObject.transform.position, Quaternion.identity);
            rubbish.transform.SetParent(gameObject.transform, false);
            rubbish.transform.position = gameObject.transform.position;
            hammerScript.badEnemyHit = false;
        }
       
	}

}

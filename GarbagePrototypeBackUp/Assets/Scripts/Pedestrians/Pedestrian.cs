using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour {

    public float speed;

    public bool opposite;
    
    Quaternion facingDifferentWay;

    public Animator animator;

    public ReferencedScripts referencesScript;

	public Texture[] textures;
	public Renderer[] rend;
	int oneRandomNumber;
	int twoRandomNumber;

    public void PedestrianRefsValues () {
        facingDifferentWay = Quaternion.Euler(0f, 180f, 0f);

        referencesScript = GameObject.Find("References").GetComponent<ReferencedScripts>();
    }

    public void PedestrianFacingWay()
    {
		if (!opposite) {
			transform.rotation = facingDifferentWay;
		} 
    }

    public void PedestrianMovement()
    {
        transform.Translate(new Vector3(-speed, 0, 0) * Time.fixedDeltaTime);
    }

    public void PedestrianSpeeds()
    {
        //Old Man Speed
        if (gameObject.name == "BadOldMan(Clone)" || gameObject.name == "GoodOldMan(Clone)") 
        {
            speed = .5f;
        }

        //Pram Speed
        if (gameObject.name == "DadWithPram(Clone)" || gameObject.name == "DadWithPramBad(Clone)")
        {
            speed = 1;
        }

        //Man Speed
        if(gameObject.name == "HitPed1(Clone)" || gameObject.name == "DontHitPed(Clone)" || gameObject.name == "DontHitPed No Book(Clone)" || gameObject.name == "GoodDadBook(Clone)")
        {
            speed = 3;
        }

        //Business Woman Speed
        if(gameObject.name == "BadBusinessWoman(Clone)" || gameObject.name == "GoodBusinessWoman(Clone)")
        {
            speed = 1.25f;
        }
        //Skater
        if (gameObject.name == "BadSkater(Clone)" || gameObject.name == "GoodSkater(Clone)" || gameObject.name == "GoodSkaterDonut(Clone)" || gameObject.name == "GoodSkaterSchool(Clone)")
        {
            speed = 3.2f;
        }
        //Bully
        if (gameObject.name == "BadBully(Clone)" || gameObject.name == "GoodBully(Clone)")
        {
            speed = 1.2f;
        }
        //Teacher
        if (gameObject.name == "BadTeacher(Clone)" || gameObject.name == "GoodTeacher(Clone)")
        {
            speed = 1.25f;
        }
        //Cop
        if (gameObject.name == "BadCop(Clone)" || gameObject.name == "GoodCop(Clone)")
        {
            speed = 2f;
        }
    }

    public void PedestrianSpeedChange()
    {
        if (referencesScript.playerMainScript.fuelImage.fillAmount >= .75f && speed < 3)
        {
            speed = speed + 1;
        }
    }

    public void PedestrianAnimationSpeed () {
        animator = GetComponentInChildren<Animator>();
        if (speed == 1)
        {
            animator.speed = 0.8f;
        }
        else if (speed == 2)
        {
            animator.speed = 1f;
        }
        else if (speed == 3)
        {
            animator.speed = 1.5f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

	public void MaterialChange(){
		rend = GetComponentsInChildren<Renderer> ();
		//Debug.Log (textures.Length);

		if (gameObject.name == "DontHitPed(Clone)" || gameObject.name == "HitPed1(Clone)" || gameObject.name == "DontHitPed No Book(Clone)" || gameObject.name == "GoodDadBook(Clone)") {
			oneRandomNumber = Random.Range (0, textures.Length);
			twoRandomNumber = Random.Range (0, textures.Length);

			rend [0].material.mainTexture = textures [oneRandomNumber];
			rend [1].material.mainTexture = textures [twoRandomNumber];
		}

		if(gameObject.name == "GoodOldMan(Clone)"){
			//1 newspaper 
			rend [0].material.mainTexture = textures [4];

			oneRandomNumber = Random.Range (0, 4);
			twoRandomNumber = Random.Range (5, textures.Length);

			//2 body
			rend [1].material.mainTexture = textures [oneRandomNumber];
			rend [2].material.mainTexture = textures [twoRandomNumber];

		}

		if(gameObject.name == "BadOldMan(Clone)"){
			oneRandomNumber = Random.Range (0, 4);
			twoRandomNumber = Random.Range (4, textures.Length);

			//2 body
			rend [0].material.mainTexture = textures [oneRandomNumber];
			rend [1].material.mainTexture = textures [twoRandomNumber];
		}

		if (gameObject.name == "DadWithPramBad(Clone)") {
			oneRandomNumber = Random.Range (0, 7);
			twoRandomNumber = Random.Range (0, 7);
	
			//body
			rend [0].material.mainTexture = textures [oneRandomNumber];
			//hair
			rend [1].material.mainTexture = textures [twoRandomNumber];

			int threeRandomNumber = Random.Range (7, 10);
			//3 for pram
			rend[2].material.mainTexture = textures [threeRandomNumber];
			rend[3].material.mainTexture = textures [threeRandomNumber];
			rend[4].material.mainTexture = textures [threeRandomNumber];
		}

		if(gameObject.name == "DadWithPram(Clone)"){
			//newspaper 
			rend [0].material.mainTexture = textures [10];

			oneRandomNumber = Random.Range (0, 7);
			twoRandomNumber = Random.Range (0, 7);

			int threeRandomNumber = Random.Range (7, 10);
			//body
			rend [1].material.mainTexture = textures [oneRandomNumber];
			//hair
			rend [2].material.mainTexture = textures [twoRandomNumber];
		    
			//3 for pram
			rend[3].material.mainTexture = textures [threeRandomNumber];
			rend[4].material.mainTexture = textures [threeRandomNumber];
			rend[5].material.mainTexture = textures [threeRandomNumber];
		}

        if (gameObject.name == "BadBusinessWoman(Clone)" || gameObject.name == "GoodBusinessWoman(Clone)")
        {
            oneRandomNumber = Random.Range(0, 3);
            twoRandomNumber = Random.Range(3, textures.Length);

            //body
            rend[1].material.mainTexture = textures[oneRandomNumber];
            //hair
            rend[2].material.mainTexture = textures[twoRandomNumber];

        }

        if(gameObject.name == "BadSkater(Clone)")
        {
            oneRandomNumber = Random.Range(0, 4);
            twoRandomNumber = Random.Range(4, textures.Length);

            rend[0].material.mainTexture = textures[twoRandomNumber];

            rend[1].material.mainTexture = textures[oneRandomNumber];
            rend[2].material.mainTexture = textures[oneRandomNumber];
            rend[3].material.mainTexture = textures[oneRandomNumber];
            rend[4].material.mainTexture = textures[oneRandomNumber];
            rend[5].material.mainTexture = textures[oneRandomNumber];         
        }

        if(gameObject.name == "GoodSkater(Clone)" || gameObject.name == "GoodSkaterSchool(Clone)")
        {
            oneRandomNumber = Random.Range(0, 4);
            twoRandomNumber = Random.Range(4, textures.Length);

            rend[0].material.mainTexture = textures[twoRandomNumber];

            rend[2].material.mainTexture = textures[oneRandomNumber];
            rend[3].material.mainTexture = textures[oneRandomNumber];
            rend[4].material.mainTexture = textures[oneRandomNumber];
            rend[5].material.mainTexture = textures[oneRandomNumber];
            rend[6].material.mainTexture = textures[oneRandomNumber];
        }
        if (gameObject.name == "BadBully(Clone)")
        {
            oneRandomNumber = Random.Range(0, 3);
            twoRandomNumber = Random.Range(4, 6);

            //body
            rend[0].material.mainTexture = textures[oneRandomNumber];
            //hair
            rend[1].material.mainTexture = textures[twoRandomNumber];
        }
        if (gameObject.name == "GoodBully(Clone)")
        {
            oneRandomNumber = Random.Range(0, 2);
            twoRandomNumber = Random.Range(3, 5);

            //book
            rend[0].material.mainTexture = textures[6];
            //body
            rend[1].material.mainTexture = textures[oneRandomNumber];
            //hair
            rend[2].material.mainTexture = textures[twoRandomNumber];
        }
        if (gameObject.name == "BadTeacher(Clone)")
        {
            //hair
            oneRandomNumber = Random.Range(3,5);
            //body
            twoRandomNumber = Random.Range(0,2);
            //hair
            rend[0].material.mainTexture = textures[oneRandomNumber];
            //mesh
            rend[1].material.mainTexture = textures[twoRandomNumber];
        }
        if (gameObject.name == "GoodTeacher(Clone)")
        {
            //hair
            oneRandomNumber = Random.Range(3,6);
            //body
            twoRandomNumber = Random.Range(0,2);
            //book
            rend[0].material.mainTexture = textures[6];
            //book
            rend[1].material.mainTexture = textures[6];
            //hair
            rend[2].material.mainTexture = textures[oneRandomNumber];
            //body
            rend[3].material.mainTexture = textures[twoRandomNumber];

        }
        if (gameObject.name == "GoodCop(Clone)")
        {
            //body
            oneRandomNumber = Random.Range(0, 3);
            twoRandomNumber = Random.Range(3, textures.Length);

            //hat
            rend[0].material.mainTexture = textures[3];
            //donutbag
            rend[1].material.mainTexture = textures[5];
            //segway
            rend[2].material.mainTexture = textures[4];
            //wheels
            rend[3].material.mainTexture = textures[4];
            //hair
            rend[4].material.mainTexture = textures[6];
            //mesh
            rend[5].material.mainTexture = textures[oneRandomNumber];
            //badge
            rend[6].material.mainTexture = textures[7];
        }
        if (gameObject.name == "BadCop(Clone)")
        {
            //body
            oneRandomNumber = Random.Range(0, 3);
            twoRandomNumber = Random.Range(3, textures.Length);

            //hat
            rend[0].material.mainTexture = textures[3];
            //segway
            rend[1].material.mainTexture = textures[5];
            //wheels
            rend[2].material.mainTexture = textures[5];
            //hair
            rend[3].material.mainTexture = textures[5];
            //mesh
            rend[4].material.mainTexture = textures[oneRandomNumber];
            //badge
            rend[5].material.mainTexture = textures[6];
        }
    }
}
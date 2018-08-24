using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPedestrians : MonoBehaviour {

    PedestrianPooler pedestrianPooler;

    int pedestrianSpeed;

    public GameObject[] spawners;

    //public float spawnTime = 2f;

    public string enemyType;

    public float spawnTimeDelay = 3f;
    float timeStamp;
   
    
    [Header("References Script")]
    public ReferencedScripts referencesScript;

    private void Start()
    {
        pedestrianPooler = PedestrianPooler.Instance;

        //InvokeRepeating("Spawn", 0f, spawnTime);

        timeStamp = Time.time + spawnTimeDelay;
    }
    private void FixedUpdate()
    {
		if(timeStamp < Time.time && referencesScript.gameStartCoundownScript.inTutorial == false)
        {
            
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject go;
        //Spawn Walker

        //choose type of pedestrian
        int typeOfEnemy = Random.Range(0, 6);
        switch(typeOfEnemy)
        {
            case 0:
                enemyType = "CANT Hit Ped";
                break;
            case 1:
                enemyType = "Can Hit Ped";
                break;
            case 2:
                enemyType = "Bad Pram";
                break;
            case 3:
                enemyType = "Good Pram";
                break;
            case 4:
                enemyType = "Bad Old Man";
                break;
            case 5:
                enemyType = "Good Old Man";
                break;
        }

        //choose spawn location
        int spawnPointIndex = Random.Range(0, 2);
        //Debug.Log(spawnPointIndex);

        //spawn pedestrian
        go = pedestrianPooler.SpawnFromPool(enemyType, spawners[spawnPointIndex].transform.position, spawners[spawnPointIndex].transform.rotation) as GameObject;

        //if spawning from second spawner, turn the pedestrian the opposite way, to walk as intended
        if (spawnPointIndex == 1)
        {
            if (go.GetComponent<BadHitPed>() != null)
            go.GetComponent<BadHitPed>().opposite = true;
            else
                go.GetComponent<CanHitPed>().opposite = true;
        }
        else
        {
            if (go.GetComponent<BadHitPed>() != null)
                go.GetComponent<BadHitPed>().opposite = false;
            else
                go.GetComponent<CanHitPed>().opposite = false;
        }

        timeStamp += spawnTimeDelay;
    }
}

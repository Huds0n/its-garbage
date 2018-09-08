using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPedestrians : MonoBehaviour {

    PedestrianPooler pedestrianPooler;

    int pedestrianSpeed;

    public GameObject[] spawners;

    public string enemyType;

    public float spawnTimeDelay = 3f;
    float timeStamp;

    Scene scene;
    
    [Header("References Script")]
    public ReferencedScripts referencesScript;

    private void Start()
    {
        pedestrianPooler = PedestrianPooler.Instance;

        timeStamp = Time.time + spawnTimeDelay;

        scene = SceneManager.GetActiveScene();
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

        if (scene.name == "Level1")
        {
            int typeOfEnemy = Random.Range(0, 10);
            switch (typeOfEnemy)
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
                case 6:
                    enemyType = "Bad Business Woman";
                    break;
                case 7:
                    enemyType = "Good Business Woman";
                    break;
                case 8:
                    enemyType = "Bad Skater";
                    break;
                case 9:
                    enemyType = "Good Skater";
                    break;
            }
        }

        if (scene.name == "Level2")
        {
            int typeOfEnemy = Random.Range(0, 8);
            switch (typeOfEnemy)
            {
                case 0:
                    enemyType = "CANT Hit Ped";
                    break;
                case 1:
                    enemyType = "Can Hit Ped";
                    break;
                case 2:
                    enemyType = "Bad Bully";
                    break;
                case 3:
                    enemyType = "Good Bully";
                    break;
                case 4:
                    enemyType = "Bad Teacher";
                    break;
                case 5:
                    enemyType = "Good Teacher";
                    break;
                case 6:
                    enemyType = "Bad Skater";
                    break;
                case 7:
                    enemyType = "Good Skater";
                    break;
            }
        }

        if (scene.name == "Level3")
        {
            int typeOfEnemy = Random.Range(0, 8);
            switch (typeOfEnemy)
            {
                case 0:
                    enemyType = "CANT Hit Ped";
                    break;
                case 1:
                    enemyType = "Can Hit Ped";
                    break;
                case 2:
                    enemyType = "Bad Skater";
                    break;
                case 3:
                    enemyType = "Good Skater";
                    break;
                case 4:
                    enemyType = "Bad Cop";
                    break;
                case 5:
                    enemyType = "Good Cop";
                    break;
                case 6:
                    enemyType = "Bad Chubby Lady";
                    break;
                case 7:
                    enemyType = "Good Chubby Lady";
                    break;

            }
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

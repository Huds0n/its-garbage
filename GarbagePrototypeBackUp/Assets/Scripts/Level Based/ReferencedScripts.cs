using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencedScripts : MonoBehaviour {
    
    public GameObject playerObject;
    public Transform playerTransform;
    public PlayerMain playerMainScript;
    public GameStartCountdown gameStartCoundownScript;
    public Squash squashScript;
    public Hammer hammerScript;
    //public SpawnPedestrians spawnPedestriansScript;
    public CameraShake cameraShake;
	public CameraFollow cameraFollow;
	public Lives livesScript;

    private void Awake()
    {
        
    }
	void Update(){
		
	}
}

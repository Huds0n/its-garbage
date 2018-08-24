using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

	public GameObject player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

	Vector3 velocity = Vector3.zero;

	public Indicators indicatorScript;

    public ReferencedScripts referencesScript;
	void Start(){
		player = GameObject.Find ("Player");
	}
    private void FixedUpdate()
    {
        if (referencesScript.gameStartCoundownScript.gameFinishedWin == false)
        {
            if (player != null && player.GetComponent<PlayerMain>().justDied == false)
            {
                target = player.transform;
            }
            else
            {
                GameObject[] pedestrians = GameObject.FindGameObjectsWithTag("Pedestrians");
                foreach (GameObject pedestrian in pedestrians)
                {
                    pedestrian.SetActive(false);
                }
                indicatorScript.offScreenCountRight = 0;
                indicatorScript.offScreenCountLeft = 0;
                target = null;
            }
            //if player is not empty/null, follow player 
            if (target != null)
            {
                Vector3 desiredPosition = target.position + offset;
                //Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.deltaTime);
                transform.position = smoothPosition;

                //transform.LookAt(target);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Vector3 checkpointPos;
    private Vector3 camCheckpointPos;
    private Transform myTransform;
    private Transform camTransform;
	void Start () {
        camTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        myTransform = gameObject.GetComponent<Transform>();
        checkpointPos = myTransform.position;
        camCheckpointPos = camTransform.position;
	}
	
    public void Respawn()
    {
		gameObject.GetComponent<KeyboardMovement> ().ResetVerticalVelocity ();
        myTransform.position = checkpointPos;
        camTransform.position = camCheckpointPos;
    }

    public void SetCheckpoint()
    {
        checkpointPos = myTransform.position;
        camCheckpointPos = camTransform.position;
    }
}

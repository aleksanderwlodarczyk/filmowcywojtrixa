using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Vector3 checkpointPos;
    private Vector3 camCheckpointPos;
    private Transform myTransform;
    private Transform camTransform;

    private bool oneHitShield = false;
    private GameObject shield;
	void Start () {
        camTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        myTransform = gameObject.GetComponent<Transform>();
        checkpointPos = myTransform.position;
        camCheckpointPos = camTransform.position;
        shield = gameObject.transform.GetChild(1).gameObject;
        shield.SetActive(false);
        StartCoroutine(CheckIfHaveShield());
        
    }
    
    public void GiveShield()
    {
        oneHitShield = true;
    }
	
    public void Respawn()
    {
        if (oneHitShield)
        {
            oneHitShield = false;
            shield.SetActive(false);
        }
        else
        {
            int score = GameObject.Find("scoreHandler").GetComponent<Score>().ScoreAmount;
            GameObject.Find("GameStateHandler").GetComponent<GameOver>().EndGame(score, true);
        }


		// gameObject.GetComponent<KeyboardMovement> ().ResetVerticalVelocity ();
        // myTransform.position = checkpointPos;
        // camTransform.position = camCheckpointPos;
    }

    public void SetCheckpoint()
    {
        checkpointPos = myTransform.position;
        camCheckpointPos = camTransform.position;
    }

    IEnumerator CheckIfHaveShield()
    {
        yield return new WaitForSeconds(2.5f);
        if (oneHitShield)
        {
            shield.SetActive(true);
        }

    }
}

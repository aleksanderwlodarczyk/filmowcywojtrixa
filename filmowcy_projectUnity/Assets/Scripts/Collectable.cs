using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public int scoreToAdd;

	private Score scoreHandler;

	void Start(){
		scoreHandler = GameObject.Find ("scoreHandler").GetComponent<Score> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			scoreHandler.AddScore (scoreToAdd);
			Destroy (gameObject);
		}
	}

}

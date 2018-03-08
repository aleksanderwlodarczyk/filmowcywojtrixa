using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEffect : MonoBehaviour {

	private EffectsControlling camera;
	public float value;
	public int effect;

	void Start(){
		camera = GameObject.Find ("Main Camera").GetComponent<EffectsControlling> ();
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Player") {
			camera.ChangeCameraEffect (effect, value);
			Destroy (gameObject);
		}

	}
}

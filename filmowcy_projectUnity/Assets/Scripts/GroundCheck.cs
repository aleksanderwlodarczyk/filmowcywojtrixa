using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	public string side;

	private AIAutoMove mover;

	void Start(){
		mover = transform.parent.gameObject.GetComponent<AIAutoMove> ();
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.name == "Tilemap") {
			if (side == "left") {
				mover.canGoLeft = false;
			} else {
				mover.canGoRight = false;
			}
		} else {
			if (side == "left") {
				mover.canGoLeft = true;
			} else {
				mover.canGoRight = true;
			} 
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.name == "Tilemap") {
			if (side == "left") {
				mover.canGoLeft = true;
			} else {
				mover.canGoRight = true;
			}
		}
	}
		


}

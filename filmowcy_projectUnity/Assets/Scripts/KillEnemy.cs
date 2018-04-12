using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour {

	private GameObject enemy;

	void Start () {
		enemy = transform.parent.gameObject;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			enemy.GetComponent<KillPlayer> ().enabled = false;
			Destroy (enemy);
		}
	}
}

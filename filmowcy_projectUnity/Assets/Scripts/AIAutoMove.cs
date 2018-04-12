using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIAutoMove : MonoBehaviour{

	public int speed;
	public bool canGoRight;
	public bool canGoLeft;

	private int dir;
	private Rigidbody2D rb2d;

	public int minTime;
	public int maxTime;

	int RandomDirection(){
		int random = Random.Range (0, 100);
		if (random >= 50)
			return 1;
		else
			return -1;
	}

	int RandomTime(){
		return Random.Range (minTime, maxTime);
	}

	void Awake(){
		canGoLeft = true;
		canGoRight = true;
	}

	void Start(){

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		StartCoroutine (Randomizing ());
	}


	void FixedUpdate(){
		if ((dir == 1 && canGoRight) || (dir == -1 && canGoLeft))
			rb2d.velocity = Vector3.right * speed * dir;
		else {
			rb2d.velocity = Vector3.zero;
		}

	}

	IEnumerator Randomizing(){
		while (true) {
			dir = RandomDirection ();

			yield return new WaitForSeconds (RandomTime());
		}
	}
}



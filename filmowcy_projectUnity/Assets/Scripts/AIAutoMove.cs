using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIAutoMove : MonoBehaviour{

	public int speed;
	public bool CanGoRight;
	public bool CanGoLeft;

	char RandomDirection(){
		int random = Random.Range (0, 100);
		if (random >= 50)
			return 'r';
		else
			return 'l';
	}

	int RandomTime(){
		return Random.Range (1, 4);
	}

	void Start(){
		
	}


	void FixedUpdate(){

	}
}



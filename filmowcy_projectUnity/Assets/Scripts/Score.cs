using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	private int score;

	public int ScoreAmount{
		get { return score;}
	}

	void Start(){
		score = 0;
	}

	public void AddScore(int amount){
		score += amount;
		Debug.Log (score);
	}
}

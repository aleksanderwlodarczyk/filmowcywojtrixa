using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int score;
	private Text scoreText;


	public int ScoreAmount{
		get { return score;}
	}

	void Start(){
		score = 0;
		scoreText = GameObject.Find ("scoreText").GetComponent<Text> ();
		UpdateScoreText ();
	}

	public void AddScore(int amount){
		score += amount;
		UpdateScoreText ();
	}

	private void UpdateScoreText(){
		scoreText.text = score.ToString();
	}
}

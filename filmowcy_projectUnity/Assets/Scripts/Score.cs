using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int score;
    private int currency;
	private Text scoreText;


	public int ScoreAmount{
		get { return score;}
	}

    public int PremiumCurrencyAmount
    {
        get { return currency; }
    }

    void Start(){
		score = 0;
		currency = 0;
		scoreText = GameObject.Find ("scoreText").GetComponent<Text> ();
		UpdateScoreText ();
	}

	public void AddScore(int amount){
		score += amount;
		UpdateScoreText ();
	}

    public void AddPremiumCurrency(int amount)
    {
        currency += amount;
    }

	private void UpdateScoreText(){
		scoreText.text = score.ToString();
	}
}

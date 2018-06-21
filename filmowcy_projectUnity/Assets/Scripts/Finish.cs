using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    private SceneManaging sceneManage;
    private GameOver gOver;
    private Score scoreObject;

	void Start () {
        scoreObject = GameObject.Find("scoreHandler").GetComponent<Score>();
        sceneManage = GameObject.Find("sceneHandler").GetComponent<SceneManaging>();
        gOver = GameObject.Find("GameStateHandler").GetComponent<GameOver>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            int score = scoreObject.ScoreAmount;
            int currency = scoreObject.PremiumCurrencyAmount;
            gOver.EndGame(score, currency, false);
        }
    }
}

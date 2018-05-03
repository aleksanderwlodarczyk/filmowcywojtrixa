using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    private SceneManaging sceneManage;
    private GameOver gOver;

	void Start () {
		sceneManage = GameObject.Find("sceneHandler").GetComponent<SceneManaging>();
        gOver = GameObject.Find("GameStateHandler").GetComponent<GameOver>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            int score = GameObject.Find("scoreHandler").GetComponent<Score>().ScoreAmount;
            gOver.EndGame(score, false);
        }
    }
}

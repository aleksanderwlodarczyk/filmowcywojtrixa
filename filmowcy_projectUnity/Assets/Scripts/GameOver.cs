using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject loseScreen;

	private int score;
	private GameObject leaderboard;
	private FirebaseFlow firebase;
	void Start () {
		firebase = GameObject.Find("Firebase").GetComponent<FirebaseFlow>();
	}

	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Die(int points){
		score = points;
		Time.timeScale = 0;
		loseScreen.SetActive(true);
		leaderboard = GameObject.Find("leaderboard");
		for(int i = 0; i < 3; i++){
			leaderboard.transform.GetChild(i).gameObject.GetComponent<Text>().text = firebase.RecordName(i+1) + "\t" + firebase.RecordScore(i+1).ToString();
		}
	}

	public void SaveScore(){
		string name = GameObject.Find("nameInput").transform.GetChild(1).gameObject.GetComponent<Text>().text;
		firebase.SendRecord(name, score);
	}
}

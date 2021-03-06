﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class GameOver : MonoBehaviour {

	public GameObject loseScreen;
	public bool lastLevel;
    public List<Text> leaderboard = new List<Text>();
    public int levelID;

    private DatabaseReference reference;
    private Battery battery;
	private int score;
    private int premiumCurrencyGet;
    private string[] scores = new string[3];
    private string[] names = new string[3];
    private FirebaseFlow firebase;
    private FacebookFlow facebook;
    private Text endScoreText;
    private Text endCashText;
    private Text endBatteryText;
    private Text endScreenTitle;
    private SceneManaging sManager;
    private bool leadersDownloaded;
    private bool nextLevel;

    void Start () {

        leadersDownloaded = false;
		firebase = GameObject.Find("Firebase").GetComponent<FirebaseFlow>();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://filmowcy-wojtrixa.firebaseio.com/");
        FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("filmowcy-wojtrixa@appspot.gserviceaccount.com");
        
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        facebook = GameObject.Find("Facebook").GetComponent<FacebookFlow>();
        battery = GameObject.Find("Body").GetComponent<Battery>();
        endScoreText = loseScreen.transform.GetChild(1).gameObject.GetComponent<Text>();
        endCashText = loseScreen.transform.GetChild(3).gameObject.GetComponent<Text>();
        endBatteryText = loseScreen.transform.GetChild(5).gameObject.GetComponent<Text>();
        endScreenTitle = loseScreen.transform.GetChild(6).gameObject.GetComponent<Text>();
        sManager = GameObject.Find("sceneHandler").GetComponent<SceneManaging>();

		//unlock current level
	}

	public void EndGame(int points, int premiumCurrency, bool died){
        if (died)
        {
            endScreenTitle.text = "You Died";
            nextLevel = false;
        }
        else
        {
            endScreenTitle.text = "Level Completed";
            nextLevel = true;
        }

        GetRecords();
        premiumCurrencyGet = premiumCurrency;
        score = points * (int)battery.power;
        endScoreText.text = "score: " + score.ToString();
        endCashText.text = points.ToString();
        endBatteryText.text = ((int)battery.power).ToString();
		Time.timeScale = 0;
        loseScreen.SetActive(true);

        Debug.Log("r Count: " + firebase.records);
        firebase.SetUserMoney(facebook.fbID, premiumCurrency, true);
        firebase.SetUserProgress(facebook.fbID, levelID, score);
        //leaderboard.transform.GetChild(i).gameObject.GetComponent<Text>().text = firebase.RecordName(i+1) + "\t" + firebase.RecordScore(i+1).ToString();
    }

    void GetRecords()
    {
        string path = "/scoreboard/" + (firebase.records - 2).ToString();
        
        FirebaseDatabase.DefaultInstance.GetReference(path).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("error");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log(snapshot.Child("score").ToString());

                    scores[0] = snapshot.Child("score").Value.ToString();
                    names[0] = snapshot.Child("name").Value.ToString();
                }
            });
        path = "/scoreboard/" + (firebase.records - 1).ToString();
        
        FirebaseDatabase.DefaultInstance.GetReference(path).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Child("score").ToString());

                scores[1] = snapshot.Child("score").Value.ToString();
                names[1] = snapshot.Child("name").Value.ToString();
            }
        });
        path = "/scoreboard/" + (firebase.records).ToString();
       
        FirebaseDatabase.DefaultInstance.GetReference(path).GetValueAsync().ContinueWith(task =>
        {
            
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Child("score").ToString());

                scores[2] = snapshot.Child("score").Value.ToString();
                Debug.Log(scores[2]);
                names[2] = snapshot.Child("name").Value.ToString();

                leadersDownloaded = true;
            }
        });

        
    }

    private void Update()
    {
        if(leadersDownloaded) ShowLeaderBoard(); 
    }
    void ShowLeaderBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            leaderboard[i].text = names[i] + "\t" + scores[i];
            Debug.Log(scores[i]);
        }
        leadersDownloaded = false;
    }

	public void SaveScore(){
        //string nick = GameObject.Find("nameInput").transform.GetChild(1).gameObject.GetComponent<Text>().text; // change to 1 when building on andro
        //string nick = "phone1debug";
        string nick = facebook.fbName;
		firebase.SendRecord(nick, score);
		if (nextLevel)
		{
			if (lastLevel)
			{
				sManager.LoadLevel(sManager.ShopLevelIndex);
			}
			else
			{
				sManager.LoadLevel(sManager.CurrentLevel + 1);
			}
		}
		else sManager.LoadLevel(sManager.CurrentLevel);
    }

    public void NoSaveScore()
    {
		if (nextLevel)
		{
			if (lastLevel)
			{
				sManager.LoadLevel(sManager.ShopLevelIndex);
			}
			else
			{
				sManager.LoadLevel(sManager.CurrentLevel + 1);
			}
		}
		else sManager.LoadLevel(sManager.CurrentLevel);
    }
}

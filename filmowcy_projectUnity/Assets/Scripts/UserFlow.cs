using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Threading.Tasks;

public class UserFlow : MonoBehaviour {

    public int cash;
    public string loggedUser;
    public bool playingScene;
    public bool shopScene;

	public bool doubleJump;
    public bool extendedBat;
    public bool oneHitShield;
    public Task storeItemsChecking;

    private KeyboardMovement playerMovement;
    private Player player;
    private Battery playerBattery;
    private FirebaseFlow firebase;

    public Progress progress;

	void Awake () {
        if (playingScene)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardMovement>();
            playerBattery = GameObject.FindGameObjectWithTag("Body").GetComponent<Battery>();
        }
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://filmowcy-wojtrixa.firebaseio.com/");
        FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("filmowcy-wojtrixa@appspot.gserviceaccount.com");

        firebase = GameObject.Find("Firebase").GetComponent<FirebaseFlow>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            firebase.SetUserMoney(loggedUser, 10, true);
            RefreshUsersMoney(loggedUser);
        }
    }

    public void RefreshUsersMoney(string facebookID)
    {
        FirebaseDatabase.DefaultInstance.GetReference("/users_money").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.Log("error!");
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                IDictionary<string, object> data = (IDictionary<string, object>)snapshot.Value;

                foreach (KeyValuePair<string, object> p in data)
                {
                    Debug.Log("key " + p.Key);
                    Debug.Log("value" + int.Parse(p.Value.ToString()));

                    if (p.Key == facebookID)
                    {
                        cash = int.Parse(p.Value.ToString());
                    }
                }
            }
        });

    }

    public void LoginUser(string facebookID)
    {
        //get cash from database
        Debug.Log("logging user" + facebookID);
        bool userExist = false;
        FirebaseDatabase.DefaultInstance.GetReference("/users_money").GetValueAsync().ContinueWith(task =>
        {
            Debug.Log("task started");
            if (task.IsFaulted) Debug.Log("error!");
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.ChildrenCount);
                IDictionary<string, object> data = (IDictionary<string, object>)snapshot.Value;

                foreach(KeyValuePair<string, object> p in data)
                {
                    Debug.Log("key " + p.Key);
                    Debug.Log("value" + int.Parse(p.Value.ToString()));

                    if(p.Key == facebookID)
                    {
                        loggedUser = p.Key;
                        userExist = true;
                        cash = int.Parse(p.Value.ToString());
						if (shopScene)
						{
							GameObject.FindObjectOfType<Shop>().Money = cash;
						}
                        break;
                    }
                }

                if (!userExist)
                {
                    Debug.Log("I have to register user");
                    RegisterUser(facebookID);
                }
            }
        });

        FirebaseDatabase.DefaultInstance.GetReference("/items/"+facebookID).GetValueAsync().ContinueWith(task =>
        {
            Debug.Log("task started");
            if (task.IsFaulted) Debug.Log("error!");
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.ChildrenCount);
                IDictionary<string, object> data = (IDictionary<string, object>)snapshot.Value;

                foreach (KeyValuePair<string, object> p in data)
                {
                    Debug.Log("key " + p.Key);
                    Debug.Log("value" + int.Parse(p.Value.ToString()));

                    if(p.Key == "doubleJump")
                    {
                        // will be saving these vals in player prefs in the future
                        doubleJump = p.Value.ToString() == "1" ? true : false;
                        if (playingScene)
                        {
                            playerMovement.doubleJump = doubleJump;
                        }
                        userExist = true;
                    }

                    if (p.Key == "oneHitShield")
                    {
                        // will be saving these vals in player prefs in the future
                        oneHitShield = p.Value.ToString() == "1" ? true : false;
                        if (playingScene)
                        {
                            if (oneHitShield) player.GiveShield();
                        }
                        userExist = true;
                    }

                    if (p.Key == "extendedBattery")
                    {
                        // will be saving these vals in player prefs in the future
                        extendedBat = p.Value.ToString() == "1" ? true : false;
                        if (playingScene)
                        {
                            if(extendedBat) playerBattery.ExtendBattery();

                        }
                        userExist = true;
                    }
                }

                if (!userExist)
                {
                    RegisterUser(facebookID);
                }
            }
        });

		FirebaseDatabase.DefaultInstance.GetReference("/users_progress/" + facebookID).GetValueAsync().ContinueWith(task =>
		{
			Debug.Log("task started");
			if (task.IsFaulted) Debug.Log("error!");
			else if (task.IsCompleted)
			{
				DataSnapshot snapshot = task.Result;
				Debug.Log(snapshot.ChildrenCount);
				IDictionary<string, object> data = (IDictionary<string, object>)snapshot.Value;

				foreach (KeyValuePair<string, object> p in data)
				{
					Debug.Log("key " + p.Key);
					Debug.Log("value" + int.Parse(p.Value.ToString()));

					progress.levelScore.Add(p.Key, int.Parse(p.Value.ToString()));
				}

				//progress.SetProgress();
			}
			if (!userExist)
			{
				RegisterUser(facebookID);
			}
		});

		//if (!userExist)
		//{
		//	Debug.Log("I have to register user");
		//	RegisterUser(facebookID);
		//}

	}

    void RegisterUser(string facebookID)
    {
        //if user does not exist create record with value 0
        Debug.Log("regging user");
        firebase.SetUserMoney(facebookID, 0, false);
        firebase.SetUserProgress(facebookID);
        firebase.SetUserItem(facebookID, "doubleJump", false);
        firebase.SetUserItem(facebookID, "extendedBattery", false);
        firebase.SetUserItem(facebookID, "oneHitShield", false);
        loggedUser = facebookID;
    }


}

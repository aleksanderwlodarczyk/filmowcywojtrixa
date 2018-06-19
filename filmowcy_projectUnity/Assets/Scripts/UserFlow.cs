using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class UserFlow : MonoBehaviour {

    public int cash;
    public string loggedUser;
    public bool playingScene;

    public bool doubleJump;
    public bool extendedBat;

    private KeyboardMovement playerMovement;
    private Battery playerBattery;
    private FirebaseFlow firebase;

	void Awake () {
        if (playingScene)
        {
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
            RefreshUsersMoney(loggedUser);
            firebase.SetUserMoney(loggedUser, cash + 10);
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
                        break;
                    }
                }

                if (!userExist)
                {
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

                    if (p.Key == "extendedBattery")
                    {
                        // will be saving these vals in player prefs in the future
                        extendedBat = p.Value.ToString() == "1" ? true : false;
                        if (playingScene)
                        {
                            playerBattery.ExtendBattery();
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

    }

    void RegisterUser(string facebookID)
    {
        //if user does not exist create record with value 0
        Debug.Log("regging user");
        firebase.SetUserMoney(facebookID, 0);
        firebase.SetUserItems(facebookID, false, false);
        loggedUser = facebookID;
    }


}

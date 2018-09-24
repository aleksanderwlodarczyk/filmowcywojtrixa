using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class FirebaseFlow : MonoBehaviour {

    private UserFlow user;

	public Text buildDebug;
    public DatabaseReference reference;
    public int records;

    public void RecordsCount(){
		
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard").GetValueAsync().ContinueWith(task =>
		{
			if (task.IsFaulted)
			{
				Debug.Log("error!");
				if (buildDebug)
					buildDebug.text += "\nRecordsCount Error";
			}
			else if (task.IsCompleted)
			{
				DataSnapshot snapshot = task.Result;
				Debug.Log((int)snapshot.ChildrenCount);
				records = (int)snapshot.ChildrenCount;
				if(buildDebug)
					buildDebug.text += "\n" + records.ToString();
			}
		});
	
	}
    private void Awake()
    {
        RecordsCount();
    }
    void Start () {
        user = GameObject.Find("Users").GetComponent<UserFlow>();
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://filmowcy-wojtrixa.firebaseio.com/");
		FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("filmowcy-wojtrixa@appspot.gserviceaccount.com");

    }
	
	//void ShowTestData(){

 //       reference.GetValueAsync().ContinueWith(task =>
 //       {
 //           if (task.IsFaulted) Debug.Log("error!");
 //           else if (task.IsCompleted)
 //           {
 //               DataSnapshot snapshot = task.Result;
 //               Debug.Log(snapshot.Child("scoreboard/1").Child("score").Value);
 //           }
 //       });

	//	//FirebaseDatabase.DefaultInstance.GetReference("/scoreboard").GetValueAsync().ContinueWith(task =>
	//	//{
	//	//	if(task.IsFaulted) Debug.Log("error!");
	//	//	else if(task.IsCompleted){
	//	//		DataSnapshot snapshot = task.Result;
	//	//		Debug.Log(snapshot.Child("1").Child("score").Value);
	//	//	}
	//	//});
	//}

	public string RecordName(int record){
        string nameX = "";
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/"+record.ToString()).GetValueAsync().ContinueWith(task =>
		{
			if(task.IsFaulted) Debug.Log("error!");
			else if(task.IsCompleted){
				DataSnapshot snapshot = task.Result;
                nameX = "dupa";
			}
		});
       
		return nameX;
	}

	public int RecordScore(int record){
		int score = -1;
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/"+record.ToString()).GetValueAsync().ContinueWith(task =>
		{
			if(task.IsFaulted) Debug.Log("error!");
			else if(task.IsCompleted){
				DataSnapshot snapshot = task.Result;
                score = Int32.Parse(snapshot.Child("score").ToString());
			}
		});
		return score;
	}
    public void SendRecord(string name, int score)
    {
        FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/" + (records + 1).ToString()).Child("name").SetValueAsync(name);
        FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/" + (records + 1).ToString()).Child("score").SetValueAsync(score);
    }

    public void SetUserMoney(string facebookID, int cash, bool adding)
    {
		user.RefreshUsersMoney(user.loggedUser);

        if (adding)
        {
            int toSet = user.cash + cash;
            FirebaseDatabase.DefaultInstance.GetReference("/users_money/" + facebookID).SetValueAsync(toSet);
        }
        else
        {
            FirebaseDatabase.DefaultInstance.GetReference("/users_money/" + facebookID).SetValueAsync(cash);
        }
    }

    public void SetUserProgress(string facebookID)
    {
        // temporary hard coded level count
        int levelCount = 3;
        string levelName = "";
        for (int i = 0; i < levelCount; i++)
        {
            levelName = "level" + (i + 2).ToString();
			if (i == 0)
				FirebaseDatabase.DefaultInstance.GetReference("/users_progress/" + facebookID + "/" + levelName).SetValueAsync(0);
			else
				FirebaseDatabase.DefaultInstance.GetReference("/users_progress/" + facebookID + "/" + levelName).SetValueAsync(-1);
        }
    }

    public void SetUserProgress(string facebookID, int levelID, int score)
    {
        string levelName = "level" + levelID.ToString();
        FirebaseDatabase.DefaultInstance.GetReference("/users_progress/" + facebookID + "/" + levelName).SetValueAsync(score);
    }

    public void SetUserItem(string facebookID, string itemName, bool value)
    {
        if (value) FirebaseDatabase.DefaultInstance.GetReference("/items/" + facebookID + "/" + itemName).SetValueAsync(1);
        else FirebaseDatabase.DefaultInstance.GetReference("/items/" + facebookID + "/" + itemName).SetValueAsync(0);
    }

}

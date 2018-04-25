using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class FirebaseFlow : MonoBehaviour {

	public int RecordsCount(){
		int res = -1;
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard").GetValueAsync().ContinueWith(task =>
		{
			if(task.IsFaulted) Debug.Log("error!");
			else if(task.IsCompleted){
				DataSnapshot snapshot = task.Result;
				res = (int)snapshot.ChildrenCount;
			}
		});
		return res;
	}
	
	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://filmowcy-wojtrixa.firebaseio.com/");
		FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("filmowcy-wojtrixa@appspot.gserviceaccount.com");

		ShowTestData();
	}
	
	void ShowTestData(){
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard").GetValueAsync().ContinueWith(task =>
		{
			if(task.IsFaulted) Debug.Log("error!");
			else if(task.IsCompleted){
				DataSnapshot snapshot = task.Result;
				Debug.Log(snapshot.Child("1").Child("score").Value);
			}
		});
	}

	public string RecordName(int record){
		string name = "";
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/"+record.ToString()).GetValueAsync().ContinueWith(task =>
		{
			if(task.IsFaulted) Debug.Log("error!");
			else if(task.IsCompleted){
				DataSnapshot snapshot = task.Result;
				name = snapshot.Child("name").Value.ToString();
				Debug.Log(snapshot.Child("name").GetValue(true).ToString());
			}
		});
		return name;
	}

	public int RecordScore(int record){
		int score = -1;
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/"+record.ToString()).GetValueAsync().ContinueWith(task =>
		{
			if(task.IsFaulted) Debug.Log("error!");
			else if(task.IsCompleted){
				DataSnapshot snapshot = task.Result;
				score = Int32.Parse(snapshot.Child("score").GetRawJsonValue());
			}
		});
		return score;
	}
	public void SendRecord(string name, int score){
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/"+ (RecordsCount()+1).ToString()).Child("name").SetValueAsync(name);
		FirebaseDatabase.DefaultInstance.GetReference("/scoreboard/"+ (RecordsCount()+1).ToString()).Child("score").SetValueAsync(score);
	}

}

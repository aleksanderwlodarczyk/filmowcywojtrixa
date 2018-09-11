using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public int Money { get; set; }
	public FirebaseFlow firebase;  // to set money and items

	void Start () {
		
	}
	

	void Update () {
		
	}

	public void BuyItem(int item)
	{
		//subtract money
		//update money in firebase
		//update items in firebase
	}
}

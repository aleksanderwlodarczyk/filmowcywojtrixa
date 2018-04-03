using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCharge : MonoBehaviour {

    public float mAh;

    private Battery batteryHandler;
	void Start () {
        batteryHandler = GameObject.Find("BatteryController").GetComponent<Battery>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            batteryHandler.AddPower(mAh);
        }
    }
}

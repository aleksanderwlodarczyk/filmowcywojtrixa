using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPickup : MonoBehaviour {

	public bool canHeat;

	private Heat bodyHeat;

	private void Start()
	{
		bodyHeat = GameObject.Find("Body").GetComponent<Heat>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player")
		{
			bodyHeat.heating = canHeat;
			Destroy(gameObject);
		}

	}
}

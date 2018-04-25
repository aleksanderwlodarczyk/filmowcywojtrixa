using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heat : MonoBehaviour {

    public Text UIText;
    public Text debugConsole;
    public float heatingMultiplier;
	public float coolingMultiplier;
    public bool overheated;
    public GameObject overheatMessage;
	public float maxTemperature;
	public float startingTemperature;

	private Rigidbody2D playerRb2D;
    private float temperature;
	private bool cooling;

	void Start () {
		cooling = false;
		playerRb2D = GameObject.Find ("player").GetComponent<Rigidbody2D> ();
        overheated = false;
        temperature = startingTemperature;
    }
	
	
	void Update () {
		if (!overheated) {
			if (playerRb2D.velocity.magnitude != 0) {
				temperature += Time.deltaTime * heatingMultiplier;
			} else if(temperature > 23) {
				temperature -= Time.deltaTime * coolingMultiplier;
			}

		} else {
			if (!cooling)
				StartCoroutine (Cooling ());
		}

		if (temperature > maxTemperature)
			overheated = true;

        UIText.text = Mathf.Round(temperature).ToString() + "°C"; // print it to ui no debug
    }

	IEnumerator Cooling(){
		cooling = true;
		playerRb2D.simulated = false;
		overheatMessage.SetActive (true);
		UIText.color = Color.red;
		while (temperature > 30) {
			temperature -= 0.09f * coolingMultiplier;
			yield return new WaitForSeconds (0.01f);
		}
		UIText.color = Color.white;
		overheatMessage.SetActive (false);
		playerRb2D.simulated = true;
		cooling = false;
		overheated = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heat : MonoBehaviour {

    public Text UIText;
    public Text debugConsole;
    public float multiplier;
    public bool overheated;
    public GameObject overheatMessage;

    private float maxTemperature;
    private float startingTemperature;
    private float temperature;

	void Start () {
        overheated = false;
        startingTemperature = 23f;
        maxTemperature = 65f;
        temperature = startingTemperature;
    }
	
	
	void Update () {
        if (!overheated && temperature > maxTemperature) overheated = true;

        if (!overheated) temperature += Time.deltaTime * multiplier;
        else
        {
            temperature -= 0.09f * multiplier;
            UIText.color = Color.red;
            overheatMessage.SetActive(true);
            Time.timeScale = 0;

            if (temperature <= 38f)
            {
                Time.timeScale = 1;
                overheated = false;
                UIText.color = Color.white;
                // show message "overheat"
                overheatMessage.SetActive(false);
            }
        }

        UIText.text = Mathf.Round(temperature).ToString() + "°C"; // print it to ui no debug

    }
}

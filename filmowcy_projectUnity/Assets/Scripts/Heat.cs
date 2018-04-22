using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heat : MonoBehaviour {

    public Text UIText;
    public Text debugConsole;
    public float multiplier;
    public bool overheated;

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
            debugConsole.color = Color.red;
            Time.timeScale = 0;

            if (temperature <= 38f)
            {
                Time.timeScale = 1;
                overheated = false;
                debugConsole.color = Color.white;
                // show message "overheat"
            }
        }

        debugConsole.text = Mathf.Round(temperature).ToString() + " C"; // print it to ui no debug
	}
}

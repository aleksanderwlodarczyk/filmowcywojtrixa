using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour {

    public float power;
    private float powerMax;
    private int percentage;
    private Text percentageText;
    private Image batterySprite;

    public List<Sprite> batteryStates;
	void Start () {
        powerMax = 5000;
        power = powerMax;
        percentage = Mathf.RoundToInt((power / powerMax) * 100);
        percentageText = GameObject.Find("batteryText").GetComponent<Text>();
        batterySprite = GameObject.Find("batteryImg").GetComponent<Image>();
        percentageText.text = percentage.ToString() + "%";
	}
	
	
	void Update () {
        power -= Time.deltaTime * 11;
        Debug.Log(power);
        percentage = Mathf.RoundToInt((power / powerMax) * 100);
        percentageText.text = percentage.ToString() + "%";

        if (percentage > 75 && percentage <= 100) batterySprite.sprite = batteryStates[4];
        else if (percentage > 50 && percentage <= 75) batterySprite.sprite = batteryStates[3];
        else if (percentage > 25 && percentage <= 50) batterySprite.sprite = batteryStates[2];
        else if (percentage > 0 && percentage <= 25) batterySprite.sprite = batteryStates[1];
        else if (percentage <= 0) batterySprite.sprite = batteryStates[0];
    }

    public void AddPower(float toAdd)
    {
        if((power + toAdd) >= powerMax)
        {
            power = powerMax;
        }
        else
        {
            power += toAdd;
        }
    }
}

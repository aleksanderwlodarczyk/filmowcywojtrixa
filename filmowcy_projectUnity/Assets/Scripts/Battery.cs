using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour {

    
    public float powerMax;
    private int percentage;
    private Text percentageText;
    private Image batterySprite;
    private SceneManaging sceneManage;
    private bool extended = false;

    public float power;
    public GameObject batteryExhausted;
    public List<Sprite> batteryStates;
	void Start () {
        power = powerMax;
        if (extended) power *= 1.3f;
        percentage = Mathf.RoundToInt((power / powerMax) * 100);
        percentageText = GameObject.Find("batteryText").GetComponent<Text>();
        batterySprite = GameObject.Find("batteryImg").GetComponent<Image>();
        percentageText.text = percentage.ToString() + "%";
        sceneManage = GameObject.Find("sceneHandler").GetComponent<SceneManaging>();
    }
	
    public void ExtendBattery()
    {
        extended = true;
        power *= 1.3f;
    }
	
	void Update () {
		if (GameManager.IsPaused)
		{
			power -= Time.deltaTime * 11;

			percentage = Mathf.RoundToInt((power / powerMax) * 100);
			percentageText.text = percentage.ToString() + "%";

			if (percentage > 75) batterySprite.sprite = batteryStates[4];
			else if (percentage > 50 && percentage <= 75) batterySprite.sprite = batteryStates[3];
			else if (percentage > 25 && percentage <= 50) batterySprite.sprite = batteryStates[2];
			else if (percentage > 0 && percentage <= 25) batterySprite.sprite = batteryStates[1];
			else if (percentage <= 0) batterySprite.sprite = batteryStates[0];

			if (power <= 0) StartCoroutine(BatteryExhausted());
		}
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

        if (extended) power *= 1.3f;
    }
    
    IEnumerator BatteryExhausted()
    {
        batteryExhausted.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        sceneManage.LoadLevel(sceneManage.CurrentLevel);
        batteryExhausted.SetActive(false);
        
    }

}

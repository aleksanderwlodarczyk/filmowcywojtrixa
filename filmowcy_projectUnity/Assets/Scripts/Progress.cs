using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {

    public Dictionary<string, int> levelScore = new Dictionary<string, int>();
    private MenuButton[] menuButtons;

    private void Awake()
    {
        menuButtons = GetComponentsInChildren<MenuButton>();
		StartCoroutine(DelayMethod(SetProgress, 5f));
    }

    public void SetProgress()
    {
        foreach (MenuButton mb in menuButtons)
        {
			if (mb.firebaseLevelID != "level0")
			{
				if (levelScore[mb.firebaseLevelID] != -1)
				{
					mb.SetButtonActive(true);

					mb.levelScore = levelScore[mb.firebaseLevelID];
					mb.ScoreUpdate();
				}
				else
				{
					mb.SetButtonActive(false);
				}
			}
        }
    }

	IEnumerator DelayMethod(Action method, float time)
	{
		yield return new WaitForSeconds(time);
		method();
	}
}

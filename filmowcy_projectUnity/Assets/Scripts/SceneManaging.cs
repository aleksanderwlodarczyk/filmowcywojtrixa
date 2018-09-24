using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour {

    public int CurrentLevel { get { return SceneManager.GetActiveScene().buildIndex; } }
	[SerializeField]
	private int _shopLevelIndex;
	public int ShopLevelIndex
	{
		get
		{
			return _shopLevelIndex;
		}
	}

	public void LoadLevel(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void Restart()
    {
        
        LoadLevel(CurrentLevel);
    }
}

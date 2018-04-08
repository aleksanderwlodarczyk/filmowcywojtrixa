using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour {

    public int CurrentLevel { get { return SceneManager.GetActiveScene().buildIndex; } }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}

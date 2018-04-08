using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    private SceneManaging sceneManage;

	void Start () {
		sceneManage = GameObject.Find("sceneHandler").GetComponent<SceneManaging>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sceneManage.LoadLevel(sceneManage.CurrentLevel + 1);
        }
    }
}

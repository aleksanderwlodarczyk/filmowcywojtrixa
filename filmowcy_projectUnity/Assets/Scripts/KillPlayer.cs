using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

	public bool onTrigger;
	public bool onCollision;
	public bool skipShield;

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.gameObject.tag == "Player" && onTrigger)
        {
            collision.gameObject.GetComponent<Player>().Respawn(skipShield);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Player" && onCollision)
        {
            collision.gameObject.GetComponent<Player>().Respawn(skipShield);
        }
    }
}

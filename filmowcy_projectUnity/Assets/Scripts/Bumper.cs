using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public int force;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("bumped");
            Vector2 forceToBump = new Vector2(-collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceToBump * force);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class KeyboardMovement : MonoBehaviour {

	[Header("Prędkość postaci")]
	public float speed = 10f;
	[Header("Siła skoku")]
	public float jumpForce = 50f;
	[Header("Maksymalna prędkość")]
	public float maxSpeed = 7f;

	private bool canJump;
	private bool canSpeedUp;
	private Rigidbody2D rb2d;
	private float horizontal;
	private float realSpeed;



	public float Horizontal { 
		get{
			return horizontal;
		}
	}

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		realSpeed = speed;
		canJump = true;
		canSpeedUp = true;

	}


	void Update () {

		horizontal = Input.GetAxis ("Horizontal");


		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			canSpeedUp = false;

		}	else 
			canSpeedUp = true;

		if (rb2d.velocity.x * horizontal < 0) {
			realSpeed += Mathf.Abs (rb2d.velocity.x);
		} else
			realSpeed = speed;

		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) && canJump) {
			rb2d.AddForce (Vector2.up * jumpForce * 3);

			canJump = false;
		}
	
		if (Mathf.Abs(rb2d.velocity.y) > 0.1f && rb2d.gravityScale <= 3f) {
			
			rb2d.gravityScale += 0.07f;
		}


		if (Input.GetAxis ("Horizontal") == 0f) {
			Break ();
		}


	}

	void FixedUpdate(){
		if (canSpeedUp) {
			Vector2 oldVelocity = rb2d.velocity;
			rb2d.velocity = new Vector2 ((horizontal * realSpeed * Time.fixedDeltaTime), oldVelocity.y);
		}

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "ground") {
			canJump = true;
		}

	}

	void Break(){
		Vector2 oldvelocity = rb2d.velocity;
		rb2d.velocity = new Vector2 (0f, oldvelocity.y);
	
	}
}

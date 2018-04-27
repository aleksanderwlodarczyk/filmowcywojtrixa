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
    [Header("Maksymalna prędkość wertykalna")]
    public float maxVerticalSpeed;


    //public bool playing;
    public bool jumping;
    public GameObject debugConsole;

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
		jumping = false;
        //playing = true;
	}


	void Update () {

		//horizontal = Input.GetAxis ("Horizontal");


		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			canSpeedUp = false;

		}	else 
			canSpeedUp = true;

		if (rb2d.velocity.x * horizontal < 0) {
			realSpeed += Mathf.Abs (rb2d.velocity.x);
		} else
			realSpeed = speed;

		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))) {
			StartJump ();
		}
	
		if (Mathf.Abs(rb2d.velocity.y) > 0.1f && rb2d.gravityScale <= 3f) {
			
			rb2d.gravityScale += 0.07f;
		}


		if (horizontal == 0f) {
			Break ();
		}

    }

	public void WalkLeft(){
		horizontal = -1f;
	}

	public void WalkRight(){
		horizontal = 1f;
	}

	public void ReleaseArrow(){
		horizontal = 0f;
	}

	public void StartJump(){
		if (canJump) {
			rb2d.AddForce (Vector2.up * jumpForce * 3);
			jumping = true;
			canJump = false;
		}
	}

	public void EndJump(){
		jumping = false;
	}

	void FixedUpdate(){
		if (canSpeedUp) {
			Vector2 oldVelocity = rb2d.velocity;
			rb2d.velocity = new Vector2 ((horizontal * realSpeed * Time.fixedDeltaTime), oldVelocity.y);
		}
        

        if(Mathf.Abs(rb2d.velocity.y) >= maxVerticalSpeed)
        {
            Vector2 oldVelocity = rb2d.velocity;

            if(rb2d.velocity.y < 0f) rb2d.velocity = new Vector2(oldVelocity.x, -(maxVerticalSpeed-0.5f));
            else rb2d.velocity = new Vector2(oldVelocity.x, (maxVerticalSpeed - 0.5f));
        }
	}

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "ground") {
			canJump = true;
		}

	}

	void Break(){
		Vector2 oldvelocity = rb2d.velocity;
		rb2d.velocity = new Vector2 (0f, oldvelocity.y);
	
	}

	public void ResetVerticalVelocity(){
		Vector2 oldVelocity = rb2d.velocity;
		rb2d.velocity = new Vector2 (oldVelocity.x, 0);
	}
}

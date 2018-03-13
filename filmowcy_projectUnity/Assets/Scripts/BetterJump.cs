using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {

	public float fallMultiplier;
	public float lowJumpMultiplier;

	private KeyboardMovement keyboard;
	private Rigidbody2D rb2d;

	void Awake(){
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		keyboard = gameObject.GetComponent<KeyboardMovement> ();
	}

	void FixedUpdate(){
		if (rb2d.velocity.y < 0f) {
			rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
		} else if (rb2d.velocity.y > 0f && !keyboard.jumping) {
			rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
		}
	}
}

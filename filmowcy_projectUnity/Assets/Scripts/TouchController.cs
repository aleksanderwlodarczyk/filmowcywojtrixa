using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {

    public Text debugText;

    private string direction;
    private KeyboardMovement playerMovement;
    private Rigidbody2D player;
    private int finger;

    void Start() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardMovement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        direction = "none";
        finger = 0;
    }

    bool InMovingSpace(Vector2 touchPos) {
        if (touchPos.x >= 0f && touchPos.x <= 900f) return true;
        else return false;
    }
    void Update() {

        if (Input.touches.Length < 2) finger = 0;

        Touch touch = Input.GetTouch(finger);

        debugText.text = touch.position.ToString() + " " + direction;

        if (touch.phase == TouchPhase.Moved)
        { // swiping (?)
            if (InMovingSpace(touch.position))
            {
                if (Mathf.Abs(touch.deltaPosition.x) > 35f) {
                    if (touch.deltaPosition.x > 0f)
                    {
                        playerMovement.WalkRight();
                        direction = "right";
                    }
                    else if (touch.deltaPosition.x < 0f)
                    {
                        playerMovement.WalkLeft();
                        direction = "left";
                    }

                }
                else
                {
                    if (direction == "right") playerMovement.WalkRight();
                    else if (direction == "left") playerMovement.WalkLeft();
                    else direction = "none";
                }
            }
        }else if(touch.phase == TouchPhase.Stationary && direction != "none")
        {
            if (direction == "right") playerMovement.WalkRight();
            else playerMovement.WalkLeft();
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            playerMovement.ReleaseArrow();
            direction = "none";
        }

        if (!InMovingSpace(touch.position))
        {
            if (finger == 0) finger = 1;
            else finger = 0;
        }

	}

}

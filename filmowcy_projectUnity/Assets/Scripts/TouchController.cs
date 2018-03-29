using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {

    public Text debugText;

    private string direction;
    private KeyboardMovement playerMovement;
    private Rigidbody2D player;

    void Start() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardMovement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        direction = "none";
    }

    bool InMovingSpace(Vector2 touchPos) {
        if (touchPos.x >= 16f && touchPos.x <= 710f) return true;
        else return false;
    }
    void Update() {
        Touch touch = Input.GetTouch(0);

        debugText.text = touch.position.ToString();

        if (touch.phase == TouchPhase.Moved)
        { // swiping (?)
            if (InMovingSpace(touch.position))
            {
                if (touch.deltaPosition.x > 20f)
                {
                    playerMovement.WalkRight();
                    direction = "right";
                }
                else if (touch.deltaPosition.x < -20f)
                {
                    playerMovement.WalkLeft();
                    direction = "left";
                }
                else
                {
                    if (direction == "right") playerMovement.WalkRight();
                    else playerMovement.WalkLeft();
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
        
	}

}

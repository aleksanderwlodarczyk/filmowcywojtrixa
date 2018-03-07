using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowing : MonoBehaviour {

	public GameObject objectToFollow;
	public float xBorder;
	public float yBorder;

	private Vector3 offset;
	private bool changingBoth;

	void Start () {
		changingBoth = false;
	}


	void Update () {
		offset = objectToFollow.transform.position - transform.position;

		if (offset.x > xBorder && offset.y > yBorder) {
			Vector3 offsetWhenChange = new Vector3 (xBorder, yBorder, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
			changingBoth = true;
		}

		if (offset.x > xBorder && offset.y < -yBorder) {
			Vector3 offsetWhenChange = new Vector3 (xBorder, -yBorder, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
			changingBoth = true;
		}

		if (offset.x < -xBorder && offset.y > yBorder) {
			Vector3 offsetWhenChange = new Vector3 (-xBorder, yBorder, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
			changingBoth = true;
		}

		if (offset.x < -xBorder && offset.y < -yBorder) {
			Vector3 offsetWhenChange = new Vector3 (-xBorder, -yBorder, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
			changingBoth = true;
		}




		if (offset.x > xBorder && !changingBoth) {
			Vector3 offsetWhenChange = new Vector3 (xBorder, offset.y, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
		}

		if (offset.x < -xBorder && !changingBoth) {
			Vector3 offsetWhenChange = new Vector3 (-xBorder, offset.y, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
		}

		if (offset.y > yBorder && !changingBoth) {
			Vector3 offsetWhenChange = new Vector3 (offset.x, yBorder, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
		}

		if (offset.y < -yBorder && !changingBoth) {
			Vector3 offsetWhenChange = new Vector3 (offset.x, -yBorder, offset.z);
			transform.position = objectToFollow.transform.position - offsetWhenChange;
		}

		changingBoth = false;
	}

}

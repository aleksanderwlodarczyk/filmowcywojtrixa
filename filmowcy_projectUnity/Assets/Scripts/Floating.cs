using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

    private Transform myTransform;
    private float multiplier;

	void Start () {
        myTransform = gameObject.transform;

	}
	

	void FixedUpdate () {
        multiplier = Mathf.Sin(4 * Time.time) / 250;

        myTransform.Translate(Vector3.up * multiplier);
        
	}
}

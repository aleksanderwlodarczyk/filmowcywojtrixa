using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class EffectsControlling : MonoBehaviour {

	private VignetteAndChromaticAberration vignette;
	private NoiseAndGrain noise;
	private Fisheye fisheye;
	private Tonemapping tonemapping;
	private MotionBlur motionBlur;

	void Start () {
		vignette = gameObject.GetComponent<VignetteAndChromaticAberration> ();
		noise = gameObject.GetComponent<NoiseAndGrain> ();
		fisheye = gameObject.GetComponent<Fisheye> ();
		tonemapping = gameObject.GetComponent<Tonemapping> ();
		motionBlur = gameObject.GetComponent<MotionBlur> ();
	}
	

	public void ChangeFisheye(float value){
		fisheye.strengthX = value;
	}
}

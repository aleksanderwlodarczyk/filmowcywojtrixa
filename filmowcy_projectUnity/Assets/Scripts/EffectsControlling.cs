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
	

	public void ChangeCameraEffect(int effect, float value){
		switch (effect) {
		case 1:
			vignette.intensity = value;
			break;
		case 2:
			noise.generalIntensity = value;
			break;
		case 3:
			fisheye.strengthX = value;
			break;
		case 4:
			tonemapping.middleGrey = value;
			break;
		case 5:
			motionBlur.blurAmount = value;
			break;
		}
	}


}

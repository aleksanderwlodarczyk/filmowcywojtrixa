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

		DisableAllEffects ();
	}

	void DisableAllEffects(){
		vignette.enabled = false;
		noise.enabled = false;
		fisheye.enabled = false;
		tonemapping.enabled = false;
		motionBlur.enabled = false;
	}

	public void ChangeCameraEffect(int effect, float value){
		switch (effect) {
		case 1:
			DisableAllEffects ();
			vignette.enabled = true;
			vignette.intensity = value;
			break;
		case 2:
			DisableAllEffects ();
			noise.enabled = true;
			noise.generalIntensity = value;
			break;
		case 3:
			DisableAllEffects ();
			fisheye.enabled = true;
			fisheye.strengthX = value;
			break;
		case 4:
			DisableAllEffects ();
			tonemapping.enabled = true;
			tonemapping.middleGrey = value;
			break;
		case 5:
			DisableAllEffects ();
			motionBlur.enabled = true;
			motionBlur.blurAmount = value;
			break;
		}
	}


}

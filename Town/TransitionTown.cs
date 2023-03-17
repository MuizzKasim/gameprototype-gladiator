using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTown : MonoBehaviour {


	public GameObject fadeIn;
	public GameObject fadeOut;
	public AudioSource enterSFX;
	public AudioSource exitSFX;

	void Awake(){
		fadeOut.SetActive (false);

		if (!GameState.PlayerReturningHome) {
			fadeIn.SetActive (true);
			Destroy (fadeIn, 1.2f);
		} else {
			Destroy (fadeIn);
		}

		enterSFX.Stop ();
		exitSFX.Stop ();
	}

	void Start(){
		if (!GameState.PlayerReturningHome) {
			exitSFX.Play ();
		}
	}

	public void EnableFadeOut(){
		fadeOut.SetActive (true);
	}

	public void EnableFadeIn(){

	}

	public void PlayEnterSFX(){
		enterSFX.Play ();
	}
}

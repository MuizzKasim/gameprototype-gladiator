using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

	public GameObject fadeIn;
	public GameObject fadeOut;
	public AudioSource enterSFX;
	public AudioSource BGM;

	void Awake(){
		BGM.Play ();
		fadeOut.SetActive (false);
		fadeIn.SetActive (true);
	}

	void Start(){
		enterSFX.Play ();
		Destroy (fadeIn, 1.2f);
	}

	public void EnableFadeOut(){
		fadeOut.SetActive(true);
	}
}

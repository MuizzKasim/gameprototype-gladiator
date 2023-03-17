using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTransition : MonoBehaviour {

	public GameObject fadeIn;
	public GameObject fadeOut;

	void Awake(){
		fadeOut.SetActive (false);
		fadeIn.SetActive (true);
	}

	void Start(){
		Destroy (fadeIn, 1.2f);
	}

	public void EnableFadeOut(){
		fadeOut.SetActive(true);
	}
}

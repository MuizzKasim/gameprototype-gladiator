using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackButton: MonoBehaviour{

	public AudioSource exitSFX;
	public Transition transition;

	void Awake(){
	}

	void OnMouseDown(){
		exitSFX.Play ();
		transition.EnableFadeOut ();
		StartCoroutine ("ChangeScene");
	}

	IEnumerator ChangeScene(){
		yield return new WaitForSeconds (1f);
		NavigationManager.GoBack ();
	}
}
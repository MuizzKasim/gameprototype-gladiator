using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEntry : MonoBehaviour {

	public bool canEnterArena;
	public TransitionTown transitionTown;
	public GameObject instruction;

	void Awake(){
		instruction.SetActive (false);
	}


	void OnTriggerEnter2D(Collider2D col){
		instruction.SetActive (true);
	}

	void OnTriggerExit2D(Collider2D col) {
		instruction.SetActive (false);
	} 

	void Update() {


		if (canEnterArena&& Input.GetKeyDown(KeyCode.W)){
			Debug.Log ("Preparing to Enter");

			if (NavigationManager.CanNavigate(this.tag)) {
				Debug.Log ("Entering Arena...");
				canEnterArena = false;
				transitionTown.EnableFadeOut ();
				transitionTown.PlayEnterSFX ();
				StartCoroutine ("ChangeScene");
			}
		}
	}

	IEnumerator ChangeScene(){
		yield return new WaitForSeconds (1f);
		NavigationManager.NavigateTo (this.tag);
	}
}

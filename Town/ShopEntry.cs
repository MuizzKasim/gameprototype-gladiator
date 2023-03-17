using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopEntry : MonoBehaviour {

	bool canEnterShop = false;
	public TransitionTown transitionTown;
	public GameObject instruction;

	void Awake(){
		DontDestroyOnLoad(gameObject);
		instruction.SetActive (false);
	}

	void DialogVisible(bool visibility) {

		canEnterShop= visibility;
	} 

	void OnTriggerEnter2D(Collider2D col){
		DialogVisible(true);
		instruction.SetActive (true);
	}

	void OnTriggerExit2D(Collider2D col) {
		DialogVisible(false);
		instruction.SetActive (false);
	} 
	void Update() {


		if (canEnterShop&& Input.GetKeyDown(KeyCode.W)){
			Debug.Log ("Preparing to Enter");

			if (NavigationManager.CanNavigate(this.tag)) {
				Debug.Log ("Entering...");
				GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, GameObject.Find("Player").transform.position);
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

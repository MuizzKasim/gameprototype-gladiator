using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtons : MonoBehaviour {

	//Make sure to attach these Buttons in the Inspector
	public Button Attack, Defend, Return;
	public AudioSource clickSFX;
	public ArenaTransition transition;

	void Start()
	{
		Attack.onClick.AddListener(delegate {PerformAction("Attack"); });
		Defend.onClick.AddListener(delegate {PerformAction("Defend"); });
		Return.onClick.AddListener (delegate {ReturnToTown ();});
	}

	void PerformAction(string action)
	{
		EndPrompt ();
		clickSFX.Play ();
		FindObjectOfType<ArenaManager> ().DoAction(action);

	}

	void ReturnToTown(){
		clickSFX.Play ();
		transition.EnableFadeOut ();

		FindObjectOfType<ArenaManager> ().ReturnToTown ();
	}

	void EndPrompt(){
		FindObjectOfType<ArenaManager> ().CloseCommandButtons ();
	}
}

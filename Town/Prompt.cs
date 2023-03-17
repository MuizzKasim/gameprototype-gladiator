using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour {

	//Make sure to attach these Buttons in the Inspector
	public Button Easy, Medium, Hard;
	[HideInInspector]
	public static string difficulty;
	public ArenaEntry arena;
	public AudioSource clickSFX;

	void Start()
	{
		Easy.onClick.AddListener(delegate {Difficulty("Easy"); });
		Medium.onClick.AddListener(delegate {Difficulty("Medium"); });
		Hard.onClick.AddListener(delegate {Difficulty("Hard"); });
		difficulty = "";
	}
		
	void Difficulty(string message)
	{
		Debug.Log(message);
		clickSFX.Play ();
		difficulty = message;
		arena.canEnterArena = true;
		EndPrompt ();
	}

	void EndPrompt(){
		FindObjectOfType<DialogueManager> ().EndPrompt ();
	}
}

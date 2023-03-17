using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public GameObject player;
	public GameObject dialoguePanel;
	public GameObject promptPanel;
	public Text nameComponent;
	public Text textComponent;
	public bool endDialogue;
	public bool complete;

	[HideInInspector]
	public string dialogue;

	[Range(0.05f,0.25f)]
	public float waitTime;

	private Queue<string> dialogues;

	void Start () {
		dialoguePanel.SetActive (false);
		promptPanel.SetActive (false);
		dialogues = new Queue<string> ();
		waitTime = 0.08f;
	}

	public void StartDialogue (Dialogue dialogue){

		dialoguePanel.SetActive (true);
		nameComponent.text = dialogue.name;
		endDialogue = false;
		dialogues.Clear ();

		foreach (string line in dialogue.dialogues) {
			dialogues.Enqueue (line);

		}

		DisplayNextSentence ();
	}

	public void StartPrompt (){
		promptPanel.SetActive (true);

	}

	public void DisplayNextSentence(){
		complete = false;

		if (dialogues.Count == 0) {
			endDialogue = true;
			EndDialogue ();
			return;
		} else {
			endDialogue = false;
		}

		dialogue = dialogues.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeDialogue (dialogue));
	}

	public void DisplayFullSentence(){
		complete = true;
		StopAllCoroutines ();

		textComponent.text = "";
		textComponent.text += dialogue;
	}

	IEnumerator TypeDialogue (string dialogue){

		textComponent.text = "";
		foreach (char letter in dialogue.ToCharArray()) {
			textComponent.text += letter;
			yield return new WaitForSecondsRealtime (waitTime);
		}

		complete = true;
	}

	public void EndDialogue(){
		player.GetComponent<PlayerMovement>().isInteracting = false;
		dialoguePanel.SetActive (false);

	}

	public void EndPrompt(){
		promptPanel.SetActive (false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	public KeyCode DialogueInput = KeyCode.F;

	public GameObject npc;
	public GameObject player;
	public GameObject instruction;
	private bool canInteract = false;

	void Start () {
		//npc = this.gameObject;
		player = GameObject.Find ("Player");
		instruction.SetActive (false);
	}
		
	void Update () {
		if (Input.GetKeyDown (DialogueInput)) {
			
			if (npc.transform.position.x > player.transform.position.x && npc.transform.localScale.x < 0 && canInteract) {
				Vector3 theScale = npc.transform.localScale;
				theScale.x *= -1;
				npc.transform.localScale = theScale;
			} 
			else if (npc.transform.position.x < player.transform.position.x && npc.transform.localScale.x > 0 && canInteract) {
				Vector3 theScale = npc.transform.localScale;
				theScale.x *= -1;
				npc.transform.localScale = theScale;
			}

			if (!player.GetComponent<PlayerMovement> ().isInteracting && canInteract == true && npc.tag == "Human" && FindObjectOfType<DialogueManager> ().endDialogue != true) {
				player.GetComponent<PlayerMovement>().isInteracting = true;
				npc.GetComponent<DialogueTrigger> ().TriggerDialogue ();

			}else if (!player.GetComponent<PlayerMovement>().isInteracting && canInteract == true && npc.tag == "promptNPC" && FindObjectOfType<DialogueManager> ().endDialogue != true) {
				player.GetComponent<PlayerMovement>().isInteracting = true;
				npc.GetComponent<DialogueTrigger> ().TriggerDialogue ();
				npc.GetComponent<DialogueTrigger> ().TriggerPrompt ();

			} else if (player.GetComponent<PlayerMovement>().isInteracting && canInteract == true ){
				
				if (FindObjectOfType<DialogueManager> ().complete == false) {
					FindObjectOfType<DialogueManager> ().DisplayFullSentence ();

				} else if (FindObjectOfType<DialogueManager> ().endDialogue == false && FindObjectOfType<DialogueManager> ().complete == true) {
					FindObjectOfType<DialogueManager> ().DisplayNextSentence ();
				}

			} 
		}
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "Player") {
			canInteract = true;
			instruction.SetActive (true);
		}
	}

	void OnTriggerExit2D(Collider2D col){

		if (col.gameObject.tag == "Player") {
			canInteract = false;
			instruction.SetActive (false);
			FindObjectOfType<DialogueManager> ().endDialogue = false;
			FindObjectOfType<DialogueManager> ().EndPrompt ();
		}
	}
		
}

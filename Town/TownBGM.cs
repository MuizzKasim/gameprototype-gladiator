using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBGM : MonoBehaviour {

	public AudioSource bgm0;
	public AudioSource bgm1;
	public AudioSource bgm2;

	private AudioSource selected;
	void Start () {
		SelectBGM ();
		selected.Play ();
	}

	void SelectBGM(){
		float randomiser = Random.Range (0f, 3f);

		switch ((int)randomiser) {
		case 0:
			selected = bgm0;
			break;
		case 1:
			selected = bgm1;
			break;
		case 2:
			selected = bgm2;
			break;
		default:
			break;

		}
	}

}

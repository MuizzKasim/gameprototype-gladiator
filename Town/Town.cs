using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {
	void Start () {

		if (GameState.PlayerReturningHome == false) {
			Destroy (gameObject);
		}
	}

}

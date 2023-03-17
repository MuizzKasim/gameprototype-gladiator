using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {

	public Button Inv;
	public AudioSource clickSFX;
	public GameObject Panel;

	public GameObject Item01;
	public GameObject Item02;
	public GameObject Item03;
	public GameObject Item04;
	public GameObject Item05;
	public GameObject Item06;

	public Text StatsTxt;

	void Start()
	{
		CheckItemOwnership ();

		Panel.SetActive (false);

		StatsTxt.text = "Name: "+GameState.currentPlayer.GetName()+
			"\nOccupation: "+GameState.currentPlayer.Occupation+
			"\nLevel: "+GameState.currentPlayer.Level+
			"\nExperience: "+GameState.currentPlayer.Experience+"/"+GameState.currentPlayer.MaxExperience+
			"\nHealth: "+GameState.currentPlayer.Health+"/"+GameState.currentPlayer.MaxHealth+
			"\nStrength: "+GameState.currentPlayer.Strength+
			"\nArmor: "+GameState.currentPlayer.Armor+
			"\nMoney: "+GameState.currentPlayer.Money;
	}

	void Update(){
		if (Panel.activeInHierarchy) {

			Inv.onClick.AddListener(delegate {ClosePanel(); });
		} else {

			Inv.onClick.AddListener(delegate {OpenPanel(); });
		}
	}

	void CheckItemOwnership(){
		bool item01 = false, item02 = false, item03 = false, item04 = false, item05 = false, item06 = false;

		foreach (InventoryItem item in GameState.currentPlayer.Inventory) {
			if (item.ItemName.CompareTo ("Little Dagger") == 0)
				item01 = true;
			if (item.ItemName.CompareTo ("Longsword") == 0)
				item02 = true;
			if (item.ItemName.CompareTo ("Cutlass") == 0)
				item03 = true;
			if (item.ItemName.CompareTo ("Royal Guard Longsword") == 0)
				item04 = true;
			if (item.ItemName.CompareTo ("Sleeping Dragon") == 0)
				item05 = true;
			if (item.ItemName.CompareTo ("Bearded Axe") == 0)
				item06 = true;
		}

		if(item01){Item01.SetActive (true);}else{Item01.SetActive (false);}

		if(item02){Item02.SetActive (true);}else{Item02.SetActive (false);}

		if(item03){Item03.SetActive (true);}else{Item03.SetActive (false);}

		if(item04){Item04.SetActive (true);}else{Item04.SetActive (false);}

		if(item05){Item05.SetActive (true);}else{Item05.SetActive (false);}

		if(item06){Item06.SetActive (true);}else{Item06.SetActive (false);}
	}

	void OpenPanel()
	{
		Panel.SetActive (true);
		clickSFX.pitch = 1f;
		clickSFX.Play ();
	}

	void ClosePanel(){
		Panel.SetActive (false);
		clickSFX.pitch = 0.75f;
		clickSFX.Play ();
	}


}

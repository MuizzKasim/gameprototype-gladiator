using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {

	public ShopManager Manager;
	void OnMouseDown(){
		Debug.Log ("Buying Item...");
		Manager.PurchaseSelectedItem();
	}
}

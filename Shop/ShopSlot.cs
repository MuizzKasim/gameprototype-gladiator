using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour {

	public InventoryItem Item;
	public ShopManager Manager;
	public AudioSource purchaseSFX;
	public AudioSource denialSFX;
	public GameObject insufficientFunds;


	void Start(){
		AddShopItem (Item);
		insufficientFunds.SetActive (false);
	}

	void OnMouseDown()
	{
		if (Item != null)
		{
			Manager.SetShopSelectedItem(this);
		}

		if (!Manager.Coin.activeInHierarchy || Manager.SelectedShopSlot != null) {
			Manager.EnableCoin ();
		} else {
			Manager.DisableCoin ();
		}
	}

	void OnMouseEnter()
	{
		if (Item != null)
		{
			Manager.HoverShopSelectedItem(this);
		}

		if (!Manager.Coin.activeInHierarchy || Manager.SelectedShopSlot != null) {
			Manager.EnableCoin ();
		} else {
			Manager.DisableCoin ();
		}
	}

	void OnMouseExit()
	{
		if (Item != null)
		{
			Manager.ResetShopSelectedItem();
		}

		if (!Manager.Coin.activeInHierarchy || Manager.SelectedShopSlot != null) {
			Manager.EnableCoin ();
		} else {
			Manager.DisableCoin ();
		}
	}

	public void AddShopItem(InventoryItem item)
	{
		if (GameState.currentPlayer.CheckinventoryItem (item.ItemName) == true) {
			return;
		} else {
			var spriteRenderer = GetComponent<SpriteRenderer> ();
			spriteRenderer.sprite = item.Sprite;
			spriteRenderer.transform.localScale = item.Scale;
		}
	}

	public void PurchaseItem()
	{
		if (GameState.currentPlayer.Money >= Item.Cost) {
			purchaseSFX.Play ();
			GameState.currentPlayer.Money -= Item.Cost;
			GameState.currentPlayer.AddinventoryItem (Item);
			Manager.UpdateMoneyCounter ();
			Item = null;
			var spriteRenderer = GetComponent<SpriteRenderer> ();
			spriteRenderer.sprite = null;
			Manager.ClearSelectedItem ();
		} else {
			insufficientFunds.SetActive (true);
			StartCoroutine (DeactivateObject(insufficientFunds));
		}
	}

	IEnumerator DeactivateObject(GameObject obj){
		denialSFX.Play();
		yield return new WaitForSecondsRealtime (2f);
		obj.SetActive (false);
	}
}

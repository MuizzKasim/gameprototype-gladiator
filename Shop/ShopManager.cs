using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	public GameObject PurchaseItem_NamePanel;
	public GameObject PurchaseItem_DescPanel;
	public GameObject PurchaseItem_StatsPanel;
	public GameObject SelectionCircle;
	public GameObject MoneyPanel;
	public GameObject Coin;

	private string SetPurchaseItem_NameTxt;
	private string SetPurchaseItem_DescTxt;
	private string SetPurchaseItem_StatsTxt;
	private Text PurchaseItem_NameTxt;
	private Text PurchaseItem_DescTxt;
	private Text PurchaseItem_StatsTxt;
	private Text MoneyTxt;

	public string Money;
	[HideInInspector]
	public ShopSlot SelectedShopSlot;


	public AudioSource selectionSFX;

	// Use this for initialization
	void Start () {
		PurchaseItem_NameTxt = PurchaseItem_NamePanel.GetComponentInChildren<Text> ();
		PurchaseItem_DescTxt = PurchaseItem_DescPanel.GetComponentInChildren<Text> ();
		PurchaseItem_StatsTxt = PurchaseItem_StatsPanel.GetComponentInChildren<Text> ();
		MoneyTxt = MoneyPanel.GetComponentInChildren<Text> ();
		Coin.SetActive (false);
		SelectionCircle.SetActive (false);
		Money = GameState.currentPlayer.Money.ToString();
		MoneyPanel.SetActive (true);
		MoneyTxt.text = "Money: " + Money;
	}
		
	public void SetShopSelectedItem(ShopSlot slot)
	{
		SelectedShopSlot = slot;
		SetPurchaseItem_NameTxt = slot.Item.ItemName;
		SetPurchaseItem_DescTxt = slot.Item.ItemDesc;
		SetPurchaseItem_StatsTxt = "+" + slot.Item.Strength + " Strength\t" + "+" + slot.Item.Armor + " Armor\t\t" + slot.Item.Cost;
		SelectionCircle.transform.position = slot.transform.position;

		selectionSFX.Play ();

		SelectionCircle.SetActive (true);
		PurchaseItem_NamePanel.SetActive (true);
		PurchaseItem_DescPanel.SetActive (true);
		PurchaseItem_StatsPanel.SetActive (true);
	}

	public void ResetShopSelectedItem()
	{
		PurchaseItem_NameTxt.text = SetPurchaseItem_NameTxt;
		PurchaseItem_DescTxt.text = SetPurchaseItem_DescTxt;
		PurchaseItem_StatsTxt.text = SetPurchaseItem_StatsTxt;

		PurchaseItem_NamePanel.SetActive (true);
		PurchaseItem_DescPanel.SetActive (true);
		PurchaseItem_StatsPanel.SetActive (true);
	}

	public void HoverShopSelectedItem(ShopSlot slot)
	{
		PurchaseItem_NameTxt.text = slot.Item.ItemName;
		PurchaseItem_DescTxt.text = slot.Item.ItemDesc;
		PurchaseItem_StatsTxt.text = "+" + slot.Item.Strength + " Strength\t" + "+" + slot.Item.Armor + " Armor\t\t" + slot.Item.Cost;

		PurchaseItem_NamePanel.SetActive (true);
		PurchaseItem_DescPanel.SetActive (true);
		PurchaseItem_StatsPanel.SetActive (true);
	}

	public void ClearSelectedItem()
	{
		SelectedShopSlot = null;

		PurchaseItem_NameTxt.text = "";
		PurchaseItem_DescTxt.text = "";
		PurchaseItem_StatsTxt.text = "";

		SelectionCircle.SetActive (false);
		PurchaseItem_NamePanel.SetActive (false);
		PurchaseItem_DescPanel.SetActive (false);
		PurchaseItem_StatsPanel.SetActive (false);
		Coin.SetActive (false);

	}

	public void UpdateMoneyCounter(){
		Money = GameState.currentPlayer.Money.ToString();
		MoneyTxt.text = "Money: " + Money;
	}

	public void EnableCoin(){
		Coin.SetActive (true);
	}

	public void DisableCoin(){
		Coin.SetActive (false);
	}

	public void PurchaseSelectedItem()
	{
		SelectedShopSlot.PurchaseItem();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity{
	public List<InventoryItem> Inventory = new System.Collections.Generic.List<InventoryItem>();

	public Player(){
		this.Name = "Jocelin";
		this.Occupation = "Knight";
		this.Health = 150;
		this.MaxHealth = this.Health;
		this.Money = 200;
	}

	public void AddinventoryItem(InventoryItem item)
	{
		this.Strength += item.Strength;
		this.Armor += item.Armor;
		Inventory.Add(item);

	}

	public void GainMoney(int amount){
		this.Money += amount;
	}

	public void GainEXP(int amount){
		this.Experience += amount;
	}

	public void HealHp(int amount){
		if (this.Health + amount < this.MaxHealth) {
			this.Health += amount;
		} else {
			this.Health = this.MaxHealth;
		}
	}

	public void LevelUp(){
		this.Level += 1;
		this.MaxHealth += 50;
		this.Strength += 4;
		this.Armor += 1;

		if (this.Experience > this.MaxExperience) {
			this.Experience -= this.MaxExperience;
			if (this.Level == 3) {
				this.MaxExperience = 99999;
			} else {
				this.MaxExperience *= 3;
			}
		} else {
			if (this.Level == 3) {
				this.MaxExperience = 99999;
			} else {
				this.MaxExperience *= 3;
			}
		}
	}

	public bool CheckinventoryItem(string itemName){
		bool found = false;

		foreach (InventoryItem item in GameState.currentPlayer.Inventory) {
			if (item.ItemName.CompareTo (itemName) == 0)
				found = true;
		}

		return found;
	}
}

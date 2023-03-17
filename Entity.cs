using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : ScriptableObject
{
	public string Name;
	public string Occupation;
	public int Level = 1;
	public int Experience = 0;
	public int MaxExperience = 20;
	public int Health = 2;
	public int MaxHealth = 2;
	public int Strength = 1;
	public int Armor = 0;
	public int Money = 0;

	public int TakeDamage(int Amount) {
		int totalDamage;
		float randomiser = Random.Range (0.8f, 1.2f);
		totalDamage = Mathf.FloorToInt(Mathf.Clamp((Amount - Armor),0, int.MaxValue)*randomiser);

		if (this.Health - totalDamage < 0) {
			this.Health = 0;
		} else {
			this.Health -= totalDamage;
		}
	
		return totalDamage;
	}

	public int TakeResistedDamage(int Amount){
		int totalDamage;
		float randomiser = Random.Range (0.8f, 1.2f);
		totalDamage = Mathf.FloorToInt(((Mathf.Clamp((Amount - this.Armor),0, int.MaxValue))/(int)2)*randomiser);

		if (this.Health - totalDamage < 0) {
			this.Health = 0;
		} else {
			this.Health -= totalDamage;
		}

		return totalDamage;
	}

	public void Attack(Entity Entity) { 
		Entity.TakeDamage(Strength); 
	}

	//Setters//
	public void SetName(string Name){
		this.Name = Name;
	}

	public void SetOccupation(string Occupation){
		this.Occupation = Occupation;
	}

	public void SetLevel(int Level){
		this.Level = Level;
	}

	public void SetExperience(int Experience){
		this.Experience = Experience;
	}

	public void SetHealth(int Health){
		this.Health = Health;
	}

	public void SetMaxHealth(int MaxHealth){
		this.MaxHealth = MaxHealth;
	}

	public void SetStrength(int Strength){
		this.Strength = Strength;
	}

	public void SetMaxExperience(int MaxExperience){
		this.MaxExperience = MaxExperience;
	}

	public void SetArmor(int Armor){
		this.Armor = Armor;
	}

	public void SetMoney(int Money){
		this.Money = Money;
	}

	//Getters//
	public string GetName(){
		return this.Name;
	}

	public string GetOccupation(){
		return this.Occupation;
	}

	public int GetLevel(){
		return this.Level;
	}

	public int GetExperience(){
		return this.Experience;
	}

	public int GetHealth(){
		return this.Health;
	}

	public int GetMaxHealth(){
		return this.MaxHealth;
	}

	public int GetStrength(){
		return this.Strength;
	}

	public int GetMaxExperience(){
		return this.MaxExperience;
	}

	public int GetArmor(){
		return this.Armor;
	}
	public int GetMoney(){
		return this.Money;
	}

}
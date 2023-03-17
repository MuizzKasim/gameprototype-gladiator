using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem : MonoBehaviour {

	public Sprite Sprite;
	public Vector3 Scale;
	public string ItemName;
	public string ItemDesc;
	public int Cost;
	public int Strength;
	public int Armor;
}

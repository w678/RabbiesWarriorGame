using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryStorage {

	public int ID;
	public Item item;
	public int quantity;
	public Sprite icon;

	public InventoryStorage(int id, Item newItem, int Quantity, Sprite Icon)
	{
		ID = id;
		item = newItem;
		quantity = Quantity;
		icon = Icon;
	}
}

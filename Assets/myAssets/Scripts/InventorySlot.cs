using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;
	Item item;
	public Text quantityText;
	int quantity = 0;
	public static bool updateS;

	public void AddItem (Item newItem)
	{
		item = newItem;
		quantityText.enabled = true;
		icon.sprite = item.icon;
		icon.enabled = true;
	}

	public void StackItem (Item newItem)
	{
		item = newItem;
		quantityText.text = ""+ item.quantity;
	}

	public void ClearSlot()
	{
		if (Inventory.empty)
		{
			item = null;
			icon.sprite = null;
			icon.enabled = false;
			Inventory.empty = false;
			quantityText.enabled = false;
		}
		
	}

	public void UseItem()
	{
		if (item != null)
		{
			item.Use();
			
		}
	}

	public void OnRemoveButton()
	{
		Inventory.instance.Remove(item, 1);
		ClearSlot();
	}
}

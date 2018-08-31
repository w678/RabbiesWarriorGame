using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;
	public int space = 8;
	public static Inventory instance;

	public List<InventoryStorage> inventory;
	public int currentSpace;
	public int Space;
	public static bool stacked = false;
	public static bool empty = false;
	public static int qqq;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}

	#endregion
	public List<Item> items = new List<Item>();
	public List<int> qqqq = new List<int>();

	public bool Add (Item item, int amount)
	{
		if (!item.isDefaultItem)
		{
			if (currentSpace - amount >= 0)
			{
				bool found = false;
				bool aaa = false;
				foreach (InventoryStorage ISD in inventory.ToArray())
				{
					if (ISD.ID == item.ID)
					{
						found = true;
						ISD.quantity += amount;
						Debug.Log("111");
						stacked = true;
						item.quantity = ISD.quantity;
						qqq = item.quantity;
						InventorySlot.updateS = true;
						qqqq.Add(1);
						Debug.Log(qqqq.Count + "count");
					}
				}

			

				if (!found)
				{
					items.Add(item);
					InventoryStorage IS = new InventoryStorage(item.ID, item, amount, item.icon);
					inventory.Add(IS);
					item.quantity = 1;
					qqqq.Add(1);
					currentSpace -= amount;
					qqq = 1;
					if (onItemChangedCallback != null)
					{
						onItemChangedCallback.Invoke();
					}	

					return true;
				}
				else{
					Debug.Log("Not enough room");
					return false;
				}
			}
			
		}

		return true;
	}

	public bool Remove (Item item, int amount)
	{

		bool found = false;
		bool aaa = false;
		foreach (InventoryStorage ISD in inventory.ToArray())
		{
			if (ISD.ID == item.ID)
			{
				if (ISD.quantity >= amount)
				{
					ISD.quantity -= amount;
					found = true;
					aaa = true;
					qqq = ISD.quantity;
					InventorySlot.updateS = true;
					item.quantity = qqq;
				}

				if (ISD.quantity == 0)
				{
					empty = true;
					qqq = ISD.quantity;
					items.Remove (item);
					inventory.Remove(ISD);
					
				}
			}
		}	

		if (onItemChangedCallback != null)
			{
				onItemChangedCallback.Invoke();
			}

		return true;

		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";
	public int ID = 0;
	public int quantity = 0;
	//public ParticleSystem effect;
	public Sprite icon = null;
	public bool isDefaultItem = false;
	public static bool isActive = false;
	
	

	public virtual void Use()
	{
		// Use the item
		// Something might happen
		Debug.Log("Using " + name);
		if (name == "Potion")
		{
			isActive = true;
		}
		
	}

	public void RemoveFromInventory()
	{
		Inventory.instance.Remove(this, 1);
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour {

	public Item item;
	public bool InArea = false;
	public GameObject itemDrop;
	public GameObject itemWindows;
	public Text ObjectName;
	public Text ControlButton;

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	public void PickUp()
	{
		if (InArea)
		{
			Debug.Log("Picking up item" + item.name);
			bool wasPickedUp = Inventory.instance.Add(item, 1);

			if (Inventory.stacked)
			{
				Inventory.stacked = false;
				itemWindows.SetActive(false);
				Destroy(gameObject);
			} 

			if (wasPickedUp)
			{
				itemWindows.SetActive(false);
				Destroy(gameObject);
			}
		}		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			ObjectName.text = itemDrop.name;
			ControlButton.text = "'A' to Pickup";
			itemWindows.SetActive(true);
			InArea = true;
			Debug.Log("In area");
		}
	}

	void OnTriggerExit(Collider other)
	{
		InArea = false;
		Debug.Log("Out area");
		itemWindows.SetActive(false);
	}

}

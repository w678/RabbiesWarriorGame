using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	public GameObject inventoryUI;

	public Transform itemParent;

	Inventory inventory;

	InventorySlot[] slots;

	Text aaa;

	int q = 0;

	void Start()
	{
		//string strCmdText;
    	//strCmdText= "`java jade.Boot -gui";   //This command to open a new notepad
    	//System.Diagnostics.Process.Start("CMD.exe",strCmdText); //Start cmd process

		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;

		slots = itemParent.GetComponentsInChildren<InventorySlot>();
		aaa = itemParent.GetComponentInChildren<InventorySlot>().GetComponentInChildren<Text>();

	}

	void Update()
	{
		if (q < inventory.qqqq.Count)
		{
			for (int i = 0; i < inventory.items.Count; i++)
			{
				slots[i].StackItem(inventory.items[i]);
			}
		}
				
	}

	void UpdateUI()
	{
		
		for (int i = 0; i < slots.Length; i++)
		{
			
			if (i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
				
				
			} else
			{
				slots[i].ClearSlot();
			}
		}
	}
}

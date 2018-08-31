using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	#region 

	public static Enemy instance;

	void Awake()
	{
		instance = this;
	}

	#endregion
	PlayerManager playerManager;
	CharacterStats myStats;
	Animator anim;

	public bool InArea = false;
	public GameObject itemDrop;
	public GameObject itemWindows;
	public Text ObjectName;
	public Text ControlButton;
	

	void Start()
	{
		playerManager = PlayerManager.instance;
		myStats = GetComponent<CharacterStats>();
		anim = playerManager.player.GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			Attack();
		}
	}

	public void Attack()
	{
		PlayerCombat playerCombat = playerManager.player.GetComponent<PlayerCombat>();
		
		if (playerCombat != null && InArea)
		{
			playerCombat.CAttack(myStats);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			ObjectName.text = itemDrop.name;
			ControlButton.text = "'A' to attack";
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

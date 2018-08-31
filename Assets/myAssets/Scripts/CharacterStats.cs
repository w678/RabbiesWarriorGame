using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
                               // Reference to an image to flash on the screen on being hurt.
	public Stat damage;
	public Stat armor;

	void Awake()
	{
		currentHealth = maxHealth;
	}

	public int getCurrentHealth()
	{
		return currentHealth;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			TakeDamage(10);
		}
	}

	public void TakeDamage (int damage)
	{
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);


		currentHealth -= damage;
		
		Debug.Log(transform.name + " takes " + damage + " damages.");

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		Debug.Log(transform.name + " die.");
	}

}

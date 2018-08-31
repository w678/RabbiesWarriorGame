using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	public float attackSpeed = 1f;
	public static float attackCooldown = 0f;

	public float attackDelay = 1f;

	bool Att = false;
	public event System.Action OnAttack;
	CharacterStats myStats;
	Animator anim;   
	PlayerManager playerManager;
	void Start()
	{
		myStats = GetComponent<CharacterStats>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		attackCooldown -= Time.deltaTime;
	}
	

	public void Attack(PlayerStats targetStats)
	{
		if (attackCooldown <= 0f)
		{
			StartCoroutine(Dodamage(targetStats, attackDelay));

			if (OnAttack != null)
			OnAttack();
			attackCooldown = 1f / attackSpeed;
			PlayerStats.canAttack = false;
			//Animating();
			

		}
		
		
		//targetStats.TakeDamage(myStats.damage.GetValue());
	}

	void Animating (float h)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsAttacking", walking);

        }

	IEnumerator Dodamage(PlayerStats stats, float delay)
	{
		yield return new WaitForSeconds(delay);

		stats.TakeDamage(myStats.damage.GetValue());
	}

	
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

	Animator anim;

	public static bool canAttack = true;

	public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;   
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); 
	public static bool damaged;
	public Text CoinText;
	int coin = 0;

	void Start()
	{
		EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		healthSlider.value = getCurrentHealth();
		CoinText.text = coin.ToString();

		// If the player has just been damaged...
            if(damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
	}

	public void GetCoin()
	{
		coin += 10;
	}

	public int CoinAmount()
	{
		return coin;
	}
	

	void onEquipmentChanged (Equipment newItem, Equipment oldItem)
	{
		if (newItem != null)
		{
			armor.AddModifier(newItem.armorModifier);
			damage.AddModifier(newItem.damageModifier);
		}

		if (oldItem != null)
		{
			armor.RemoveModifier(oldItem.armorModifier);
			damage.RemoveModifier(newItem.damageModifier);
		}

		
	}
	

	void Animating (float h)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsAttacking", walking);

        }

	public override void Die()
		{
			base.Die();
		}
}

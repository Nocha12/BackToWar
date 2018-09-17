using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	AudioSource playerAudio;
	Animator anim;
	PlayerMovement playerMovement;

	bool isDeath;

	void Awake () {
		anim = GetComponent<Animator> ();
		playerAudio = GetComponent<AudioSource> ();
		playerMovement = GetComponent<PlayerMovement> ();
		currentHealth = startingHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
//		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDeath) {
			Death ();
		} else {
			//anim.SetTrigger ("Damage");
		}
	}

	void Death () {
		isDeath = true;

		anim.SetTrigger ("Die");
		playerMovement.enabled = false;
	}
}

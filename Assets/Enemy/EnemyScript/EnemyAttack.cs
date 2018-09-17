using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	protected Animator avatar;
	public float timeBetweenAttacks = 0.625f;
	public int attackDamage = 10;

	GameObject player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	bool playerInRange;
	float lastAttackTime;

	void Awake()
	{
		avatar = transform.parent.GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = transform.parent.GetComponent<EnemyHealth> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = false;
		}
	}

	void Update()
	{
		if (playerInRange && enemyHealth.currentHealth > 0) {
			Attack ();
		}
	}

	void Attack()
	{
		if (Time.time - lastAttackTime > 0.75f) {
			lastAttackTime = Time.time;

			avatar.SetTrigger ("StartAttack");
		}

		if (playerHealth.currentHealth > 0)
			playerHealth.TakeDamage (attackDamage);
	}


}

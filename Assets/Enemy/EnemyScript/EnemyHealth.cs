using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	protected Animator avatar;
	public int startingHealth = 100;
	public int currentHealth;

	private Rigidbody rBody;
	public NormalTarget playerAttackTargets;

	bool isDead;
	bool damaged;

	void Awake()
	{
		avatar = transform.GetComponent<Animator>();
		currentHealth = startingHealth;
		rBody = GetComponent<Rigidbody> ();
		playerAttackTargets = FindObjectOfType<NormalTarget> () as NormalTarget;
	}

	public void TakeDamage(int amount)
	{
		damaged = true;

		currentHealth -= amount;

		if (currentHealth <= 0 && !isDead) {
            playerAttackTargets.targetList.Remove (GetComponent<Collider>());
		
			Death ();
		}
	}

	public IEnumerator StartDamage(int damage, Vector3 pos, float delay, float pushBack)
	{
		yield return new WaitForSeconds (delay);
		try {
			Vector3 diff = (pos - transform.position);
			diff.Normalize();
			TakeDamage(damage);
			rBody.velocity = new Vector3(0, 0, 0);
			rBody.AddForce( -diff * 50 * pushBack);
		}
		catch(MissingReferenceException e)
		{
			Debug.Log (e.ToString ());
		}
	}

	void Update()
	{
		damaged = false;
	}

	void Death()
	{
		isDead = true;

		GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<Collider> ().isTrigger = true;
		avatar.SetTrigger ("Die");
		FadeManager.instance.FaidOut (gameObject);
		EnemySpawnManager.enemyCount--;

		Destroy (this.gameObject, 1.5f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
	
	Transform player;
	public NavMeshAgent nav;
	private Quaternion startQaternion, randomQuaternion;

	public bool isFollow;

	void Awake()
	{
		Physics.gravity = new Vector3(0, -25, 0);
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		StartCoroutine (SetRandomQuaternion ());
	}

	void Update()
	{
		if (Vector3.Distance (player.transform.position, transform.position) < 30 || isFollow) {
			if (nav.enabled) {
				StopCoroutine (SetRandomQuaternion());
				isFollow = true;
				nav.SetDestination (player.position);
			}
		} else if (!isFollow) {
			if(nav.isOnNavMesh)
				transform.Translate (Vector3.forward * Time.deltaTime * 3);
			transform.rotation = Quaternion.Slerp (startQaternion, randomQuaternion, Time.time * 0.05f);
		}
	}

	IEnumerator SetRandomQuaternion()
	{
		while (true) {
			randomQuaternion = Quaternion.Euler (0, Random.Range (-300, 300), 0);
			startQaternion = transform.rotation;
			yield return new WaitForSeconds(5);
		}
	}
}

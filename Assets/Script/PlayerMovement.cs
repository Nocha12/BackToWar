using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	protected Animator avatar;

	float lastAttackTime, lastSkillTime, lastDashTime;

	public bool attacking = false; 
	public bool dashing = false; 

	public Vector3 rotationOffset = new Vector3 (0, 0, 0);

	void Start () {
		avatar = GetComponent<Animator>();
	}

	float h, v;

	public void OnStickChanged(Vector2 stickPos)
	{
		h = stickPos.x;
		v = stickPos.y;
	}

	void Update () {
		if (!avatar)
			return;
	
		avatar.SetFloat ("Speed", (h * h + v * v));

		Rigidbody rigidbody = GetComponent<Rigidbody> ();

		if (rigidbody) {
			Vector3 speed = rigidbody.velocity;
			speed.x = 4 * h;
			speed.z = 4 * v;
			rigidbody.velocity = speed;

			if (h != 0 && v != 0) {
				transform.rotation = Quaternion.LookRotation (new Vector3 (h, 0, v));
				transform.Rotate(new Vector3(0, -90, 0));
			}
		}
	}
}

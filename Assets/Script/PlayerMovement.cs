using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	protected Animator avatar;

	float lastAttackTime, lastSkillTime, lastDashTime;

	public float jumpPower;
	public bool attacking = false; 
	public bool dashing = false; 
	private bool jumped = false;

	public Vector3 rotationOffset = new Vector3 (0, 0, 0);
	protected PlayerAttack playerAttack;

	void Start () {
		avatar = GetComponent<Animator>();
		playerAttack = GetComponent<PlayerAttack> ();
		Physics.gravity = new Vector3(0, -25, 0);
	}

	public float h, v;

	public void OnStickChanged(Vector2 stickPos)
	{
		h = Mathf.Abs(stickPos.x) < 0.001f ? 0 : stickPos.x;
		v = Mathf.Abs(stickPos.y) < 0.001f ? 0 : stickPos.y;;
	}

	void Update () {
		CheckGround ();

		if (!avatar)
			return;
	
		avatar.SetFloat ("Speed", (h * h + v * v));

		Rigidbody rigidbody = GetComponent<Rigidbody> ();

		if (rigidbody) {
			if (Input.GetKeyDown(KeyCode.Space) && !jumped) {
				jumped = true;
				rigidbody.AddForce (Vector3.up * jumpPower, ForceMode.Impulse);
			}

			Vector3 speed = rigidbody.velocity;
			speed.x = 8 * h;
			speed.z = 8 * v;
			rigidbody.velocity = speed;

			if (h != 0 && v != 0) {
				transform.rotation = Quaternion.LookRotation (Vector3.Normalize(new Vector3 (h, 0, v)));
				transform.Rotate (new Vector3 (0, -90, 0));
			}
		}
	}
	void CheckGround()
	{      
		RaycastHit hit;
		Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

		if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f))
		{
			if (hit.transform.CompareTag ("Ground")) {
				jumped = false;
				return;
			}
		}
		jumped = true;
	}

	public void OnAttackDown()
	{
		attacking = true;
		StartAttack ();
	}

	void StartAttack()
	{
		if (Time.time - lastAttackTime > 0.625f) {
			lastAttackTime = Time.time;
			if (attacking) {
				avatar.SetTrigger ("AttackStart");
				avatar.SetBool ("Combo", false);
				playerAttack.NormalAttack ();
			}
		}
		else if (Time.time - lastAttackTime < 0.3f && !avatar.GetBool("Combo")) {
			avatar.SetBool ("Combo", true);
			lastAttackTime += 0.425f;
			playerAttack.NormalAttack ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {
	public float distanceAway = 21;

	private int touchId = -1;
	public float speed = 0.1f;
	public Vector2 nowPos, prePos;
	public Vector3 movePos;

	public PlayerMovement player;
	private Transform follow;

	void Start()
	{
		follow = player.transform;	
	}

	void Update()
	{
		int i = 0;

		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				i++;

				if (touch.phase.Equals (TouchPhase.Began)) {
					if (i != TouchPad.touchId) {
						touchId = i;
						prePos = touch.position - touch.deltaPosition;
					}
				}
				if (touch.phase.Equals (TouchPhase.Moved) || touch.phase.Equals (TouchPhase.Stationary)) {
					if (touchId.Equals (i)) {
						nowPos = touch.position - touch.deltaPosition;
						movePos = (Vector3)(prePos - nowPos) * speed;
						transform.Translate(movePos);
						prePos = touch.position - touch.deltaPosition;
					}
				}
				if(touch.phase.Equals(TouchPhase.Ended))
				{
					if (touchId.Equals (i))
						touchId = -1;
				}
			}
		}
	}

	void LateUpdate () {
		Vector3 relativePos = follow.position - transform.position;
		transform.rotation = Quaternion.LookRotation (relativePos);

		Vector3 diffVector = transform.position - follow.position;
		diffVector.Normalize ();
		transform.position = follow.position + diffVector * distanceAway;

		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (new Vector3(0,1,0));
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (new Vector3(0,-1,0));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (new Vector3(1,0,0));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (new Vector3(-1,0,0));
		}
	}
}

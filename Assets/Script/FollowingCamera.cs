using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {
	
	public float distanceAway = 7;
	public float distanceUp = 4;

	public Transform follow;
	public Vector3 offset = new Vector3(0, 0, 0);

	void LateUpdate () {
		transform.position = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway + offset;
	}
}

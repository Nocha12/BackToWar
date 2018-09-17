using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour {
	private RectTransform touchPad;

	private Vector3 startPos = Vector3.zero;
	private bool buttonPressed = false;
	public static int touchId = -1;

	public float dragRadius = 65;
	public PlayerMovement player;

	void Start () {
		touchPad = GetComponent<RectTransform> ();
		startPos = touchPad.position;
	}

	public void ButtonDown()
	{
		buttonPressed = true;
	}

	public void ButtonUp()
	{
		buttonPressed = false;

		HandleInput (startPos);
	}

	void FixedUpdate()
	{
		HandleTouchInput ();

		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
			HandleInput (Input.mousePosition);
		#endif
	}

	void HandleTouchInput()
	{
		int i = 0;

		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				i++;
				Vector3 touchPos = new Vector3 (touch.position.x, touch.position.y);

				if (touch.phase.Equals (TouchPhase.Began)) {
					if (touch.position.x <= startPos.x + dragRadius)
						touchId = i;
				}
				if (touch.phase.Equals (TouchPhase.Moved) || touch.phase.Equals (TouchPhase.Stationary)) {
					if (touchId.Equals(i))
						HandleInput (touchPos);
				}
				if(touch.phase.Equals(TouchPhase.Ended))
				{
					if (touchId.Equals (i))
						touchId = -1;
				}
			}
		}
	}

	void HandleInput(Vector3 input)
	{
		if (buttonPressed) {
			Vector3 diffVector = (input - startPos);

			if (diffVector.sqrMagnitude > dragRadius * dragRadius) {
				diffVector.Normalize ();
				touchPad.position = startPos + diffVector * dragRadius;
			} else {
				touchPad.position = input;
			}
		} else {
			touchPad.position = startPos;
		}

		Vector3 diff = touchPad.position - startPos;

		Vector2 normDiff = new Vector3 (diff.x / dragRadius, diff.y / dragRadius);

		if (player)
			player.OnStickChanged (normDiff);
	}
}

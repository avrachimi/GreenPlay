using UnityEngine;
using System.Collections;

public class openCircleDoor : MonoBehaviour {

	private Rigidbody2D rb2d; 
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//touchInput();

		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;

		}
		if (fingerCount > 0) {
			rb2d.MoveRotation(-90f);
		}
		else {
			rb2d.MoveRotation(0f);
		}
	}

	void touchInput()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			//LATER: make sure to check if hitting pause or mute button
			if (hit.collider.name == "open") {
				
				rb2d.MoveRotation(-90f);
			} else {
				rb2d.MoveRotation(0f);
			}
		}
		else {
			rb2d.MoveRotation(0f);
		}
	}
}

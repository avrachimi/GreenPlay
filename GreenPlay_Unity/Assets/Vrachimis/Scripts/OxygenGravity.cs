using UnityEngine;
using System.Collections;

public class OxygenGravity : MonoBehaviour {

	public GameObject gravityObject;
	public float acceleration = 9.81f;
	public Sprite carbon;


	private Rigidbody2D rb2d;
	private SpriteRenderer spriteRenderer;

	private Transform lastPosition;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//add gravity
		rb2d.gravityScale = -acceleration;

		if ((transform.position.x < -1.5f) && (transform.position.y < 1) && (transform.position.y > -1)) {
			spriteRenderer.sprite = carbon;
			Debug.Log("HI");
		}
	}

	void FixedUpdate() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 2f);
		if (hit.collider != null) {
			//Debug.DrawLine(transform.position, transform.position,Color.blue);
		}
	}
}

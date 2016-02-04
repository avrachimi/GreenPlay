using UnityEngine;
using System.Collections;

public class OxygenGravity : MonoBehaviour {

	public GameObject gravityObject;
	public float acceleration = 9.81f;
	public Sprite carbon;
	public Sprite oxygen;


	private Rigidbody2D rb2d;
	private SpriteRenderer spriteRenderer;

	private Transform lastPosition;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		gravityObject = GameObject.Find("gravityObject");
	}
	
	// Update is called once per frame
	void Update () {
		//add gravity
		rb2d.gravityScale = -acceleration;
		Vector3 dir = (gravityObject.transform.position - transform.position).normalized;

		if ((transform.position.x < -1.5f) && (transform.position.y < 1) && (transform.position.y > -1)) {
			spriteRenderer.sprite = carbon;
			Debug.Log("HI");
		}
		else if ((transform.position.y < 0) && (transform.position.x > -0.65f) && (transform.position.x < 1.5f)) {

			//(speed * Time.fixedDeltaTime) makes the object move by 'speed' units per second, framerate independent
			rb2d.MovePosition(transform.position + dir * (4f * Time.fixedDeltaTime));
		}
		else if (transform.position.y > 15) {
			//Destroy(gameObject);
		}
		else if (transform.position.y > 1.83f) {
			//rb2d.MovePosition(transform.position + dir * (4f * Time.fixedDeltaTime));
			
		}
	}

	void FixedUpdate() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 2f);
		if (hit.collider != null) {
			//Debug.DrawLine(transform.position, transform.position,Color.blue);
		}
	}
}

using UnityEngine;
using System.Collections;

public class OxygenGravity : MonoBehaviour {

	public GameObject gravityObject;
	public float acceleration = 9.81f;
	public Sprite carbon;
	public Sprite oxygen;
	public float force;

	private Rigidbody2D rb2d;
	private SpriteRenderer spriteRenderer;

	private Transform lastPosition;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		gravityObject = GameObject.Find("gravityObject");
		rb2d.gravityScale = -acceleration;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		//add gravity
		Vector3 dir = (gravityObject.transform.position - transform.position).normalized;

		if ((transform.position.x < -1.5f) && (transform.position.y < 1) && (transform.position.y > -1)) {
			//spriteRenderer.sprite = carbon;
			rb2d.gravityScale = -0.3f;
			//Debug.Log("HI");
		}
		else if ((transform.position.y < 0) && (transform.position.x > -0.5f) && (transform.position.x < 1f)) {

			//(speed * Time.fixedDeltaTime) makes the object move by 'speed' units per second, framerate independent
			rb2d.MovePosition(transform.position + dir * (6.5f * Time.fixedDeltaTime));
			//rb2d.AddForce(new Vector2(0,force/100));
		}
		else if (transform.position.x < 0.2f && transform.position.x > -0.2f && transform.position.y > 3.22f && transform.position.y < 12f) {
			rb2d.MovePosition(transform.position + dir * (7f * Time.fixedDeltaTime));
			//rb2d.AddForce(new Vector2(0,force/100));
		}
		else if ((transform.position.x < -2.3f) && (transform.position.y < -1)) {
			rb2d.velocity = new Vector2(0,0);
		}
		else {
			rb2d.gravityScale = -acceleration;
		}
	}
}

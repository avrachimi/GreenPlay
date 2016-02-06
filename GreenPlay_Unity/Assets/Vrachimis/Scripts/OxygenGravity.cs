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
		//add gravity
		Vector3 dir = (gravityObject.transform.position - transform.position).normalized;

		if ((transform.position.x < -1.5f) && (transform.position.y < 1) && (transform.position.y > -1)) {
			spriteRenderer.sprite = carbon;
			rb2d.gravityScale = -0.3f;
			Debug.Log("HI");
		}
		else if ((transform.position.y < 0) && (transform.position.x > -0.65f) && (transform.position.x < 1.5f)) {

			//(speed * Time.fixedDeltaTime) makes the object move by 'speed' units per second, framerate independent
			rb2d.MovePosition(transform.position + dir * (4f * Time.fixedDeltaTime));
			//rb2d.AddForce(new Vector2(0,force/100));
		}
		else if (transform.position.x < 0.156f && transform.position.x > -0.146f && transform.position.y > 1.76f && transform.position.y < 6.87f) {
			rb2d.MovePosition(transform.position + dir * (6f * Time.fixedDeltaTime));
			//rb2d.AddForce(new Vector2(0,force/100));
		}
		else {
			rb2d.gravityScale = -acceleration;
		}
	}
}

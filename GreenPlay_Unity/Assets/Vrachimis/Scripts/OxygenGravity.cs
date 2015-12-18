using UnityEngine;
using System.Collections;

public class OxygenGravity : MonoBehaviour {

	public GameObject gravityObject;
	public float acceleration = 9.81f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<Rigidbody2D>().AddForce((gravityObject.transform.position - transform.position).normalized * acceleration);
		//GetComponent<Rigidbody2D>().AddForce(new Vector2(0,0.5f));
		GetComponent<Rigidbody2D>().gravityScale = -acceleration;
		//GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	}
}

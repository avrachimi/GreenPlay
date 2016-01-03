using UnityEngine;
using System.Collections;

public class CupMovement : MonoBehaviour {


	public GameObject center;
	public Transform[] wayPointList;
	public int currentWayPoint = 0; 
	public float speed = 4f;
	// Maximum turn rate in degrees per second.
	public float turningRate = 30f; 

	Transform targetWayPoint;

	// Rotation we should blend towards.
	private Quaternion _targetRotation = Quaternion.identity;
	private Rigidbody2D rb2d;
	private float timer;

	private Vector3 dir;
	private float angle;

	// Use this for initialization
	void Start () {
		
		rb2d  = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if ((transform.position.y < 0) && (transform.position.x > -0.8f) && (transform.position.x < 0.8f)) //ro
		{
			//make cup look at the tree
			//RIGIDBODY.MOVEROTATION
			dir = center.transform.position - transform.position;
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);

			//remove all childs, except the filter
			for (var i = transform.childCount - 1; i >= 0; i--)
			{
				// objectA is not the attached GameObject, so you can do all your checks with it.
				Transform objectA = transform.GetChild(i);
				if (objectA.name != "CupFilter")
				{
					objectA.transform.parent = null;
					
				}
			}

		} else //rotate object downwards
		{
			RotateObject(Vector3.down);
		}

		//NOT SURE IF THIS IS NEEDED. TEST LATER
		transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);



		//move cup smoothly back into its original rotation
		Vector3 targetUp = new Vector3(center.transform.position.x, center.transform.position.y, 0);
		float damping = 4;
		transform.up = Vector3.Slerp(transform.up, targetUp, Time.deltaTime * damping);
	}

	void FixedUpdate()
	{
		// check if there is a waypoint in the list
		if(currentWayPoint < this.wayPointList.Length)
		{
			if(targetWayPoint == null)
			{
				targetWayPoint = wayPointList[currentWayPoint];
				Debug.Log("Waypoint was empty. Object: " + gameObject.name);
			}
			timer += Time.fixedDeltaTime;
			walk();
		}
	}

	void walk(){
		// move towards the next waypsoint
		//transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position,   speed*Time.deltaTime);

		Vector2 dir = (targetWayPoint.position - transform.position);
		//rb2d.AddForce(dir  * speed * Time.deltaTime);
		rb2d.MovePosition(new Vector2(transform.position.x,transform.position.y)  + dir * speed / 5 * Time.fixedDeltaTime);
		//rb2d.MovePosition(new Vector2(Mathf.Lerp(rb2d.position.x, rb2d.position.x + dir.x, timer),Mathf.Lerp(rb2d.position.y, rb2d.position.y + dir.y, timer)));

		if(transform.position == targetWayPoint.position)
		{
			currentWayPoint ++ ;
			targetWayPoint = wayPointList[currentWayPoint];
			Debug.Log("Added the next waypoint(" + currentWayPoint + "). Object: " + gameObject.name);
		}

		if (currentWayPoint == wayPointList.Length-1) currentWayPoint = -1; //keep going around the waypoints
	}

	//make atoms childs so that they stay together
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "atom")
		{
			coll.gameObject.transform.parent = this.transform;
		}
	}

	//remove atoms from childs to moe on their own
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "atom")
		{
			coll.gameObject.transform.parent = null;
		}
	}

	void RotateObject(Vector3 angles)
	{
		_targetRotation = Quaternion.Euler(angles);
	}
}

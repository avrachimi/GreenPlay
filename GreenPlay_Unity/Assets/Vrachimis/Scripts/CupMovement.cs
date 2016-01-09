using UnityEngine;
using System.Collections;

public class CupMovement : MonoBehaviour {


	public GameObject center;
	public Transform[] wayPointList;
	public int currentWayPoint = 0; 
	public float speed = 4f;
	// Maximum turn rate in degrees per second.
	public float turningRate = 30f;
	public float arrivalDistance = 0.1f;

	Transform targetWayPoint;

	// Rotation we should blend towards.
	private Quaternion _targetRotation = Quaternion.identity;
	private Rigidbody2D rb2d;
	private float timer;
	float lastDistanceToTarget = 0f;
	private Vector3 direction;
	private float angle;

	// Use this for initialization
	void Start () {
		
		rb2d  = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

		direction = center.transform.position - transform.position;
		float ang = Vector2.Angle(rb2d.transform.position, direction);

		if ((transform.position.y < 0) && (transform.position.x > -0.8f) && (transform.position.x < 0.8f)) //rotate towards the tree in the center
		{

			//make cup look at the tree
			//RIGIDBODY.MOVEROTATION


			Vector3 cross = Vector3.Cross(rb2d.transform.position, direction);

			if (cross.z > 0) ang = 360 - ang;
			rb2d.MoveRotation(ang);
			//remove all childs, except the filter
			/*for (var i = transform.childCount - 1; i >= 0; i--)
			{
				// objectA is not the attached GameObject, so you can do all your checks with it.
				Transform objectA = transform.GetChild(i);
				if (objectA.name != "CupFilter")
				{
					objectA.transform.parent = null;
					
				}
			}*/

		} else //rotate object downwards
		{
			//RotateObject(Vector3.down);
			rb2d.MoveRotation(0);
		}

		//NOT SURE IF THIS IS NEEDED. TEST LATER
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);



		//move cup smoothly back into its original rotation
		/*Vector3 targetUp = new Vector3(center.transform.position.x, center.transform.position.y, 0);
		float damping = 4;
		transform.up = Vector3.Slerp(transform.up, targetUp, Time.deltaTime * damping);*/
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
			walk();
		}
	}


	void walk()
	{
		//If we're close to target, or overshot it, get next waypoint;
		float distanceToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
		if((distanceToTarget < arrivalDistance) || (distanceToTarget > lastDistanceToTarget))
		{
			currentWayPoint++;
			if(currentWayPoint >= wayPointList.Length)
			{
				currentWayPoint = 0;
			}
			targetWayPoint = wayPointList[currentWayPoint];
			lastDistanceToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
			Debug.Log("Added the next waypoint(" + currentWayPoint + "). Object: " + gameObject.name);
		}else{
			lastDistanceToTarget = distanceToTarget;
		}

		//Get direction to the waypoint.
		//Normalize so it doesn't change with distance.
		Vector3 dir = (targetWayPoint.position - transform.position).normalized;

		//(speed * Time.fixedDeltaTime) makes the object move by 'speed' units per second, framerate independent
		rb2d.MovePosition(transform.position + dir * (speed * Time.fixedDeltaTime));

	}

	/*
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
	}*/

	void RotateObject(Vector3 angles)
	{
		_targetRotation = Quaternion.Euler(angles);
	}
}

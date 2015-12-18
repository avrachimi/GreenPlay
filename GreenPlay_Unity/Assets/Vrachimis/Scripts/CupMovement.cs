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

	private Vector3 dir;
	private float angle;

	// Use this for initialization
	void Start () {
		
		/*UpdatePosition = true;
		DelayUpdatePos = 1;
		FirstYPos = 5;
		MovementSpeed = 5;*/

		//iTween.MoveTo(this.gameObject,iTween.Hash("path",iTweenPath.GetPath("CupPath"),"time",20));
	
	}
	
	// Update is called once per frame
	void Update () {

		if ((transform.position.y < 0) && (transform.position.x > -1f) && (transform.position.x < 1f)) //ro
		{
			dir = center.transform.position - transform.position;
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);

		} else
		{
			RotateObject(Vector3.down);
		}

		transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);

		// check if we have somewere to walk
		if(currentWayPoint < this.wayPointList.Length)
		{
			if(targetWayPoint == null)
				targetWayPoint = wayPointList[currentWayPoint];
			walk();
		}

		Vector3 targetUp = new Vector3(center.transform.position.x, center.transform.position.y, 0);
		float damping = 4;
		transform.up = Vector3.Slerp(transform.up, targetUp, Time.deltaTime * damping);
	}

	void walk(){
		// rotate towards the target
		//transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed*Time.deltaTime, 0.0f);

		// move towards the target
		transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position,   speed*Time.deltaTime);

		if(transform.position == targetWayPoint.position)
		{
			currentWayPoint ++ ;
			targetWayPoint = wayPointList[currentWayPoint];
		}

		if (currentWayPoint == wayPointList.Length-1) currentWayPoint = 0; //keep going around the waypoints
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

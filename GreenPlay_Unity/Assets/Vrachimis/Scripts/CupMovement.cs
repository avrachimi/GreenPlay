using UnityEngine;
using System.Collections;

public class CupMovement : MonoBehaviour {


	public GameObject center;
	public Transform[] wayPointList;
	public int currentWayPoint = 0; 
	public float speed = 4f;

	Transform targetWayPoint;

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
		dir = center.transform.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

		// check if we have somewere to walk
		if(currentWayPoint < this.wayPointList.Length)
		{
			if(targetWayPoint == null)
				targetWayPoint = wayPointList[currentWayPoint];
			walk();
		}
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

		if (currentWayPoint == 8) currentWayPoint = 0;
	}
}

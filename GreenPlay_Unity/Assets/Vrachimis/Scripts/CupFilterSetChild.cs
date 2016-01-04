using UnityEngine;
using System.Collections;

public class CupFilterSetChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "atom")
		{
			coll.gameObject.transform.parent = transform.parent;
			Debug.Log("Entered filter");
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "atom")
		{
			coll.gameObject.transform.parent = null;
		}
	}*/
}

using UnityEngine;
using System.Collections;

public class CupFilterScoreIncrement : MonoBehaviour {

	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
		if (gameManagerObject != null)
		{
			gameManager = gameManagerObject.GetComponent <GameManager>();
		}
		if (gameManager == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}


	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "atom")
		{
			coll.gameObject.transform.parent = transform.parent;
			gameManager.incrementScore(1);
			Debug.Log("Entered filter");
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "atom")
		{
			coll.gameObject.transform.parent = null;
		}
	}
}

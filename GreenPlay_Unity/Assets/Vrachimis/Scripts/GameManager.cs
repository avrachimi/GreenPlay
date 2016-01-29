using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GUIText scoreText;
	public GameObject circleDoor;
	public GameObject cupObject;
	public GameObject oxygenSet;

	private int score;
	private int cupCounter = 0;
	private int oxygenCounter = 0;
	private Rigidbody2D rb2dCircleDoor;
	// Use this for initialization
	void Start () {
		score = 0;
		updateScore();
		rb2dCircleDoor = circleDoor.GetComponent<Rigidbody2D>();
		InvokeRepeating("spawnCup",1f,1.461f);
		InvokeRepeating("spawnOxygen",0f,0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		touchInput();


	}

	public void incrementScore(int amount)
	{
		score += amount;
		updateScore();
	}

	void updateScore()
	{
		scoreText.text = "" + score;
	}

	void touchInput()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			//LATER: make sure to check if hitting pause or mute button
			if (hit.collider.name == "open") {
				rb2dCircleDoor.MoveRotation(-90f);
			} else if (hit.collider.name == "pause"){
				Time.timeScale = 0;
				//should display options menu
			}
		}
		else if (hit == null || hit.collider == null) {
			rb2dCircleDoor.MoveRotation(0f);
			Time.timeScale = 1;
		}
	}

	void spawnCup()
	{
		Instantiate(cupObject,new Vector3(1.5f,-2.5f,0),Quaternion.identity);
		cupCounter += 1;
		if(cupCounter >= 7) CancelInvoke("spawnCup");
	}

	void spawnOxygen()
	{
		Instantiate(oxygenSet, new Vector3(0.5f,0,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(-0.5f,0,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(0,0,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(0.5f,-0.5f,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(-0.5f,-0.5f,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(0,-0.5f,0), Quaternion.identity);
		oxygenCounter += 6;
		if(oxygenCounter >= 100) CancelInvoke("spawnOxygen");
	}
}

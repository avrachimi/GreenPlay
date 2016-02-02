using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GUIText scoreText;
	public GameObject circleDoor;
	public GameObject cupObject;
	public GameObject oxygenSet;
	public int cupsDestroyed = 0;

	private int score;
	private int cupCounter = 0;
	private int oxygenCounter = 0;
	private Rigidbody2D rb2dCircleDoor;

	private CupMovement cupMovement;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;



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
		
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began) {
				rb2dCircleDoor.MoveRotation(-90f);
				Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
				if (hit != null && hit.collider != null) {
					Debug.Log ("I'm hitting "+hit.collider.name);
					//LATER: make sure to check if hitting pause or mute button
					if (hit.collider.name == "pause") {
						Time.timeScale = 0;
					} else {
						Time.timeScale = 1;
					}
				}
			}
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				rb2dCircleDoor.MoveRotation(0f);
			}
		}
		/*
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			//LATER: make sure to check if hitting pause or mute button
			if (hit.collider.name == "pause"){
				Time.timeScale = 0;
				//should display options menu
			}
			else {
				//rb2dCircleDoor.MoveRotation(0f);
			}
		}
		else if (hit == null || hit.collider == null) {
			//rb2dCircleDoor.MoveRotation(0f);
			Time.timeScale = 1;
		}*/
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

	public void endGame()
	{
		//scoreText.enabled = false;
		scoreText.text = "asdf";
		Debug.Log("B");
	}

	public void incrementCupsDestroyed() 
	{
		cupsDestroyed += 1;
		Debug.Log("increment");
	}
}

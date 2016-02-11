using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GUIText scoreText;
	public GameObject circleDoor;
	public GameObject circleDoor2;
	public CameraController cameraController;
	public GameObject cupObject;
	public GameObject oxygenSet;
	public int cupsDestroyed = 0;
	public GUIStyle playStyle;
	public GUIStyle homeStyle;
	public GUIStyle pauseStyle;
	public Vector2 playPos;
	public Vector2 homePos;
	public Vector2 pausePos;
	public Vector2 playSize;
	public Vector2 homeSize;
	public Vector2 pauseSize;

	private bool pausePressed = false;
	private int score1Size = 3;
	private int score2Size = 3;
	private int score3Size = 4;
	private int score4Size = 4;

	private int score = 0;
	private int highScore;

	public int cupCounter = 0;
	private int oxygenCounter = 0;
	private Rigidbody2D rb2dCircleDoor;
	private Rigidbody2D rb2dCircleDoor2;

	private CupMovement cupMovement;

	// Use this for initialization
	void Start () {

		Application.targetFrameRate = 60;
		score1Size = 3;
		score2Size = 3;
		score3Size = 4;
		score4Size = 4;

		Debug.Log("GameManager RUNS!");

		Application.targetFrameRate = 60;
		GameObject cameraControllerObject = GameObject.FindWithTag("MainCamera");
		cameraController = cameraControllerObject.GetComponent<CameraController>();

		scoreText.fontSize = Mathf.Min(Screen.height,Screen.width)/3;
		scoreText.pixelOffset = new Vector2(0,Screen.height * 0.13f);
		scoreText.anchor = TextAnchor.UpperCenter;
		scoreText.alignment = TextAlignment.Center;
		score = 0;

		updateScore();
		rb2dCircleDoor = circleDoor.GetComponent<Rigidbody2D>();
		rb2dCircleDoor2 = circleDoor2.GetComponent<Rigidbody2D>();

		InvokeRepeating("spawnCup",0f,2.045f); //2.922f
		InvokeRepeating("spawnOxygen",0f,0.2f);

		highScore = PlayerPrefs.GetInt("HighScore");
	}
	
	// Update is called once per frame
	void Update () {

		playStyle.fixedWidth = Screen.width - (Screen.width * playSize.x);
		playStyle.fixedHeight = Screen.height - (Screen.height * playSize.y);
		homeStyle.fixedWidth = Screen.width - (Screen.width * homeSize.x);
		homeStyle.fixedHeight = Screen.height - (Screen.height * homeSize.y);
		pauseStyle.fixedWidth = Screen.width - (Screen.width * pauseSize.x);
		pauseStyle.fixedHeight = Screen.height - (Screen.height * pauseSize.y);


		if (score.ToString().Length == 1) scoreText.fontSize = Mathf.Min(Screen.height,Screen.width)/score1Size;
		else if (score.ToString().Length == 2) scoreText.fontSize = Mathf.Min(Screen.height,Screen.width)/score2Size;
		else if (score.ToString().Length == 3) scoreText.fontSize = Mathf.Min(Screen.height,Screen.width)/score3Size;
		else if (score.ToString().Length == 4) scoreText.fontSize = Mathf.Min(Screen.height,Screen.width)/score4Size;

		updateScore();
		touchInput();

		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);

		}

	}

	public void startGame()
	{
		cupsDestroyed = 0;
		oxygenCounter = 0;
		cupCounter = 0;
		score = 0;
		updateScore();
		scoreText.enabled = true;
		cameraController.currentTime = 0f;
		foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("Cup"))
		{
			Destroy(fooObj);
		}

		foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("atom"))
		{
			Destroy(fooObj);
		}

		InvokeRepeating("spawnCup",0f,2.045f); //2.922f
		InvokeRepeating("spawnOxygen",0f,0.2f);

		highScore = PlayerPrefs.GetInt("HighScore");	
	}

	public void incrementScore(int amount)
	{
		score += amount; //changed it for testing new raycast
		//score = amount;
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
				rb2dCircleDoor2.MoveRotation(90f);
				//circleDoor.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,-90), 1000 * Time.deltaTime);
				//collCircleDoor.enabled = false;
				Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
				if (hit != null && hit.collider != null) {
					//Debug.Log ("I'm hitting "+hit.collider.name);
					//LATER: make sure to check if hitting pause or mute button
					if (hit.collider.name == "pause") {
						pausePressed = togglePause();
						//.timeScale = 0;
					}
				}
			}
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				//collCircleDoor.enabled = true;
				rb2dCircleDoor.MoveRotation(0f);
				rb2dCircleDoor2.MoveRotation(0f);
				//circleDoor.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,0), 1000 * Time.deltaTime);
			}
		}
	}

	void OnGUI()
	{
		if (pausePressed) {
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * pausePos.x ), Screen.height/2 - (Screen.height * pausePos.y),0,0),"",pauseStyle)) {
				//do nothing. just to display the graphic
			}
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * playPos.x ), Screen.height/2 - (Screen.height * playPos.y), 300 , Screen.height - (Screen.height * playSize.y)),"",playStyle)) {
				//pausePressed = false;
				pausePressed = togglePause();
			}
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * homePos.x ), Screen.height/2 - (Screen.height * homePos.y), Screen.width - (Screen.width * homeSize.x) , Screen.height - (Screen.height * homeSize.y)),"",homeStyle)) {
				save();
				pausePressed = togglePause();
				Time.timeScale = 1;
				SceneManager.LoadScene(0);
			}

		}
	}

	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}

	void spawnCup()
	{
		Instantiate(cupObject,new Vector3(2.7f,-4.5f,0),Quaternion.identity);
		cupCounter += 1;
		if(cupCounter >= 7) CancelInvoke("spawnCup");
	}

	void spawnOxygen()
	{
		Instantiate(oxygenSet, new Vector3(1f,0,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(-1f,0,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(0,0,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(1f,-1f,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(-1f,-1f,0), Quaternion.identity);
		Instantiate(oxygenSet, new Vector3(0,-1f,0), Quaternion.identity);
		oxygenCounter += 6;
		if(oxygenCounter >= 50) CancelInvoke("spawnOxygen");
	}

	public void endGame()
	{
		scoreText.enabled = false;
		scoreText.text = "asdf";
		//Debug.Log("B");
		save();
		cameraController.moveCamera();
	}

	public void incrementCupsDestroyed(int num) 
	{
		cupsDestroyed = num;
		//Debug.Log("increment" + cupsDestroyed);
	}

	void OnApplicationPause() 
	{
		save();
		pausePressed = true;
		Time.timeScale = 0;
	}

	void OnApplicationFocus()
	{
		Time.timeScale = 1;
		pausePressed = false;
	}

	public void save()
	{
		PlayerPrefs.SetInt("HighScore", highScore);
		PlayerPrefs.SetInt("Score", score);
		PlayerPrefs.Save();
	}
}

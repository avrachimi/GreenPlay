using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GUIText scoreText;
	public GameObject circleDoor;
	public GameObject circleDoor2;
	public CameraController cameraController;
	public GameObject cupObject;
	public GameObject oxygenSet;
	public int cupsDestroyed = 0;

	private int score = 0;
	private int highScore;

	public int cupCounter = 0;
	private int oxygenCounter = 0;
	private Rigidbody2D rb2dCircleDoor;
	private Rigidbody2D rb2dCircleDoor2;
	private CircleCollider2D collCircleDoor;

	private CupMovement cupMovement;

	// Use this for initialization
	void Start () {
		Debug.Log("GameManager RUNS!");
		/*AppLovin.SetSdkKey("fyTagFDisKNQOwVukdLCv5_iCinuTdf8aDiFTxzKFKleEFWztt9nz9T9sE1KthSRwAhN5ehLzR_CgW9XNIIKSm");
		AppLovin.InitializeSdk ();
		AppLovin.PreloadInterstitial();*/

		Application.targetFrameRate = 60;
		collCircleDoor = circleDoor.GetComponent<CircleCollider2D>();
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
		touchInput();

		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);

		}

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
				rb2dCircleDoor2.MoveRotation(90f);
				//circleDoor.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,-90), 1000 * Time.deltaTime);
				//collCircleDoor.enabled = false;
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
				//collCircleDoor.enabled = true;
				rb2dCircleDoor.MoveRotation(0f);
				rb2dCircleDoor2.MoveRotation(0f);
				//circleDoor.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,0), 1000 * Time.deltaTime);
			}
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
		if(oxygenCounter >= 50) CancelInvoke("spawnOxygen");
	}

	public void endGame()
	{
		scoreText.enabled = false;
		scoreText.text = "asdf";
		Debug.Log("B");
		PlayerPrefs.SetInt("Score", score);
		PlayerPrefs.Save();
		cameraController.moveCamera();
	}

	public void incrementCupsDestroyed(int num) 
	{
		cupsDestroyed = num;
		Debug.Log("increment" + cupsDestroyed);
	}

	void OnApplicationPause() 
	{
		PlayerPrefs.Save();
		Time.timeScale = 0;
	}

	void OnApplicationFocus()
	{
		Time.timeScale = 1;
	}
}

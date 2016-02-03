using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	public ResizeBackground resizeBackground;
	public float time = 0.1f;
	public float maxVelocity = 1f;

	private Vector3 velocity = Vector3.zero;

	private Vector3 startPos;
	// Use this for initialization
	void Start () {
		GameObject resizeBackgroundObject = GameObject.Find("Background");
		if (resizeBackgroundObject != null)
		{
			resizeBackground = resizeBackgroundObject.GetComponent <ResizeBackground>();
		}
		if (resizeBackground == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void moveCamera() 
	{
		Vector3 targetX = new Vector3(2 * resizeBackground.worldScreenWidth,0,-10);
		//transform.position = Vector3.Slerp(transform.position, targetX, Time.deltaTime);
		transform.position = Vector3.SmoothDamp(transform.position, targetX, ref velocity, time /10);
		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.02f) {
			SceneManager.LoadScene("GameOverScene");
		}
	}
}

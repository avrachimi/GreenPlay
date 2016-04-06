using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using ChartboostSDK;
using Facebook.Unity;

public class CameraController : MonoBehaviour {

	public ResizeBackground resizeBackground;
	public float time = 0.1f;
	public float maxVelocity = 1f;

	private Vector3 velocity = Vector3.zero;

	public float currentTime = 0f;
	float timeToMove = 2f;

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
		//DontDestroyOnLoad(transform.gameObject);

	}


	
	// Update is called once per frame
	void Update () {
		


	}

	public void moveCamera() 
	{
		Vector3 targetX = new Vector3(2 * resizeBackground.worldScreenWidth,0,-10);
		//transform.position = Vector3.Slerp(transform.position, targetX, Time.deltaTime);
		//transform.position = Vector3.SmoothDamp(transform.position, targetX, ref velocity, time /10);
		if (currentTime <= timeToMove)
		{
			currentTime += Time.deltaTime;
			transform.position = new Vector3(Mathf.Lerp(0f, targetX.x, currentTime / timeToMove),0,-10);
		}

		if (transform.position.x < 0.2f) {
			//currentTime = 0;
		}
		/*if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.02f && closeScene) {
			//Chartboost.showInterstitial(CBLocation.HomeScreen);
			SceneManager.LoadScene(2);
		}

		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.09f && shownAd == false) {
			AppLovin.ShowInterstitial();
			//Chartboost.showInterstitial(CBLocation.Default);
			shownAd = true;
		}*/
	}

	// Awake function from Unity's MonoBehavior
	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}


}

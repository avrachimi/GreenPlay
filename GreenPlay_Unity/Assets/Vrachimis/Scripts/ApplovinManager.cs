using UnityEngine;
using System.Collections;

public class ApplovinManager : MonoBehaviour {

	public bool show_Applovin;

	public ResizeBackground resizeBackground;

	private Vector3 targetX;
	private bool closeScene = false;
	private bool isOnce = true;


	void Start () {
		show_Applovin = true;

		GameObject resizeBackgroundObject = GameObject.Find("Background");
		if (resizeBackgroundObject != null)
		{
			resizeBackground = resizeBackgroundObject.GetComponent<ResizeBackground>();
		}
		if (resizeBackground == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void Update () {
		targetX = new Vector3(2 * resizeBackground.worldScreenWidth,0,-10);

		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.02f && isOnce) {
			closeScene = true;
			isOnce = false;
		} else if (transform.position.x < 5f) {
			isOnce = true;
		}

		if (show_Applovin) {
			
			ApplovinLoader.myApplovin.preloadInterstitial ();

		}

		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.02f && closeScene == true) {

			show_Applovin = true;
			closeScene = false;
			ApplovinLoader.myApplovin.showInterstitial ();
			Debug.Log ("-------------------------Show AppLovin Interstitial.");
		} else
		{
			closeScene = false;
		}
	}

	void onAppLovinEventReceived(string ev)
	{
		Debug.Log(ev);
		if (ev.Contains("REWARDAPPROVEDINFO"))
		{
			// Split the string into its three components.
			//char[] delimiter = { '|' };
			//string[] split = ev.Split(delimiter);
			// Pull out and parse the amount of virtual currency.
			//double amount = double.Parse(split[1]);
			// Pull out the name of the virtual currency
			//string currencyName = split[2];
			// Do something with this info - for example, grant coins to the user
			//myFunctionToUpdateBalance(currencyName, amount);
		}
		if (ev.Contains("DISPLAYEDINTER") || ev.Contains("VIDEOBEGAN"))
		{
			Time.timeScale = 0.0f;
			AudioListener.pause = true;
			closeScene = false;
		}
		if (ev.Contains("HIDDENINTER") || ev.Contains("VIDEOSTOPPED"))
		{
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			closeScene = false;
			//AppLovin.PreloadInterstitial();
		}
		if (ev.Contains("LOADEDINTER"))
		{
			// The last ad load was successful.
			// Probably do AppLovin.ShowInterstitial();
			//AppLovin.ShowInterstitial();
			//shownAd = true;
			//closeScene = true;
			show_Applovin = false;
		}
		if (string.Equals(ev, "LOADINTERFAILED")) {
			//AppLovin.PreloadInterstitial();
			show_Applovin = true;
		}
		if (ev.Contains("CLICKED")) {
			//save the score and high score
		}
	}

}
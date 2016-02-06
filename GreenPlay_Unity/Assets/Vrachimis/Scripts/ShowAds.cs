using UnityEngine;
using System.Collections;
using ChartboostSDK;
using UnityEngine.SceneManagement;

public class ShowAds : MonoBehaviour {

	public GameObject cameraPos ;

	private bool closeScene = false;
	private bool shownAppLovin = false;
	private bool shownAd = false;
	private Vector3 targetX;
	private int randomNum = 0;
	private Random rnd = new Random();

	public ResizeBackground resizeBackground;
	// Use this for initialization
	void Start () {
		//Chartboost.showInterstitial(CBLocation.Default);
		GameObject resizeBackgroundObject = GameObject.Find("Background");
		if (resizeBackgroundObject != null)
		{
			resizeBackground = resizeBackgroundObject.GetComponent<ResizeBackground>();
		}
		if (resizeBackground == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		AppLovin.SetUnityAdListener("AppLovin Object");
		AppLovin.PreloadInterstitial();


	}
	
	// Update is called once per frame
	void Update () {
		targetX = new Vector3(2 * resizeBackground.worldScreenWidth,0,-10);
		Debug.Log(targetX);
		Debug.Log(cameraPos.transform.position);
		if (!Chartboost.hasInterstitial(CBLocation.Default) || !Chartboost.hasMoreApps(CBLocation.GameOver)) {
			//Chartboost.showInterstitial(CBLocation.Default);
			Chartboost.cacheInterstitial(CBLocation.Default);
			Chartboost.cacheMoreApps(CBLocation.GameOver);
		}
		
		if (cameraPos.transform.position.x < targetX.x + 0.02f && cameraPos.transform.position.x > targetX.x - 0.02f && closeScene) {
			//Chartboost.showInterstitial(CBLocation.HomeScreen);
			SceneManager.LoadScene(2);
		}

		if (cameraPos.transform.position.x < targetX.x + 0.02f && cameraPos.transform.position.x > targetX.x - 0.09f && shownAd == false) {
			Debug.Log("Applovin");
			AppLovin.ShowInterstitial();
			//Chartboost.showInterstitial(CBLocation.Default);
			//Chartboost.showMoreApps(CBLocation.GameOver);
			shownAd = true;
			shownAppLovin = true;
		}
		else if (cameraPos.transform.position.x < targetX.x + 0.02f && cameraPos.transform.position.x > targetX.x - 0.09f && shownAppLovin == false) {
			//AppLovin.ShowInterstitial();
			randomNum = Random.Range(1, 7);
			Debug.Log("Chartboost");
			if (randomNum <= 4) {
				Chartboost.showInterstitial(CBLocation.Default);
			}
			else {
				Chartboost.showMoreApps(CBLocation.GameOver);
			}
			shownAppLovin = true;
			shownAd = true;
		}
	}

	void onAppLovinEventReceived(string ev)
	{
		Debug.Log(ev);
		if (ev.Contains("REWARDAPPROVEDINFO"))
		{
			// Split the string into its three components.
			char[] delimiter = { '|' };
			string[] split = ev.Split(delimiter);
			// Pull out and parse the amount of virtual currency.
			//double amount = double.Parse(split[1]);
			// Pull out the name of the virtual currency
			//string currencyName = split[2];
			// Do something with this info - for example, grant coins to the user
			//myFunctionToUpdateBalance(currencyName, amount);
		}
		if (string.Equals(ev, "DISPLAYEDINTER") || string.Equals(ev, "VIDEOBEGAN"))
		{
			Time.timeScale = 0.0f;
			AudioListener.pause = true;
		}
		if (ev.Contains("HIDDENINTER") || string.Equals(ev, "VIDEOSTOPPED"))
		{
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			AppLovin.PreloadInterstitial();
			closeScene = true;
			shownAppLovin = true;
			shownAd = true;
		}
		if (string.Equals(ev, "LOADEDINTER"))
		{
			// The last ad load was successful.
			// Probably do AppLovin.ShowInterstitial();
			//AppLovin.ShowInterstitial();
			//shownAd = true;
			//closeScene = true;
		}
		if (string.Equals(ev, "LOADINTERFAILED")) {
			//AppLovin.PreloadInterstitial();
			shownAppLovin = false;
		}
		if (string.Equals(ev, "LOADFAILED"))
		{
			// The last ad load failed.
			shownAppLovin = false;
		}
	}

	void OnEnable() 
	{
		Chartboost.didInitialize += didInitialize;
		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial += didDismissInterstitial;
		Chartboost.didCloseInterstitial += didCloseInterstitial;
	}

	void OnDisable()
	{
		Chartboost.didInitialize -= didInitialize;
		Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial -= didDismissInterstitial;
		Chartboost.didCloseInterstitial -= didCloseInterstitial;
	}

	void didInitialize(bool status) {
		AddLog(string.Format("didInitialize: {0}", status));
	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) {
		AddLog(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
		//closeScene = true;
	}

	void didDismissInterstitial(CBLocation location) {
		AddLog("didDismissInterstitial: " + location);
		closeScene = true;
	}

	void didCloseInterstitial(CBLocation location) {
		AddLog("didCloseInterstitial: " + location);
		closeScene = true;
	}

	void AddLog(string text)
	{
		Debug.Log(text);

	}
}

using UnityEngine;
using System.Collections;
using ChartboostSDK;
using UnityEngine.SceneManagement;

public class ShowAds : MonoBehaviour {

	public GameObject cameraPos ;
	public GUIStyle style;

	private bool closeScene = false;
	private bool shownAppLovin = false;
	private bool shownAd = false;
	private bool failedToLoadAppLovin = true;
	private Vector3 targetX;
	private int randomNum = 0;
	private string debg = "none";

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
		failedToLoadAppLovin = true;
		AppLovin.SetUnityAdListener("AppLovin Object");
		AppLovin.PreloadInterstitial();

		if (!Chartboost.hasInterstitial(CBLocation.Default)) {
			//Chartboost.showInterstitial(CBLocation.Default);
			Chartboost.cacheInterstitial(CBLocation.Default);

		}
		if (!Chartboost.hasMoreApps(CBLocation.GameOver)) {
			Chartboost.cacheMoreApps(CBLocation.GameOver);
		}

		randomNum = Random.Range(1, 7);
		shownAppLovin = true;
		shownAd = false;


		InvokeRepeating("cacheAds",2,15);

	}

	void OnGUI()
	{
		GUI.Label(new Rect(100,100,100,100),"" + debg,style);
	}
	
	// Update is called once per frame
	void Update () {
		targetX = new Vector3(2 * resizeBackground.worldScreenWidth,0,-10);
		Debug.Log(targetX);
		Debug.Log(cameraPos.transform.position);

		
		if (cameraPos.transform.position.x < targetX.x + 0.02f && cameraPos.transform.position.x > targetX.x - 0.02f && closeScene) {
			//cameraPos.transform.position.x < targetX.x + 0.02f && cameraPos.transform.position.x > targetX.x - 0.02f && 
			//Chartboost.showInterstitial(CBLocation.HomeScreen);
			debg = "close scene";
			SceneManager.LoadScene(2);
		}

		if (cameraPos.transform.position.x < targetX.x + 0.02f && cameraPos.transform.position.x > targetX.x - 0.09f && shownAd == false) {
			Debug.Log("Applovin");
			debg = "app lovin";
			AppLovin.ShowInterstitial();
			shownAd = true;
			//shownAppLovin = true;
		}
		else if (cameraPos.transform.position.x < targetX.x + 0.02f && cameraPos.transform.position.x > targetX.x - 0.09f && shownAppLovin == false) {
			//AppLovin.ShowInterstitial();
			debg = "chartboost";
			Debug.Log("Chartboost");
			Chartboost.showInterstitial(CBLocation.Default);
			/*if (randomNum <= 4) {
				Chartboost.showInterstitial(CBLocation.Default);
			}
			else {
				Chartboost.showMoreApps(CBLocation.GameOver);
			}*/
			shownAppLovin = true;
			//shownAd = true;
		}
	}

	void cacheAds()
	{
		if (!Chartboost.hasInterstitial(CBLocation.Default)) {
			//Chartboost.showInterstitial(CBLocation.Default);
			Chartboost.cacheInterstitial(CBLocation.Default);

		}
		if (!Chartboost.hasMoreApps(CBLocation.GameOver)) {
			Chartboost.cacheMoreApps(CBLocation.GameOver);
		}

		if (failedToLoadAppLovin) {
			AppLovin.PreloadInterstitial();
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
		if (ev.Contains("DISPLAYEDINTER") || ev.Contains("VIDEOBEGAN"))
		{
			shownAppLovin = true;
			shownAd = true;
			Time.timeScale = 0.0f;
			AudioListener.pause = true;
		}
		if (ev.Contains("HIDDENINTER") || ev.Contains("VIDEOSTOPPED"))
		{
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			//AppLovin.PreloadInterstitial();
			closeScene = true;
			shownAppLovin = true;
			shownAd = true;
		}
		if (ev.Contains("LOADEDINTER"))
		{
			// The last ad load was successful.
			// Probably do AppLovin.ShowInterstitial();
			//AppLovin.ShowInterstitial();
			//shownAd = true;
			//closeScene = true;
			debg = "loaded app lovin";
			failedToLoadAppLovin = false;
		}
		if (string.Equals(ev, "LOADINTERFAILED")) {
			//AppLovin.PreloadInterstitial();
			debg = "failed applovin";
			failedToLoadAppLovin = true;
			shownAppLovin = false;
			shownAd = true;
		}
		if (ev.Contains("CLICKED")) {
			//save the score and high score
		}
	}

	void OnEnable() 
	{
		Chartboost.didFailToLoadMoreApps += didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps += didDismissMoreApps;
		Chartboost.didCloseMoreApps += didCloseMoreApps;
		Chartboost.didClickInterstitial += didClickInterstitial;
		Chartboost.didClickMoreApps += didClickMoreApps;
		Chartboost.didInitialize += didInitialize;
		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial += didDismissInterstitial;
		Chartboost.didCloseInterstitial += didCloseInterstitial;
	}

	void OnDisable()
	{
		Chartboost.didFailToLoadMoreApps -= didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps -= didDismissMoreApps;
		Chartboost.didCloseMoreApps -= didCloseMoreApps;
		Chartboost.didClickInterstitial -= didClickInterstitial;
		Chartboost.didClickMoreApps -= didClickMoreApps;
		Chartboost.didInitialize -= didInitialize;
		Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial -= didDismissInterstitial;
		Chartboost.didCloseInterstitial -= didCloseInterstitial;
	}

	void didInitialize(bool status) {
		AddLog(string.Format("didInitialize: {0}", status));
		debg = "CB Init";
	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) {
		AddLog(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
		debg = "failed chartboost";
		closeScene = true;
	}

	void didDismissInterstitial(CBLocation location) {
		AddLog("didDismissInterstitial: " + location);
		debg = "failed chartboost";
		closeScene = true;
	}

	void didCloseInterstitial(CBLocation location) {
		AddLog("didCloseInterstitial: " + location);
		debg = "closed chartboost";
		closeScene = true;
	}

	void didClickMoreApps(CBLocation location) {
		//save data
	}

	void didClickInterstitial(CBLocation location) {
		//save data
	}

	// Called after a MoreApps page has been dismissed.
	void didDismissMoreApps(CBLocation location) {
		
	}

	// Called after a MoreApps page has been closed.
	void didCloseMoreApps(CBLocation location) {
		closeScene = true;
	}
	// Called after a MoreApps page attempted to load from the Chartboost API
	// servers but failed.
	void didFailToLoadMoreApps(CBLocation location, CBImpressionError error) {
		
	}

	void AddLog(string text)
	{
		Debug.Log(text);

	}
}

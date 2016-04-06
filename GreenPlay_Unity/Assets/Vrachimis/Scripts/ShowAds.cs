using UnityEngine;
using System.Collections;
using ChartboostSDK;
using UnityEngine.SceneManagement;

public class ShowAds : MonoBehaviour {

	public GameObject cameraPos ;
	public GUIStyle style;

	public GameManager gameManager;

	private bool closeScene = false;
	private bool shownChartboost = false;
	private bool shownAd = true;
	private bool failedToLoadAppLovin = true;
	private bool failedToLoadInterstitial = true;
	private bool failedToLoadMoreApps = true;
	private Vector3 targetX;
	private int randomNum = 0;
	private string debg = "none";
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

		if (!Chartboost.hasInterstitial(CBLocation.Default)) {
			//Chartboost.showInterstitial(CBLocation.Default);
			Chartboost.cacheInterstitial(CBLocation.Default);

		}
		if (!Chartboost.hasMoreApps(CBLocation.GameOver)) {
			Chartboost.cacheMoreApps(CBLocation.GameOver);
		}

		randomNum = Random.Range(1, 7);
		shownChartboost = true;
		shownAd = false;


		InvokeRepeating("cacheAds",2,15);

	}

	void OnGUI()
	{
		GUI.Label(new Rect(100,100,100,100),"" + debg,style);
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void showAd()
	{
		if (shownAd == false) {
			Chartboost.showInterstitial(CBLocation.Default);
		}
		else if (shownAd == true) {
			gameManager.startGameGOS();
		}
	}

	void cacheAds()
	{
		if (!Chartboost.hasInterstitial(CBLocation.Default)) {
			//Chartboost.showInterstitial(CBLocation.Default);
			failedToLoadInterstitial = false;
			Chartboost.cacheInterstitial(CBLocation.Default);

		}
		if (!Chartboost.hasMoreApps(CBLocation.GameOver)) {
			failedToLoadMoreApps = false;
			Chartboost.cacheMoreApps(CBLocation.GameOver);
		}
	}
		

	void OnEnable() 
	{
		Chartboost.didDisplayInterstitial += didDisplayInterstitial;
		Chartboost.didCacheMoreApps += didCacheMoreApps;
		Chartboost.didCacheInterstitial += didCacheInterstitial;
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
		Chartboost.didDisplayInterstitial -= didDisplayInterstitial;
		Chartboost.didCacheMoreApps -= didCacheMoreApps;
		Chartboost.didCacheInterstitial -= didCacheInterstitial;
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
		shownAd = false;

	}

	void didDisplayInterstitial(CBLocation location) {
		shownChartboost = true;
		shownAd = false;
	}

	void didCacheInterstitial(CBLocation location) {
		failedToLoadInterstitial = false;
		shownAd = false;
	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) {
		AddLog(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
		debg = "failed chartboost";
		failedToLoadInterstitial = true;
		shownChartboost = false;
		shownAd = true;
		showAd();
		//closeScene = true;
	}

	void didDismissInterstitial(CBLocation location) {
		AddLog("didDismissInterstitial: " + location);
		debg = "dismiss chartboost";
		closeScene = true;
		shownChartboost = true; //not sure
		shownAd = true;
		showAd();
	}

	void didCloseInterstitial(CBLocation location) {
		AddLog("didCloseInterstitial: " + location);
		debg = "closed chartboost";
		closeScene = true;
		shownChartboost = true;
		shownAd = true;
		showAd();
	}

	void didClickMoreApps(CBLocation location) {
		//save data
		shownAd = true;
		gameManager.save();
	}

	void didClickInterstitial(CBLocation location) {
		//save data
		shownAd = true;
		showAd();
		gameManager.save();
	}

	// Called after a MoreApps page has been dismissed.
	void didDismissMoreApps(CBLocation location) {
		closeScene = true;
		shownChartboost = false; //not sure
		shownAd = true;
		showAd();
	}

	// Called after a MoreApps page has been closed.
	void didCloseMoreApps(CBLocation location) {
		closeScene = true;
		shownChartboost = true;
		shownAd = true;
		showAd();
	}
	// Called after a MoreApps page attempted to load from the Chartboost API
	// servers but failed.
	void didFailToLoadMoreApps(CBLocation location, CBImpressionError error) {
		failedToLoadMoreApps = true;
		//shownChartboost = true;
	}

	void didCacheMoreApps(CBLocation location) {
		failedToLoadMoreApps = false;
	}

	void AddLog(string text)
	{
		Debug.Log(text);

	}
}

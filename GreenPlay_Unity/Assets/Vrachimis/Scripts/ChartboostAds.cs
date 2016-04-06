using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class ChartboostAds : MonoBehaviour {


	public ResizeBackground resizeBackground;

	private Vector3 targetX;
	private bool closeScene = false;
	private bool isOnce = true;

	// Use this for initialization
	void Start () {
		Debug.Log("Started Ads");

		GameObject resizeBackgroundObject = GameObject.Find("Background");
		if (resizeBackgroundObject != null)
		{
			resizeBackground = resizeBackgroundObject.GetComponent<ResizeBackground>();
		}
		if (resizeBackground == null)
		{
			Debug.Log ("Cannot find 'ResizeBackground' script");
		}

		InvokeRepeating("cacheAds",2,15);
	}
	
	// Update is called once per frame
	void Update () {

		targetX = new Vector3(2 * resizeBackground.worldScreenWidth,0,-10);

		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.02f && isOnce) {
			closeScene = true;
			isOnce = false;
		} else if (transform.position.x < 5f) {
			isOnce = true;
		}

		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.02f && closeScene == true) {
			closeScene = false;
			Chartboost.showInterstitial(CBLocation.Default);
			Debug.Log ("-------------------------Show Chartboost Interstitial.");
		} else
		{
			closeScene = false;
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
		Debug.Log("INITIALIZE CHARTBOOST");

	}

	void didDisplayInterstitial(CBLocation location) {
		closeScene = false;
	}

	void didCacheInterstitial(CBLocation location) {
		Debug.Log("CACHED INTER");
	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) {
		AddLog(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
		closeScene = false;
	}

	void didDismissInterstitial(CBLocation location) {
		AddLog("didDismissInterstitial: " + location);
		closeScene = false;
	}

	void didCloseInterstitial(CBLocation location) {
		AddLog("didCloseInterstitial: " + location);
		closeScene = false;
	}

	void didClickMoreApps(CBLocation location) {
	}

	void didClickInterstitial(CBLocation location) {
		//save here
	}

	// Called after a MoreApps page has been dismissed.
	void didDismissMoreApps(CBLocation location) {
	}

	// Called after a MoreApps page has been closed.
	void didCloseMoreApps(CBLocation location) {
	}
	// Called after a MoreApps page attempted to load from the Chartboost API
	// servers but failed.
	void didFailToLoadMoreApps(CBLocation location, CBImpressionError error) {
	}

	void didCacheMoreApps(CBLocation location) {
	}

	void AddLog(string text)
	{
		Debug.Log(text);

	}
}

using UnityEngine;
using System.Collections;

public class ApplovinLoader : MonoBehaviour {

	public static AppLovin myApplovin = null;
	public string SDKKey = "fyTagFDisKNQOwVukdLCv5_iCinuTdf8aDiFTxzKFKleEFWztt9nz9T9sE1KthSRwAhN5ehLzR_CgW9XNIIKSm";

	void Start () {

		if (SDKKey == "") {

			Debug.LogWarning ("Please input AppLovin SDK key to the ApplovinLoader gameobject.");

		} else {

			myApplovin = AppLovin.getDefaultPlugin ();
			myApplovin.setSdkKey (SDKKey);
			myApplovin.initializeSdk ();
			myApplovin.preloadInterstitial ();
		}
	}
}
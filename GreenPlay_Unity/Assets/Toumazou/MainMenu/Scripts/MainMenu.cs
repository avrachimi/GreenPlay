/// <summary>
/// Main menu.
/// Attached to main camera->eni3ero ti simeni
/// </summary>

using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class MainMenu : MonoBehaviour {
	//variables

	public float playX;
	public float playY;
	public float shopX;
	public float shopY;
	public float noadsX;
	public float noadsY;

	public GameObject play_button;
	public GameObject ads_button;
	public GameObject shop_button;
	public GameObject playTest;
	public GameObject shopTest;
	public GameObject noadsTest;
	public Texture background;

	// Use this for initialization
	void Start () {
		Instantiate (play_button, new Vector3 (-0.09f,0.59f, 0), Quaternion.identity);
		//Instantiate (ads_button, new Vector3 (-2.17f/500*Screen.width, -0.7f/500*Screen.height, 0), Quaternion.identity);
		Instantiate (shop_button, new Vector3 (2.46f/500*Screen.width, -0.59f/500*Screen.height, 0), Quaternion.identity);

		AppLovin.InitializeSdk(); //men to pira3is tuto
		//Chartboost.cacheInterstitial();
		int rand = Random.Range(1,6);
		if (rand==1) Chartboost.showInterstitial(CBLocation.Default);
		//AppLovin.ShowInterstitial();
		/*playTest = GameObject.Find("play_button(Clone)");
		shopTest = GameObject.Find ("shop_button(Clone)");
		noadsTest = GameObject.Find ("noads_button(Clone)");*/
	}

	// Update is called once per frame
	void Update () {
		/*playTest.transform.position = new Vector3 (playX/500*Screen.width, playY/500*Screen.height, 0);
		shopTest.transform.position = new Vector3 (shopX/500*Screen.width, shopY/500*Screen.height, 0);
		noadsTest.transform.position = new Vector3 (noadsX/500*Screen.width, noadsY/500*Screen.height, 0);*/
	}
}

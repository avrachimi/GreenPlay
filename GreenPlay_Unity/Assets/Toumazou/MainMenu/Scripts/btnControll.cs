using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using OnePF;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Facebook.Unity;



public class btnControll : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer spriteRenderer;
	public Sprite btnPlayPressed;
	public Sprite btnShopPressed;
	public Sprite btnAdsPressed;
	public Texture btnplay;
	public Texture btnShop;
	public Texture btnAds;
	public Texture btnLeaderboard;
	public Texture googlePlayGames;
	public Texture btnInfo;
	public Texture infoScreen;
	public GUIStyle btnAdsStyle;
	public GUIStyle btnLeaderboardStyle;
	public GUIStyle btnPlayStyle;
	public GUIStyle btnShopStyle;
	public GUIStyle googlePlayGamesStyle;
	public GUIStyle btnInfoStyle;
	public GUIStyle infoStyle;
	public GUIStyle btnDonateStyle;

	public OpenIABEventManager openIABEventManager;

	public float playX;
	public float playY;
	public float shopX;
	public float shopY;
	public float noadsX;
	public float noadsY;

	public Vector2 btnAdsPos;
	public float btnAdsSize;
	public Vector2 btnPlayPos;
	public float btnPlaySize;
	public Vector2 btnShopPos;
	public float btnShopSize;
	public Vector2 googlePlayGamesPos;
	public float googlePlayGamesSize;
	public Vector2 btnInfoPos;
	public float btnInfoSize;
	public Vector2 infoScreenPos;
	public Vector2 infoScreenSize;
	public Vector2 btnDonateSize;
	public Vector2 btnDonatePos;

	//TA PUKATO VARIABLES EKAMATA EGO GIA TO STORE SCREEN; VRACHIMIS
	private bool inShop = false;
	private bool showInfo = false;

	public GUIStyle buyCoinsStyle;
	public GUIStyle buyMoneyStyle;
	public GUIStyle shopStyle;
	public Vector2 buyCoinsPos;
	public Vector2 buyMoneyPos;
	public Vector2 shopPos;
	public Vector2 buyCoinsSize;
	public Vector2 buyMoneySize;
	public Vector2 shopSize;

	//OpenIAB
	const string SKU = "sku";

	string _label = "";
	bool _isInitialized = false;
	Inventory _inventory = null;

	private bool hasNoAds = false;


	private void OnEnable()
	{
		// Listen to all events for illustration purposes
		OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}
	private void OnDisable()
	{
		// Remove all event handlers
		OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	void Start () {



		// Application public key
		var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAh11RsO7eMO39cIUOoagxcaYmymF8i7bOSZ6N29fr0XpAoT3dLSlKmgwGnxkFfskcX7NowO1lMZRIw1GMPCdGNYPj0W/cZneRuCdSsiFJJ18wiRI6FeYrTXe9LyCXmnkhGfAEuKxoXCN5HdZDFi4/RugQTyqsn0RTDoSZysofdRo9CwC0d8ei4vS77Ys3aeLPpwGoJXCRYhGk8VGzdl5KkwLcs0mINAz4YO7/SwS6mxXEhpL2/uBWLCXEyO9XAIMoWCxz4eXpJoukljykQERPq07Ba090AVp0iYd7n5Q16yplyCRdE+ZYigb3dBymDHOanu63tuRQ1cwQUVNJM3AEOwIDAQAB";
		//var yandexPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApvU8l4ONEEsSGznPN6DnjIbJnv6vEgm08nbbi+2fMc0V46N7x7jBWTWAf2K6XLZg/rLUkqbWISq12PLvt7ydcsD+Hb9ZubdN2h8LNCTohVPeDbJjd5khtF4J5FNP2/XSTc1C7cSCBTGmqH0fUr77v4x/JMpxKlSjPN6KbNnaF2BLDAdi3012lz2XX4BVgUj7LArID/vYSYGlwMzMkvhUSpvZOM/WIPN+8YDgQAFBlRGRjLhY/3Vpq/AtXtVAzzyfTOZYkwNqdXpwAq5+/51LphowUI5NEBYh8lhQeOJmPNA6EcF1h5L9cJTVLy3bkuCXcjoN2eEO1Nq0h/40G0R4pwIDAQAB";
		//var slideMePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAogOQb0mMbuq4FQ4ZhWRhN8k76/gXOUE370VubZa9Up25GdptYXoRNniecUTDLyjfvWp7+YFW8iPqIp523qNXtQ0EynNhK4xNLvJCd1CjfAju6M0f+o8MOL1zV7g3dHqxICZoHwqBbQOWneDzG/DzJ22AVdLKwty0qbv8ESaCOCJe31ZnoYVMw5KNVkSuRrrhiGGh6xj7F3qZ0T5TOSp3fK7soDamQLevuU7Ndn5IQACjo92HNN0O2PR2cvEjkCRuIkNk2hnqinac984JCzCC0SC/JBnUZUAeYJ7Y8sjT+79z1T1g7yGgDesopnqORiBkeXEZHrFy7PifdA/ZX7rRwQIDAQAB";

		//PlayerPrefs.SetInt("HighScore", 0);



		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// require access to a player's Google+ social graph to sign in
			.RequireGooglePlus()
			.Build();

		PlayGamesPlatform.InitializeInstance(config);

		PlayGamesPlatform.Activate();

		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
		});

		var options = new Options();
		options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
		options.checkInventory = false;
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
		options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, googlePublicKey} };
		//options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_YANDEX, yandexPublicKey } };
		//options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_SLIDEME, slideMePublicKey } };
		options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;

		// Transmit options and start the service
		OpenIAB.init(options);

		//OpenIAB.queryInventory();
		spriteRenderer = GetComponent<SpriteRenderer>();

		//Invoke("checkInventory", 2);
		checkConnection();
	}

	void OnGUI()
	{
		if (!inShop) {
			if (!hasNoAds) {
				if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * btnAdsPos.x ), Screen.height/2 - (Screen.height * btnAdsPos.y), Screen.width - (Screen.width * btnAdsSize) , Screen.width - (Screen.width * btnAdsSize)), "", btnAdsStyle)) {

					OpenIAB.purchaseProduct("no_ads");

					//OpenIAB.restoreTransactions();
					//checkInventory();

				}
			}
			else if (hasNoAds) {
				if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * btnAdsPos.x ), Screen.height/2 - (Screen.height * btnAdsPos.y), Screen.width - (Screen.width * btnAdsSize) , Screen.width - (Screen.width * btnAdsSize)), "", btnLeaderboardStyle)) {
					//display leaderboard
					Social.ShowLeaderboardUI();
					//OpenIAB.purchaseProduct("no_ads");
				}
			}
				
			if (GUI.Button (new Rect (Screen.width / 2 - (Screen.width * btnPlayPos.x), Screen.height / 2 - (Screen.height * btnPlayPos.y), Screen.width - (Screen.width * btnPlaySize), Screen.width - (Screen.width * btnPlaySize)), "", btnPlayStyle)) {
				if (hasNoAds) {
					SceneManager.LoadScene(2);
				}
				else if (!hasNoAds) {
					SceneManager.LoadScene(1);
				}
			}
			if (GUI.Button (new Rect (Screen.width / 2 - (Screen.width * btnShopPos.x), Screen.height / 2 - (Screen.height * btnShopPos.y), Screen.width - (Screen.width * btnShopSize), Screen.width - (Screen.width * btnShopSize)), "", btnShopStyle)) {
				//inShop = true;
				Social.ShowAchievementsUI();
			}
		}

		//VRACHIMIS
		if (inShop) {
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * shopPos.x ), Screen.height/2 - (Screen.height * shopPos.y),0,0),"",shopStyle)) {
				//do nothing. just to display the graphic
			}
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * buyCoinsPos.x ), Screen.height/2 - (Screen.height * buyCoinsPos.y), Screen.width - (Screen.width * buyCoinsSize.x) , Screen.height - (Screen.height * buyCoinsSize.y)),"",buyCoinsStyle)) {
				//pausePressed = false;
				inShop = false;
			}
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * buyMoneyPos.x ), Screen.height/2 - (Screen.height * buyMoneyPos.y), Screen.width - (Screen.width * buyMoneySize.x) , Screen.height - (Screen.height * buyMoneySize.y)),"",buyMoneyStyle)) {
				inShop = false;
			}
		}

		if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * googlePlayGamesPos.x ), Screen.height/2 - (Screen.height * googlePlayGamesPos.y), Screen.width - (Screen.width * googlePlayGamesSize) , Screen.width - (Screen.width * googlePlayGamesSize)), googlePlayGames, googlePlayGamesStyle)) {

			// sign out
			PlayGamesPlatform.Instance.SignOut();

			// authenticate user:
			Social.localUser.Authenticate((bool success) => {
				// handle success or failure
			});

			//checkInventory();
		}
		if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * btnInfoPos.x ), Screen.height/2 - (Screen.height * btnInfoPos.y), Screen.width - (Screen.width * btnInfoSize) , Screen.width - (Screen.width * btnInfoSize)), "", btnInfoStyle)) {
			//info
			//toggleInfoScreen();
			showInfo = true;
		}

		if (GUI.Button (new Rect (Screen.width / 2 - (Screen.width * btnDonatePos.x), Screen.height / 2 - (Screen.height * btnDonatePos.y), Screen.width - (Screen.width * btnDonateSize.x), Screen.width - (Screen.width * btnDonateSize.y)), "", btnDonateStyle)) {
			Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=GTS8QNN6XQMQJ");
		}

		if (showInfo) {
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * infoScreenPos.x ), Screen.height/2 - (Screen.height * infoScreenPos.y), Screen.width - (Screen.width * infoScreenSize.x) , Screen.height - (Screen.height * infoScreenSize.y)), "", infoStyle)) {
				//info
				showInfo = false;
			}
		}
	}

	void toggleInfoScreen()
	{
		if (showInfo) showInfo = false;
		else showInfo = true;
	}

	void checkInventory()
	{
		OpenIAB.queryInventory();
		if (_inventory != null && _inventory.HasPurchase("no_ads")) {
			PlayerPrefs.SetInt("NoAds", 1);
			hasNoAds = true;
		}
		else {
			PlayerPrefs.SetInt("NoAds", 0);
			hasNoAds = false;
		}
	}
	// Update is called once per frame
	void Update () {

		//VRACHIMIS
		buyCoinsStyle.fixedWidth = Screen.width - (Screen.width * buyCoinsSize.x);
		buyCoinsStyle.fixedHeight = Screen.height - (Screen.height * buyCoinsSize.y);
		buyMoneyStyle.fixedWidth = Screen.width - (Screen.width * buyMoneySize.x);
		buyMoneyStyle.fixedHeight = Screen.height - (Screen.height * buyMoneySize.y);
		shopStyle.fixedWidth = Screen.width - (Screen.width * shopSize.x);
		shopStyle.fixedHeight = Screen.height - (Screen.height * shopSize.y);


		 //(Screen.width, Screen.height);
		btnAdsStyle.fixedWidth = Screen.width - (Screen.width * btnAdsSize);
		btnLeaderboardStyle.fixedWidth = Screen.width - (Screen.width * btnAdsSize);
		btnPlayStyle.fixedWidth = Screen.width - (Screen.width * btnPlaySize);
		btnShopStyle.fixedWidth = Screen.width - (Screen.width * btnShopSize);
		googlePlayGamesStyle.fixedWidth = Screen.width - (Screen.width * googlePlayGamesSize);
		googlePlayGamesStyle.fixedHeight = Screen.height - (Screen.height * googlePlayGamesSize);
		infoStyle.fixedWidth = Screen.width - (Screen.width * infoScreenSize.x);
		infoStyle.fixedHeight = Screen.height - (Screen.height * infoScreenSize.y);
		btnDonateStyle.fixedWidth = Screen.width - (Screen.width * btnDonateSize.x);
		btnDonateStyle.fixedHeight = Screen.height - (Screen.height * btnDonateSize.y);
		//infoStyle.fixedHeight = Screen.height - (Screen.height * infoScreenSize);

		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			//LATER: make sure to check if hitting pause or mute button
			if (hit.collider.name == "background") {
				inShop = false;
				showInfo = false;
			}


		}

		/*
		if (gameObject.name == "play_button(Clone)") {
			spriteRenderer.sprite = btnplay;
			transform.localScale = new Vector3 (0.6f,0.6f, 0);
		}
		else if (gameObject.name == "shop_button(Clone)") {
			spriteRenderer.sprite = btnShop;
			transform.localScale = new Vector3 (0.4f, 0.4f, 0);
		}
		else if (gameObject.name == "noads_button(Clone)") {
			spriteRenderer.sprite = btnAds;
			transform.localScale = new Vector3 (0.4f, 0.4f, 0);
		}*/
	}

	public void didPurchase(bool success)
	{
		if (success) {
			PlayerPrefs.SetInt("NoAds", 1);
		}
		else {
			PlayerPrefs.SetInt("NoAds", 0);
		}
	}



	private void billingSupportedEvent()
	{
		_isInitialized = true;
		Debug.Log("billingSupportedEvent");
		checkInventory();
	}
	private void billingNotSupportedEvent(string error)
	{
		Debug.Log("billingNotSupportedEvent: " + error);
	}
	private void queryInventorySucceededEvent(Inventory inventory)
	{
		Debug.Log("queryInventorySucceededEvent: " + inventory);
		if (inventory != null)
		{
			_label = inventory.ToString();
			_inventory = inventory;
		}
		if (inventory != null && inventory.GetPurchase("no_ads") != null) {
			hasNoAds = true;
			didPurchase(true);
		}
	}
	private void queryInventoryFailedEvent(string error)
	{
		Debug.Log("queryInventoryFailedEvent: " + error);
		_label = error;
	}
	private void purchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("purchaseSucceededEvent: " + purchase);
		_label = "PURCHASED:" + purchase.ToString();
		hasNoAds = true;
		didPurchase(true);
	}
	private void purchaseFailedEvent(int errorCode, string errorMessage)
	{
		Debug.Log("purchaseFailedEvent: " + errorMessage);
		_label = "Purchase Failed: " + errorMessage;
		hasNoAds = false;
		didPurchase(false);
	}
	private void consumePurchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		_label = "CONSUMED: " + purchase.ToString();
	}
	private void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
		_label = "Consume Failed: " + error;
	}

	public string GetHtmlFromUri(string resource)
	{
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
			{
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess)
				{
					using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
					{
						//We are limiting the array to 80 so we don't have
						//to parse the entire html document feel free to 
						//adjust (probably stay under 300)
						char[] cs = new char[80];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs)
						{
							html +=ch;
						}
					}
				}
			}
		}
		catch
		{
			return "";
		}
		return html;
	}

	void checkConnection()
	{
		string HtmlText = GetHtmlFromUri("http://google.com");
		int temp = PlayerPrefs.GetInt("NoAds");
		if(HtmlText == "")
		{
			//No connection
			if (temp == 1)
			{
				hasNoAds = true;
			}
			else 
			{
				hasNoAds = false;
			}
		}
		else if(!HtmlText.Contains("schema.org/WebPage"))
		{
			//Redirecting since the beginning of googles html contains that 
			//phrase and it was not found
			if (temp == 1)
			{
				hasNoAds = true;
			}
			else 
			{
				hasNoAds = false;
			}
		}
		else
		{
			//success
			//do nothing since there's internet connection I can query the purchases
		}
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

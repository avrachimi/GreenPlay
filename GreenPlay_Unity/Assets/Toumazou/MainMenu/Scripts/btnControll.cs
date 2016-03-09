﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnControll : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer spriteRenderer;
	public Sprite btnPlayPressed;
	public Sprite btnShopPressed;
	public Sprite btnAdsPressed;
	public Texture btnplay;
	public Texture btnShop;
	public Texture btnAds;
	public GUIStyle btnAdsStyle;
	public GUIStyle btnPlayStyle;
	public GUIStyle btnShopStyle;


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

	//TA PUKATO VARIABLES EKAMATA EGO GIA TO STORE SCREEN; VRACHIMIS
	private bool inShop = false;

	public GUIStyle buyCoinsStyle;
	public GUIStyle buyMoneyStyle;
	public GUIStyle shopStyle;
	public Vector2 buyCoinsPos;
	public Vector2 buyMoneyPos;
	public Vector2 shopPos;
	public Vector2 buyCoinsSize;
	public Vector2 buyMoneySize;
	public Vector2 shopSize;



	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnGUI()
	{
		if (!inShop) {
			if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * btnAdsPos.x ), Screen.height/2 - (Screen.height * btnAdsPos.y), Screen.width - (Screen.width * btnAdsSize) , Screen.width - (Screen.width * btnAdsSize)), btnAds, btnAdsStyle)) {
				//Application.OpenURL("https://play.google.com/store/apps/details?id=com.lego.nexoknights.merlok&hl=en");
				/*int x = 237;
				PlayerPrefs.SetInt("HighScore", x);
				PlayerPrefs.Save();*/
			}
				
			if (GUI.Button (new Rect (Screen.width / 2 - (Screen.width * btnPlayPos.x), Screen.height / 2 - (Screen.height * btnPlayPos.y), Screen.width - (Screen.width * btnPlaySize), Screen.width - (Screen.width * btnPlaySize)), btnplay, btnPlayStyle)) {
				SceneManager.LoadScene(1);
			}
			if (GUI.Button (new Rect (Screen.width / 2 - (Screen.width * btnShopPos.x), Screen.height / 2 - (Screen.height * btnShopPos.y), Screen.width - (Screen.width * btnShopSize), Screen.width - (Screen.width * btnShopSize)), btnShop, btnShopStyle)) {
				inShop = true;
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
		btnPlayStyle.fixedWidth = Screen.width - (Screen.width * btnPlaySize);
		btnShopStyle.fixedWidth = Screen.width - (Screen.width * btnShopSize);

		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			//LATER: make sure to check if hitting pause or mute button
			if (hit.collider.name == "background") {
				inShop = false;
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
}

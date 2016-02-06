﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class btnControll : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer spriteRenderer;
	public Sprite btnPlayPressed;
	public Sprite btnShopPressed;
	public Sprite btnAdsPressed;
	public Sprite btnplay;
	public Sprite btnShop;
	public Texture btnAds;
	public GUIStyle btnAdsStyle;

	public float playX;
	public float playY;
	public float shopX;
	public float shopY;
	public float noadsX;
	public float noadsY;

	public Vector2 btnAdsPos;
	public float btnAdsSize;



	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();

		//spriteRenderer.sprite = btnShop;
		//spriteRenderer.sprite = btnAds;
		//transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		if (gameObject.name == "play_button(Clone)") {
			spriteRenderer.sprite = btnplay;
			transform.localScale = new Vector3 (0.6f,0.6f, 0);
			}
		else if (gameObject.name == "shop_button(Clone)") {
			spriteRenderer.sprite = btnShop;
			transform.localScale = new Vector3 (0.4f,0.4f, 0);
		}
		/*else if (gameObject.name == "noads_button(Clone)") {
			spriteRenderer.sprite = btnAds;
			transform.localScale = new Vector3 (0.4f, 0.4f, 0);
		}*/

	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width/2 - (Screen.width * btnAdsPos.x ), Screen.height/2 - (Screen.height * btnAdsPos.y), 100 , 200), btnAds, btnAdsStyle))
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.lego.nexoknights.merlok&hl=en");
	}
	
	// Update is called once per frame
	void Update () {
		 //(Screen.width, Screen.height);
		btnAdsStyle.fixedWidth = Screen.width - (Screen.width * btnAdsSize);

		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			//LATER: make sure to check if hitting pause or mute button
			if (hit.collider.name == "play_button(Clone)") {
				Debug.Log ("PLay");
				//spriteRenderer.sprite = btnPlayPressed;
				hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = btnPlayPressed;
				transform.localScale = new Vector3 (0.6f,0.6f, 0);
				SceneManager.LoadScene(1);
			}
			/*else if (hit.collider.name == "noads_button(Clone)") {
				Debug.Log ("ADS");
				hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = btnAdsPressed;
				transform.localScale = new Vector3 (0.4f, 0.4f, 0);
			}*/
			else if (hit.collider.name == "shop_button(Clone)") {
				Debug.Log ("Shop");
				hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = btnShopPressed;
				transform.localScale = new Vector3 (0.4f, 0.4f, 0);
			}


		}



		if (gameObject.name == "play_button(Clone)") {
			spriteRenderer.sprite = btnplay;
			transform.localScale = new Vector3 (0.6f,0.6f, 0);
		}
		else if (gameObject.name == "shop_button(Clone)") {
			spriteRenderer.sprite = btnShop;
			transform.localScale = new Vector3 (0.4f, 0.4f, 0);
		}
		/*else if (gameObject.name == "noads_button(Clone)") {
			spriteRenderer.sprite = btnAds;
			transform.localScale = new Vector3 (0.4f, 0.4f, 0);
		}*/
	}
}

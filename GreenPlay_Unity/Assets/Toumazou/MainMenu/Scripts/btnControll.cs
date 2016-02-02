using UnityEngine;
using System.Collections;

public class btnControll : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer spriteRenderer;
	public Sprite btnPlayPressed;
	public Sprite btnShopPressed;
	public Sprite btnAdsPressed;
	public Sprite btnplay;
	public Sprite btnShop;
	public Sprite btnAds;

	public float playX;
	public float playY;
	public float shopX;
	public float shopY;
	public float noadsX;
	public float noadsY;





	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();

		//spriteRenderer.sprite = btnShop;
		//spriteRenderer.sprite = btnAds;
		//transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		if (gameObject.name == "play_button(Clone)") {
			spriteRenderer.sprite = btnplay;
			transform.localScale = new Vector3 (1.01f/500*Screen.width,0.57f/500*Screen.height, 0);
			}
		else if (gameObject.name == "shop_button(Clone)") {
			spriteRenderer.sprite = btnShop;
			transform.localScale = new Vector3 (0.5f/500*Screen.width, 0.5f/500*Screen.height, 0);
		}
		else if (gameObject.name == "noads_button(Clone)") {
			spriteRenderer.sprite = btnAds;
			transform.localScale = new Vector3 (0.5f/500*Screen.width, 0.5f/500*Screen.height, 0);
		}

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			//LATER: make sure to check if hitting pause or mute button
			if (hit.collider.name == "play_button(Clone)") {
				Debug.Log ("kotsiod");
				spriteRenderer.sprite = btnPlayPressed;
				transform.localScale = new Vector3 (1.01f*Screen.width, 0.57f/500*Screen.height, 0);
				}
			else if (hit.collider.name == "shop_button(Clone)") {
				Debug.Log ("kotsiod");
				spriteRenderer.sprite = btnShopPressed;
				transform.localScale = new Vector3 (0.5f/500*Screen.width, 0.5f/500*Screen.height, 0);
			}
			else if (hit.collider.name == "noads_button(Clone)") {
				Debug.Log ("kotsiod");
				spriteRenderer.sprite = btnAdsPressed;
				transform.localScale = new Vector3 (0.5f/500*Screen.width, 0.5f/500*Screen.height, 0);
			}

		}



		if (gameObject.name == "play_button(Clone)") {
			spriteRenderer.sprite = btnplay;
			transform.localScale = new Vector3 (Screen.width*playX/500, playY* Screen.height /500, 0);
		}
		else if (gameObject.name == "shop_button(Clone)") {
			spriteRenderer.sprite = btnShop;
			transform.localScale = new Vector3 (shopX/500*Screen.width, shopY/500*Screen.height, 0);
		}
		else if (gameObject.name == "noads_button(Clone)") {
			spriteRenderer.sprite = btnAds;
			transform.localScale = new Vector3 (noadsX/500*Screen.width, noadsY/500*Screen.height, 0);
		}
	}
}

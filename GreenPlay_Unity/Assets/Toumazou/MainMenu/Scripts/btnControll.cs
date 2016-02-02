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





	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();

		//spriteRenderer.sprite = btnShop;
		//spriteRenderer.sprite = btnAds;
		//transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		if (gameObject.name == "play_button(Clone)") {
			spriteRenderer.sprite = btnplay;
			transform.localScale = new Vector3 (0.5f, 0.5f, 0);
			}
		else if (gameObject.name == "shop_button(Clone)") {
			spriteRenderer.sprite = btnShop;
			transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		}
		else if (gameObject.name == "noads_button(Clone)") {
			spriteRenderer.sprite = btnAds;
			transform.localScale = new Vector3 (0.5f, 0.5f, 0);
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
				spriteRenderer.sprite = btnPlayPressed;
				transform.localScale = new Vector3 (0.5f, 0.5f, 0);
				}
			else if (hit.collider.name == "shop_button(Clone)") {
				spriteRenderer.sprite = btnShopPressed;
				transform.localScale = new Vector3 (0.5f, 0.5f, 0);
			}
			else if (hit.collider.name == "noads_button(Clone)") {
				spriteRenderer.sprite = btnAdsPressed;
				transform.localScale = new Vector3 (0.5f, 0.5f, 0);
			}
		}
	}
}

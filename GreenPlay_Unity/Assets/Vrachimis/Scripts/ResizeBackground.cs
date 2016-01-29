using UnityEngine;
using System.Collections;

public class ResizeBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ResizeSpriteToScreen();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ResizeSpriteToScreen() {
		Sprite sr = GetComponent<SpriteRenderer>().sprite;
		//if (sr == null) return;

		transform.localScale = new Vector3(1,1,1);

		float width = sr.bounds.size.x;
		float height = sr.bounds.size.y;

		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		Vector3 localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 0);
		transform.localScale = localScale;
	}
}

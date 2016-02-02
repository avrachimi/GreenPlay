using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

	public int location = 1;

	private Vector3 localScale;
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
		localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 0);
		transform.localScale = localScale;

		if (location == 1) {
			transform.position = new Vector3(0,0,0);
		}
		else if (location == 2) {
			transform.position = new Vector3(worldScreenWidth,0,0);
		}
		else if (location == 3) {
			transform.position = new Vector3(2 * (worldScreenWidth),0,0);
		}
	}
}

/// <summary>
/// Main menu.
/// Attached to main camera->eni3ero ti simeni
/// </summary>

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	//variables
	public Texture background;
	public GUIStyle PlayButtonTexture;//image of play button
	public GUIStyle OptionsButtonTexture;//image of options button
	public GUIStyle LeaderboardButtonTexture;//image of leaderboard button
	public GUIStyle ShopButtonTexture;//image of Shop button
	public float PlayButtonPlacementY; // y position of the button play
	public float OptionsButtonPlacementY; // y position of the button options

	public float PlayButtonPlacementX; // x position of the button play
	public float OptionsButtonPlacementX; // x position of the button options

	public float LeaderboardButtonPlacementY; // y position of the button Leaderboard
	public float LeaderboardButtonPlacementX; // x position of the button Leaderboard

	public float ShopButtonPlacementY; // y position of the button Shop
	public float ShopButtonPlacementX; // x position of the button Shop

	void OnGUI(){
	//Display our background texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), background); 

	//Display our buttons with textures
		if (GUI.Button (new Rect(Screen.width * PlayButtonPlacementX, Screen.height * PlayButtonPlacementY,Screen.width * .6f, Screen.height * .2f) ,"", PlayButtonTexture)){
			print ("Clicked");
		}

		if (GUI.Button (new Rect(Screen.width * OptionsButtonPlacementX, Screen.height * OptionsButtonPlacementY,Screen.width * .1f, Screen.height * .06f) ,"", OptionsButtonTexture)){
			print ("Clicked");
		}

		if (GUI.Button (new Rect(Screen.width * LeaderboardButtonPlacementX, Screen.height * LeaderboardButtonPlacementY,Screen.width * .24f, Screen.height * .10125f) ,"", LeaderboardButtonTexture)){
			print ("Clicked");
		}

		if (GUI.Button (new Rect(Screen.width * ShopButtonPlacementX, Screen.height * ShopButtonPlacementY,Screen.width * .24f, Screen.height * .10125f) ,"", ShopButtonTexture)){
			print ("Clicked");
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

// this script is attached to the emtpy gameObject "GameObject" in the menu2 scene
// it realizes the menu behaviour

// menu scene loads lots (and loads) of game elements. 
// So please be patient if you own a slow machine

public class MenuScript : MonoBehaviour {

	// Reference to the blox-Font
	public Font myFont;

	// I tried to make the GUI resolution independent
	// so, here the native resolution is set to 1024x768
	// OnGUI resizes the GUI-elements
	// These values have to be float,
	// else division in method OnGUI will return null (rx, ry)
	private float nativeWidth = 1024;
	private float nativeHeight = 768;

	// Reference to the rectangles which can be clicked on the screen
	private Rect play;
	private Rect exit;

	// Use this for initialization
	void Start () {
		// initializes the menu entries
		play = new Rect(255,340,222,50);
		exit = new Rect(315,125,215,50);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {
		// care for resolution independence
		float rx = Screen.width / nativeWidth;
		float ry = Screen.height / nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0,0,0), Quaternion.identity, new Vector3 (rx, ry, 1));

		// set the menu entries color
		GUI.contentColor = new Color(1,1,1,1);
		// build a label for the game title
		GUIStyle myTitleStyle = new GUIStyle(GUI.skin.label);
		myTitleStyle.fontSize = 92;
		myTitleStyle.font = myFont;
		GUI.Label(new Rect(280,100,600,100), "Tetra  Pack", myTitleStyle);
		GUIStyle myTitle2Style = new GUIStyle(GUI.skin.label);
		myTitle2Style.fontSize = 48;
		myTitle2Style.font = myFont;
		GUI.Label(new Rect(320,200,600,100), "The Legend Returns", myTitle2Style);

		// build a label for the menu entries
		GUI.contentColor = new Color(1,1,1,1);
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.label);
		myButtonStyle.fontSize = 48;
		myButtonStyle.font = myFont;
		// check for click on entry "play game"
		// if clicked, load the scene "game2"
		if (GUI.Button(new Rect(280,380,600,100), "Play Game", myButtonStyle)){
			Application.LoadLevel("game2");
		}
		
		// check for click on entry "exit game"
		// if clicked, quit the application
		if (GUI.Button(new Rect(340,500,600,100), "Exit Game", myButtonStyle)){
			Application.Quit();
		}

	}
}

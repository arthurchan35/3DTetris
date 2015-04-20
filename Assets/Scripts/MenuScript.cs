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
	private float nativeWidth = 1280;
	private float nativeHeight = 720;

	public int difficulty;
	// Reference to the rectangles which can be clicked on the screen
	private Rect play;
	private Rect exit;

	// Use this for initialization
	void Start () {
		// initializes the menu entries
		difficulty = -1;
		Debug.Log (difficulty);
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
		GUI.contentColor = new Color(0,0,0,1);
		// build a label for the game title
		GUIStyle myTitleStyle = new GUIStyle(GUI.skin.label);
		myTitleStyle.fontSize = 108;
		myTitleStyle.font = myFont;
		GUI.Label(new Rect(280,100,600,100), "3D TETRIS", myTitleStyle);


		// build a label for the menu entries
		GUI.contentColor = new Color(0,0,0.5f,1);
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.label);
		myButtonStyle.fontSize = 72;
		myButtonStyle.font = myFont;
		// check for click on entry "play game"
		// if clicked, load the scene "game2"
		if (GUI.Button(new Rect(220,260,600,100), "Play Game", myButtonStyle)){
			Application.LoadLevel("game2");
		}

		if (GUI.Button(new Rect(1000,260,600,100), "easy", myButtonStyle)){
			difficulty = 1;
			Debug.Log ("hit easy, " + difficulty);
		}

		if (GUI.Button(new Rect(1000,380,600,100), "normal", myButtonStyle)){
			difficulty = 2;
			Debug.Log ("hit normal, " + difficulty);
		}

		if (GUI.Button(new Rect(1000,500,600,100), "hard", myButtonStyle)){
			difficulty = 3;
			Debug.Log ("hit hard, " + difficulty);
		}

		if (GUI.Button(new Rect(280,380,600,100), "show high scores", myButtonStyle)){
			Application.LoadLevel("highscore");
		}
		// check for click on entry "exit game"
		// if clicked, quit the application
		if (GUI.Button(new Rect(340,500,600,100), "Exit Game", myButtonStyle)){
			Application.Quit();
		}

	}


	void Awake() {
		DontDestroyOnLoad(this);
	}

	public int getWeight() {
		return (int)nativeWidth;
	}

	public int getDiff() {
		return difficulty;
	}
}

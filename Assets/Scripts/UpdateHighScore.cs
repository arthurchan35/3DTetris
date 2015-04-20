using UnityEngine;
using System.Collections;

public class UpdateHighScore : MonoBehaviour {
	
	string loginURL = "http://localhost/update.php";
	string playername = "";
	string label = "";
	int highscore = 0;

	void Start () {

		GameObject game = GameObject.Find ("GameManager");
		if (game != null)
			Debug.Log ("game is not null");
		else
			Debug.Log ("game is null");
		highscore = game.GetComponent<Gamemanager> ().getScore();
		Destroy(game);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

	}
	void OnGUI() {
		//Cursor.visible = true;
			GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), loginWindow, "HIGHSCORE");
		
	}
	
	void loginWindow(int windowID) {

		GUI.Label (new Rect (140, 40, 130, 100), "ENTER YOUR NAME");
		playername = GUI.TextField (new Rect (25, 60, 375, 30), playername);
		if (GUI.Button (new Rect (25, 160, 375, 50), "Send")) {
			StartCoroutine(HandleLogin(playername, highscore));
		}

		GUI.Label(new Rect(Screen.width / 2 - 40, 40, 80, 120), label);
		if (GUI.Button (new Rect (Screen.width / 2 - 40, Screen.height - 40, 80, 40), "RERTURN")) {
			Application.LoadLevel("menu2");
		}
	}
	
	IEnumerator HandleLogin(string pn, int hs) {
		string url = this.loginURL + "?player=" + playername + "&score=" + highscore;
		Debug.Log (url);
		WWW reader = new WWW (url);
		yield return reader;

	}
	
}

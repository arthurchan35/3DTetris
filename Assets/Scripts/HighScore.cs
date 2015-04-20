using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {
	
	string loginURL = "http://localhost/phpinfo.php";
	string playername = "";
	string label = "";
	string highscore = "";
	
	void OnGUI() {
		GameObject ms = GameObject.Find ("Menu");
		Destroy(ms);

		GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), loginWindow, "HIGHSCORE");
		
	}
	
	void loginWindow(int windowID) {
		StartCoroutine(HandleLogin(playername, highscore));
		//GUI.Label (new Rect (140, 40, 130, 100), "PLAYERNAME");
		//playername = GUI.TextField (new Rect (25, 60, 375, 30), playername);
		//GUI.Label (new Rect (140, 92, 130, 100), "HIGHSCORE");
		//highscore =GUI.TextField (new Rect (25, 115, 375, 30), highscore);
		//if (GUI.Button (new Rect (25, 160, 375, 50), "Send")) {
		//	StartCoroutine(HandleLogin(playername, highscore));
		//}
		
		GUI.Label(new Rect(Screen.width / 2 - 40, 40, 120, 240), label);
		if (GUI.Button (new Rect (Screen.width / 2 - 40, Screen.height - 40, 80, 40), "RERTURN")) {
			Application.LoadLevel("menu2");
		}
	}
	
	IEnumerator HandleLogin(string pn, string hs) {
		WWW reader = new WWW (loginURL);
		yield return reader;
		
		if (reader.error != null) {
			label = "error connection";
		} else {
			label = reader.text;
		}
	}
	
}

using UnityEngine;
using System.Collections;

// this script is connected to the emtpy gameObject in the startup scene
// it initializes the sound system and makes it persistent
// so it "survives" scene change

public class SoundManager : MonoBehaviour {

	// check if sound is muted
	bool isMuted;

	// Use this for initialization
	void Start () {
		// make this object persistent
		DontDestroyOnLoad (this);
		// play sound
		isMuted = false;
		// immediately load the menu scene
		Application.LoadLevel ("menu2");
	}
	
	// Update is called once per frame
	void Update () {
		// if player presses "M", sound will be muted / unmuted
		if (Input.GetKeyUp (KeyCode.M)) {
			isMuted = !isMuted;
		}
		// set the sound level
		if (isMuted)
			GetComponent<AudioSource>().volume = 0f;
		else
			GetComponent<AudioSource>().volume = 0.1f;
	}
}

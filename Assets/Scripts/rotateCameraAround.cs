using UnityEngine;
using System.Collections;

// this script is attached to the main camera in the menu scene
//
// it simply rotates the camera around the scenery

public class rotateCameraAround : MonoBehaviour {

	float degrees = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// this is the magic: set a fix point to rotate around,
		// then use deltatime and a given angle to rotate the camera
		transform.RotateAround( new Vector3(6.24f,2.46f,3.88f), Vector3.up, degrees*Time.deltaTime);
	}
}

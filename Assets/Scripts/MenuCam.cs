using UnityEngine;
using System.Collections;

public class MenuCam : MonoBehaviour {
	
	float lookSens;
	float yRot;
	float xRot;
	float yRotV;
	float xRotV;
	
	
	// Use this for initialization
	void Start () {
		lookSens = 5.0f;
		//Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		yRot += Input.GetAxis ("Mouse X") * lookSens;
		xRot -= Input.GetAxis ("Mouse Y") * lookSens;
		transform.rotation = Quaternion.Euler (xRot, yRot, 0);
	}
	
	public float getYRot() {
		return this.yRot;
	}
	
	public float getXRot() {
		return this.xRot;
	}
}

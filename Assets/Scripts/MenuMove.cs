using UnityEngine;
using System.Collections;

public class MenuMove : MonoBehaviour {
	
	GameObject camObj;
	float leftRight;
	float forwardBackward;
	float verticalSpeed;
	Vector3 move;
	// Use this for initialization
	void Start () {
		camObj = GameObject.Find("Main Camera");
		leftRight = 0;
		forwardBackward = 0;
		verticalSpeed = 0;
		move = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (camObj != null) {
			float y = camObj.GetComponent<MenuCam> ().getYRot ();
			float x = camObj.GetComponent<MenuCam> ().getXRot ();
			transform.rotation = Quaternion.Euler (0, y, 0);
		}
		CharacterController cc = GetComponent<CharacterController> ();
		leftRight = Input.GetAxis("Horizontal");
		forwardBackward = Input.GetAxis("Vertical");
		verticalSpeed += Physics.gravity.y * Time.deltaTime;
		if (cc.isGrounded) {
			if(Input.GetKeyDown(KeyCode.Space))
				verticalSpeed = 5;
		}
		move = new Vector3 (leftRight * 5, verticalSpeed, forwardBackward * 5);
		move = transform.rotation * move;
		cc.Move(move * Time.deltaTime);
	}
}

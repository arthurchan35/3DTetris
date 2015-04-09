using UnityEngine;
using System.Collections;

public class MoveControl : MonoBehaviour {

	GameObject camObj;
	float leftRight;
	float forwardBackward;
	// Use this for initialization
	void Start () {
		camObj = GameObject.Find("Main Camera");
		leftRight = 0;
		forwardBackward = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float y = camObj.GetComponent<CamControl> ().getYRot ();
		float x = camObj.GetComponent<CamControl> ().getXRot ();
		transform.rotation = Quaternion.Euler (x, y, 0);

		CharacterController cc = GetComponent<CharacterController> ();
		leftRight = Input.GetAxis("Horizontal");
		forwardBackward = Input.GetAxis("Vertical");

		Vector3 move = new Vector3 (leftRight, 0, forwardBackward);
		move = transform.rotation * move;
		cc.Move(move);
	}
}

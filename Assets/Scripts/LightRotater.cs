using UnityEngine;
using System.Collections;

// this script was intended to move the clouds
// since the game produces a lot of cpu workload (due to the size of gameobjects)
// I decided not to activate it

public class LightRotater : MonoBehaviour {

	float speed = 5f;
	int moveLight = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.L))
			moveLight += 1;

		if (moveLight % 3 == 1)
			transform.RotateAround (new Vector3 (6.24f, 2.46f, 3.88f), Vector3.left, speed * Time.deltaTime);
		else if (moveLight % 3 == 2)
			transform.RotateAround (new Vector3 (6.24f, 2.46f, 3.88f), Vector3.right, speed * Time.deltaTime);
		
			//transform.Rotate(new Vector3 (90f, 0, 0));

	}
}

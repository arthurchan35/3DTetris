using UnityEngine;
using System.Collections;

// this script was intended to move the clouds
// since the game produces a lot of cpu workload (due to the size of gameobjects)
// I decided not to activate it

public class cloudMover : MonoBehaviour {

	float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		position.z = position.z - speed*Time.deltaTime;
		if (position.z < 2)
			position.z = 16;
		transform.position = position;

	}
}

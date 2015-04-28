using UnityEngine;
using System.Collections;


public class Block : MonoBehaviour {

	public string[] BlockStructure;
	public GameObject cubus;
	public Material blockMaterial;
	public AudioClip swoosh;
	
	private GameObject block;
	private bool [,,] blockMatrix;
	private int size;
	private int halfSize;
	private float halfSizeFloat;
	
	private int yPosition;
	private int xPosition;
	private int zPosition;

	public float fallingInterval = 1f;
	float movingInterval = 0.1f;
	float elapsedTimeFalling = 0f;
	float elapsedTimeMoving = 0f;

	private bool isFrozen;
	private bool dropped;



	void Awake () {
	
		size = BlockStructure.Length;
		Debug.Log("the size is: " + size);

		halfSize = size / 2;
		halfSizeFloat = size * 0.5f;


		blockMatrix = new bool[size, size, size];
		// Instantiate Block from the Blockstructure
		for (int y = 0; y < size; y++) {
			for (int x = 0; x < size; x++) {
				if (BlockStructure[y][x].ToString() == "1")
				{
					blockMatrix[x,y,0] = true;
					// if Block Bit is set, Instantiate Block 
					block  = Instantiate(cubus,new Vector3(x-halfSizeFloat,halfSizeFloat-y,0.5f -halfSizeFloat), Quaternion.identity) as GameObject;
					// give color to the block
					block.transform.GetComponentInChildren<MeshRenderer>().GetComponent<Renderer>().material = blockMaterial;
					// bind block to parent for rotation and movement 
					block.transform.parent = this.transform;
				}
			}
		}
		Vector3 position = transform.position;
		position.x = Gamemanager.thisOne.getFieldWidth () / 2 + (size % 2 == 0 ? 0.0f : 0.5f);
		xPosition = (int)(position.x - halfSizeFloat);
		yPosition = Gamemanager.thisOne.getFieldHeight () - 1;
		position.y = yPosition - halfSizeFloat; 
		position.z = Gamemanager.thisOne.getFieldLength () / 2 + (size % 2 == 0 ? 0.5f : 0.0f);
		if (size == 2)
			zPosition = (int)(position.z - 0);
		else
			zPosition = (int)(position.z - 1f);
	//	position.x = Gamemanager.thisOne.getFieldWidth () / 2;
	//	xPosition = (int)(position.x - halfSizeFloat);
	//	yPosition = Gamemanager.thisOne.getFieldHeight () - 1;
	//	position.y = yPosition - halfSizeFloat; 
	//	position.z = Gamemanager.thisOne.getFieldLength () / 2;
	//	zPosition = (int)(position.z - halfSizeFloat);


		transform.position = position;
		
		isFrozen = false;
		dropped = false;


		if (Gamemanager.thisOne.checkBlock (blockMatrix, size, xPosition, yPosition, zPosition)) {
			// this game is over
			Gamemanager.thisOne.gameOver();
			// and out
			return;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!isFrozen){
			elapsedTimeFalling += Time.deltaTime;
			elapsedTimeMoving += Time.deltaTime;
			if (elapsedTimeFalling >= fallingInterval) {
				elapsedTimeFalling -= fallingInterval;
				fall ();
			}
			if (elapsedTimeMoving >= movingInterval) {
				checkInput ();
			}
		}
	}

	public void freeze(){
		isFrozen = true;
		Vector3 position = transform.position;
		position.x = -8.5f;
		position.y = 15.25f;
		transform.position = position;
	}

	// let the block fall
	void fall() {
			// let block fall (virtually)
			yPosition--;
			// is there a collision at the new position?
			if (Gamemanager.thisOne.checkBlock(blockMatrix,size, xPosition, yPosition, zPosition)) {
				// Then set the block at actual position...
				Gamemanager.thisOne.setBlock(blockMatrix,size, xPosition, yPosition+1, zPosition,dropped);
				// and destroy the gameObject
				Destroy(gameObject);
				return;
			}

			// move the block physically
			Vector3 position = transform.position;
			position.y -= 1;
			transform.position = position;
	}


	// steer the block
	void checkInput() {
	
		if (Input.GetKeyUp(KeyCode.Keypad4)) {
			moveAlongX(-1);
		}

		if (Input.GetKeyUp(KeyCode.Keypad6)) {
			moveAlongX(1);
		}
		if (Input.GetKeyUp (KeyCode.Keypad8)) {
			moveAlongZ(1);
		}
		if (Input.GetKeyUp (KeyCode.Keypad2)) {
			moveAlongZ(-1);
		}

		if (Input.GetKeyUp (KeyCode.Keypad1)) {
			rotateBlockLeft();
		}

		if (Input.GetKeyUp (KeyCode.Keypad3)) {
			rotateBlockRight();
		}
		if (Input.GetKeyUp (KeyCode.Keypad7)) {
			rotateBlockForward();
		}
		if (Input.GetKeyUp (KeyCode.Keypad9)) {
			rotateBlockBackward();
		}

		// drop the block
		if (Input.GetKeyUp (KeyCode.Keypad5)) {
			fallingInterval = 0f;
			dropped = true;
		}
	}

	// move the block physically
	void moveAlongX(int direction) {
		if (!Gamemanager.thisOne.checkBlock(blockMatrix, size,xPosition+direction, yPosition, zPosition)) {
			GetComponent<AudioSource>().PlayOneShot(swoosh);
			xPosition += direction;
			Vector3 position = transform.position;
			position.x += direction;
			transform.position = position;
		}
	}

	void moveAlongZ(int direction) {
		if (!Gamemanager.thisOne.checkBlock (blockMatrix, size, xPosition, yPosition, zPosition + direction)) {
			GetComponent<AudioSource>().PlayOneShot(swoosh);
			zPosition += direction;
			Vector3 position = transform.position;
			position.z += direction;
			transform.position = position;
		}
	}

	void rotateBlockRight() {
		bool[,,] tempMatrix = new bool[size, size,size];
		for (int z = 0; z < size; z++) {
			for (int y = 0; y < size; y++) {
				for (int x = 0; x < size; x++) {
					tempMatrix [y, x, z] = blockMatrix [x, (size - 1) - y, z];
				}
			}
		}
		if (!Gamemanager.thisOne.checkBlock (tempMatrix, size, xPosition, yPosition, zPosition)) {
			GetComponent<AudioSource>().PlayOneShot(swoosh);
			System.Array.Copy(tempMatrix, blockMatrix, size * size * size);
			transform.Rotate(Vector3.back*+90.0f, Space.World);
		}
	}

	void rotateBlockLeft() {

		bool[,,] tempMatrix = new bool[size, size, size];
		for ( int z = 0; z < size; z++){
			for (int y = 0; y < size; y++) {
				for (int x = 0; x < size;x++) {
					tempMatrix[(size - 1) - y, (size - 1) - x, z] = blockMatrix[x,(size-1)-y, z];
				}
			}
		}

		if (!Gamemanager.thisOne.checkBlock (tempMatrix, size, xPosition, yPosition, zPosition)) {
			GetComponent<AudioSource>().PlayOneShot(swoosh);
			System.Array.Copy(tempMatrix, blockMatrix, size * size * size);
			transform.Rotate(Vector3.forward*+90.0f, Space.World);
		}
	}

	void rotateBlockBackward() {
		bool[,,] tempMatrix = new bool[size, size, size];
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				for ( int z = 0; z < size; z++)
					tempMatrix[x ,(size-1)-z, (size-1)-y] = blockMatrix[x,(size-1)-y, z];
			}
		}

		if (!Gamemanager.thisOne.checkBlock (tempMatrix, size, xPosition, yPosition, zPosition)) {
			GetComponent<AudioSource>().PlayOneShot(swoosh);
			System.Array.Copy(tempMatrix, blockMatrix, size * size * size);
			transform.Rotate(Vector3.left*+90.0f, Space.World);
		}
	}

	void rotateBlockForward() {
		bool[,,] tempMatrix = new bool[size, size, size];
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				for ( int z = 0; z < size; z++)
					tempMatrix[x ,z, y] = blockMatrix[x,(size-1)-y, z];
			}
		}

		if (!Gamemanager.thisOne.checkBlock (tempMatrix, size, xPosition, yPosition, zPosition)) {
			GetComponent<AudioSource>().PlayOneShot(swoosh);
			System.Array.Copy(tempMatrix, blockMatrix, size * size * size);
			transform.Rotate(Vector3.right*+90.0f, Space.World);

		}
	} 	

	public void setFallingInterval(float interval){
		fallingInterval = interval;
	}

	public bool isDropped(){
		return dropped;
	}
}

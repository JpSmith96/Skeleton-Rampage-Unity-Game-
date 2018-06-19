using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
	public Transform cam, player, center;
	public float viewDistance, viewScrollSpeed = 2f, viewMin = -6.8f, viewMax = -10f;
	public float speed, turnSpeed = 100f;
	public float mouseSensitivityX = 120f, mouseSensitivityY = 50f, mouseHeight = 3f, mouseRight = 0.5f;

	HealthScript hp;
	Animator anim;
	Rigidbody rb;

	float mX, mY;
	float forwardBack, strafe;




	void Awake(){
		viewDistance = -3;
		anim = player.GetComponent<Animator> ();
		rb = player.GetComponent<Rigidbody> ();
		hp = player.GetComponent<HealthScript> ();
		Cursor.lockState = CursorLockMode.Locked; //locks the players mouse to the centre of the screen and sets the curser to invisible.
		Cursor.visible = false;
	}


	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//updates the players movement and mouse look each update
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void FixedUpdate(){
		//viewDistanceControl ();
		mouseLook ();
		playerMove ();
	}






	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Takes the vertical and horizontal axis and multiplies them by the speed
	//if the axis are not 0 then the running animation will play
	//The player is then moved based on their axis with a gravitational strength of 2.5 units
	//The player's rigidbody velocity is then increaded to meet the player movement vector
	//The centre position which the camera pivots around is then set behind the player and the rigid body follows the rotation of the centre point
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void playerMove(){
		forwardBack = Input.GetAxis ("Vertical") * speed;
		strafe = Input.GetAxis ("Horizontal") * speed;

		if (forwardBack != 0 || strafe !=0)
			anim.SetBool ("isRunning", true);
		else
			anim.SetBool ("isRunning", false);
		
		Vector3 move = new Vector3 (strafe, -2.5f, forwardBack);
		move = player.rotation * move;

		rb.velocity = move * speed;

		//rb.position = new Vector3 (rb.position.x, 0.0f, rb.position.z);
		//player.GetComponent<CharacterController> ().Move (move * Time.deltaTime);
		//player.GetComponent<Rigidbody> ().position = (move * Time.deltaTime);


		center.position = new Vector3 (player.position.x, player.position.y + mouseHeight, player.position.z);
		Quaternion angle = Quaternion.Euler (0, center.eulerAngles.y, 0);
		rb.rotation = Quaternion.Slerp (rb.rotation, angle, turnSpeed);
		//player.rotation = Quaternion.Slerp(player.rotation, angle, Time.deltaTime * turnSpeed);


	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Takes both axis' of mouse movement and then applies that rotation to the centre point
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void mouseLook(){
		mX += Input.GetAxis ("Mouse X");
		mY += Input.GetAxis ("Mouse Y");
		mY = Mathf.Clamp (mY, -60f, 60f);
		//cam.LookAt (center);
		center.localRotation = Quaternion.Euler (-mY, mX, 0);

	}







	/*void viewDistanceControl(){
		viewDistance += Input.GetAxis ("Mouse ScrollWheel") * viewScrollSpeed;
		if (viewDistance > viewMin)
			viewDistance = viewMin;
		if (viewDistance < viewMax)
			viewDistance = viewMax;

		cam.transform.localPosition = new Vector3 (0, 0, viewDistance);
	}*/


}

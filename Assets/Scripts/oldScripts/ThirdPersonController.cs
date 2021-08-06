using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour 
{

	public Transform playerCam, character, centerPoint;
	private float mouseX, mouseY;
	public float mouseYPosition = 1f;
	public float mouseSensitivity = 3.5f;
	private float moveFB, moveLR;
	public float moveSpeed = 2.25f;
	public float turboSpeed = 4f;
	public float zoom;
	public float zoomSpeed = 2f;
	public float zoomMin = -2f;
	public float zoomMax = -10f;
	public float rotationSpeed = 5f;
	public bool serveReady = false;
	public bool serveReady2 = true;
	public bool pass = false;

	CursorLockMode lockMode;
	CursorLockMode unlockMode;

	static Animator anim;

	void Awake () {
		
	}



	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false;
		//zoom = -3.5f;
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		

		if (Input.GetKeyDown (KeyCode.B) & serveReady == true | Input.GetMouseButton(0) & serveReady == true) 
			{
				serveReady = !serveReady;
				serveReady2 = !serveReady2;
				pass = true;
			}
			
		if (Input.GetKeyDown (KeyCode.B) & serveReady == false & pass == false) 
		{
			//Cursor.visible = !serveReady;
			serveReady = !serveReady;
			serveReady2 = !serveReady2;

		} 

		playerCam.transform.localPosition = new Vector3 (0, 4f, zoom);
		playerCam.LookAt (centerPoint);

		if(serveReady2 == true) 
		{
			pass = false;	

			if (Input.GetButton ("Left Shift")) 
			{
				zoom += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
			}

			if (zoom > zoomMin) {
				zoom = zoomMin;
			}
			if (zoom < zoomMax) {
				zoom = zoomMax;
			}




			mouseX += Input.GetAxis ("Mouse X") * mouseSensitivity * 1.2f;
			mouseY -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			mouseY = Mathf.Clamp (mouseY, -30f, 40f);
			

			centerPoint.localRotation = Quaternion.Euler (mouseY, mouseX, 0);



			//Add movement for moving right only after a certain threshold
			if (anim.GetFloat("walkRight") > 0.35f & anim.GetFloat("walkRight") <= 1.0f & anim.GetBool ("wasRunningRight") == false & anim.GetBool("isWalkingRight") == true) 
			{
				moveFB = Input.GetAxis ("Vertical") * moveSpeed;
				moveLR = 1f*moveSpeed*(anim.GetFloat("walkRight")-0.35f);
				//Debug.Log (moveLR);
			}


			//Smooth Animation from Running Right to Walking Right
			if (anim.GetFloat("walkRight") >= 1f & anim.GetFloat("walkRight") <= 1.95f & anim.GetBool ("wasRunningRight") == true & anim.GetBool("isWalkingRight") == true) 
			{
				moveFB = Input.GetAxis ("Vertical") * moveSpeed;
				moveLR = (1.5f/1.541f/1.497436f)*moveSpeed*(anim.GetFloat("walkRight"));
				Debug.Log (moveLR);
			}




			//Add movement for running right only after a certain threshold
			if (anim.GetFloat("walkRight") > 1.35f & anim.GetBool ("isRunningRight") == true & anim.GetBool("isWalkingRight") == false)
			{
				moveFB = Input.GetAxis ("Vertical") * moveSpeed;
				moveLR = moveSpeed*(anim.GetFloat("walkRight")-0.35f);
				//Debug.Log (moveLR);
			}



			if (anim.GetBool ("isIdle") == true & Input.GetAxis ("Horizontal") == 0f ) 
			{
				moveFB = 0f;
				moveLR = 0f;
			}


			if (Input.GetButton ("Fire3") == true) {
				moveFB = Input.GetAxis ("Vertical") * turboSpeed;
				moveLR = Input.GetAxis ("Horizontal") * turboSpeed;
			}

			Vector3 movement = new Vector3 (moveLR, 0, moveFB);
			movement = character.rotation * movement;

			character.GetComponent<CharacterController> ().Move (movement * Time.deltaTime);
			centerPoint.position = new Vector3 (character.position.x, character.position.y + mouseYPosition, character.position.z);

			if (Input.GetAxis ("Vertical") > 0 | Input.GetAxis ("Vertical") < 0) {
				Quaternion turnAngle = Quaternion.Euler (0, centerPoint.eulerAngles.y, 0);
				character.rotation = Quaternion.Slerp (character.rotation, turnAngle, Time.deltaTime * rotationSpeed);
			}
			
		}



	
	}
		


}

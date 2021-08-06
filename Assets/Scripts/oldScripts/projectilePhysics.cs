using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilePhysics : MonoBehaviour {

	public Rigidbody ball;
	public Transform ballsy;
	public Transform target;
	public Transform setterTarget;
	public Transform volador;
	public float adjustment = 0.175f;
	public float rotateSpeed;

	public float h = 5f;
	public float gravity = 9.8f;

	public float mouseXpos;
	public float mouseYpos;


	void Start ()
	{
		ball.useGravity = false;
	}


	void Update()
	{

		if (ServePositionUI.SelectServePosition) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				mouseXpos = Input.mousePosition.x;
				mouseYpos = Input.mousePosition.y;
				target.position = new Vector3 ((mouseXpos - 768f)/27.778f, 0.001f, (mouseYpos-232f)/27.778f);
				Debug.Log (mouseXpos);
				Debug.Log (mouseYpos);
			}
		} 
		else 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				Launch ();
			}
		}


		//if (volador.position.x - ball.position.x < 1f & volador.position.y - ball.position.y < 0.1f & volador.position.z - ball.position.z < 1f & Input.GetMouseButtonDown (0)) 
		if (Mathf.Sqrt((volador.position.x - ball.position.x)*(volador.position.x - ball.position.x) +(volador.position.y - ball.position.y)*(volador.position.y - ball.position.y) + (volador.position.z - ball.position.z)*(volador.position.z - ball.position.z)) < 3f & Input.GetMouseButtonDown (1)) 
		{
			LaunchReception ();
		}



		//Quaternion spin = Quaternion.Euler (ballsy.eulerAngles.x, 0, 0);
		//ball.rotation = Quaternion.Slerp(ballsy.rotation, spin, Time.deltaTime*10);
		//Quaternion spin = Quaternion.Euler (-ballsy.eulerAngles.x, 0, 0);
		//ball.rotation = Quaternion.Euler(360*Time.deltaTime, 0, 0);
		rotate ();
	}


	void rotate() 
	{
		ball.transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime, Space.World);
	}


	void Launch()
	{
		Physics.gravity = Vector3.up * -gravity;
		ball.useGravity = true;
		ball.velocity = CalculateLaunchVelocity ();
		print (CalculateLaunchVelocity ());

	}


	Vector3 CalculateLaunchVelocity()
	{
		//target.position = new Vector3 ((mouseXpos - 768f)/27.778f, 0.001, (mouseYpos-232f)/27.778f); 
		float displacementY = ball.position.y - target.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
	
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (2 * gravity * (h-displacementY));
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt (2 * h / gravity) + Mathf.Sqrt (2 * (h - displacementY - adjustment) / gravity));

		return velocityXZ + velocityY;
	}

	void LaunchReception()
	{
		Physics.gravity = Vector3.up * -gravity;
		ball.useGravity = true;
		ball.velocity = CalculateLaunchVelocityReception ();
		print (CalculateLaunchVelocityReception ());

	}


	Vector3 CalculateLaunchVelocityReception()
	{
		//target.position = new Vector3 ((mouseXpos - 768f)/27.778f, 0.001, (mouseYpos-232f)/27.778f); 
		float displacementY = ball.position.y - setterTarget.position.y;
		Vector3 displacementXZ = new Vector3 (setterTarget.position.x - ball.position.x, 0, setterTarget.position.z - ball.position.z);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (2 * gravity * (h-displacementY));
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt (2 * h / gravity) + Mathf.Sqrt (2 * (h - displacementY - adjustment) / gravity));

		return velocityXZ + velocityY;
	}


}

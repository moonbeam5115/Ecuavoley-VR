using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour {

	public GameObject ballObject;
	public GameObject Launcher;
	public GameObject ballShadow;
	public GameObject inGameVolleyBall;
	public float waitTime = 10.0f;
	public float heightReturn = 5f;
	public float adjustment = 0.175f;
	public float gravity = 9.8f;

	public Transform startLaunchPosition;
	public Transform attackingPosition;

	public Rigidbody ballRigidbody;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("InstantiateBall", 10, 10);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate ()
	{
		inGameVolleyBall = GameObject.FindGameObjectWithTag ("volleyBall");
		startLaunchPosition = Launcher.transform;
		if (inGameVolleyBall != null & ballShadow != null) {
			
			GameObject.FindGameObjectWithTag("ballShadow").transform.position = new Vector3 (inGameVolleyBall.transform.position.x, 0.01f, inGameVolleyBall.transform.position.z);
		} else 
		{
			return;
		}

	}
		


	Vector3 CalculateLaunchVelocity()
	{ 
		float displacementY = startLaunchPosition.position.y - attackingPosition.position.y;

		Vector3 displacementXZ = new Vector3 (attackingPosition.position.x - startLaunchPosition.position.x, 0, attackingPosition.position.z - startLaunchPosition.position.z);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (2 * gravity *Mathf.Abs( (heightReturn-displacementY)));
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt (2 * heightReturn / gravity) + Mathf.Sqrt (2 * Mathf.Abs( (heightReturn - displacementY- adjustment)) / gravity));

		return velocityXZ + velocityY;

	}


	void InstantiateBall ()
	{
		GameObject obj = Instantiate (ballObject, new Vector3(startLaunchPosition.position.x, startLaunchPosition.position.y , startLaunchPosition.position.z), Quaternion.identity);
		ballRigidbody = obj.GetComponent<Rigidbody> ();
		ballRigidbody.tag = "volleyBall";
		LaunchBall (ballRigidbody);
		Instantiate (ballShadow,  startLaunchPosition);
		ballShadow.tag = "ballShadow";
	}


	void LaunchBall (Rigidbody balon)
	{
		balon.velocity = CalculateLaunchVelocity ();
		print (CalculateLaunchVelocity ());
	}



}

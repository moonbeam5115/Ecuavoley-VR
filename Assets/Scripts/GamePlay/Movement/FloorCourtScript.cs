using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCourtScript : MonoBehaviour {

	public GameObject voladorAttack;
	public GameObject balon;

	public Collider floorCourtLeft;
	public Collider floorCourtRight;
	VoladorServe voladorServe;
	bool collided;


	// Use this for initialization
	void Start () {
		voladorServe = voladorAttack.GetComponent<VoladorServe> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

//		balon = GameObject.FindGameObjectWithTag ("volleyBall");
//	if (balon != null) 
//	{
//
//		if (balon.transform.position.y < 0.5)
//		{
//			if (collided == true) 
//			{
//				Destroy (balon);
//				voladorServe.receiveStateCheck = false;
//				collided = false;
//			}
//		}
//
//	}

	}

	void OnTriggerEnter(Collider ball)
	{
		if (ball.tag == "volleyBall") 
		{
			collided = true;
		}
	}

}

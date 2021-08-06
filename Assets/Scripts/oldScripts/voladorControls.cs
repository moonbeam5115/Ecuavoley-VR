using System.Collections.Generic;
using UnityEngine;

public class voladorControls : MonoBehaviour 
{

	static Animator anim;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			anim.SetTrigger ("walkFwd");
			anim.SetBool ("isWalkingFwd", true);
			anim.SetBool ("isIdle", false);
		} else if (Input.GetKeyUp (KeyCode.W)) 
		{
			anim.SetBool ("isWalkingFwd", false);
		} 
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			anim.SetTrigger ("walkBkwd");
			anim.SetBool ("isWalkingBkwd", true);
			anim.SetBool ("isIdle", false);
		}
		else if (Input.GetKeyUp (KeyCode.S))
		{
			anim.SetBool ("isWalkingBkwd", false);	
		}

		if (Input.GetKeyDown (KeyCode.V)) 
		{
			anim.SetTrigger ("receptionDig");
		}

		if (Input.GetKey(KeyCode.LeftShift) & Input.GetKey (KeyCode.W))
		{
			anim.SetTrigger ("runFwd");
			anim.SetBool ("isRunningFwd", true);
			anim.SetBool ("isIdle", false);
		} else if (Input.GetKeyUp (KeyCode.W) | Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			anim.SetBool ("isRunningFwd", false);
			anim.ResetTrigger ("runFwd");
		} 


	}
}
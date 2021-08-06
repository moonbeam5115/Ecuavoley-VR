using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controls : MonoBehaviour {


	private float movement;
	private float numPasser_XIdle;
	private float numPasser_YIdle;
	private float XWalkSpeed;
	private float YWalkSpeed;

	public float XSpeedFactor = 2.0f;
	public float YSpeedFactor = 2.0f;

	static Animator anim;

	public enum MovementType {
		Idle,
		WalkingFwd,
		WalkingRight,
		WalkingLeft,
		WalkingBckwd,
		RunningFwd,
		RunningRight,
		RunningLeft,
		RunningBckwd,
		WalkingFwdRight,
		WalkingFwdLeft,
		WalkingBckwdRight,
		WalkingBckwdLeft,
		RunningFwdRight,
		RunningFwdLeft,
		RunningBckwdRight,
		RunningBckwdLeft
	}

	public MovementType movementType;




	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		animationStateManager ();
	}




	void animationStateManager()
	{
		
	}

}

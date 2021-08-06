using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocadorControls : MonoBehaviour 
{

	private float movement;
	private float numPasser_XIdle;
	private float numPasser_YIdle;
	private float XWalkSpeed;
	private float YWalkSpeed;

	private float previousInputXValue;
	private float currentInputXValue;
	private float previousInputYValue;
	private float currentInputYValue;
	private float deltaInputX;
	private float deltaInputY;



	public float XSpeedFactor = 2.0f;
	public float YSpeedFactor = 2.0f;

	static Animator anim;
	// Use this for initialization

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


	void Start () 
	{
		anim = GetComponent<Animator> ();
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(anim.GetBool("ReceivingInput"))
		{
		animationStateManager ();
		}
		if (Input.GetKeyDown (KeyCode.V)) 
		{
			anim.SetTrigger ("ReceptionDig");
		}

	}

	#region Methods

	void animationStateManager ()
	{

	#region SETTING FLOATS AND BOOLS FOR ANIMATIONS

		switch(movementType)
		{
		#region IDLE AND WALKING
		case MovementType.Idle:
		#region IDLE TO MOVEMENT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();


			//RETURNING FROM MOVEMENT
			if(anim.GetBool("wasMoving"))
			{
				numPasser_YIdle = anim.GetFloat("ySpeed");
				numPasser_XIdle = anim.GetFloat("xSpeed");
				numPasser_YIdle = Mathf.Lerp(numPasser_YIdle, 0f, YSpeedFactor*Time.deltaTime);
				numPasser_XIdle = Mathf.Lerp(numPasser_XIdle, 0f, XSpeedFactor*Time.deltaTime);
				if(Mathf.Abs(numPasser_XIdle) < 0.05f)
				{
					numPasser_XIdle = 0f;
				}
				if(Mathf.Abs(numPasser_YIdle) < 0.05f)
				{
					numPasser_YIdle = 0f;
				}

			}
			else
			{
				numPasser_YIdle = 0f;
				numPasser_XIdle = 0f;
			}

			//Set XSpeed and YSpeed
			anim.SetFloat("ySpeed", numPasser_YIdle);
			anim.SetFloat("xSpeed", numPasser_XIdle);
			if(numPasser_YIdle < 0.001f && numPasser_XIdle < 0.001f)
			{
				anim.SetBool("isIdle", true);
				anim.SetBool("wasIdle", false);
			}
		#region FWD-BACK-RIGHT-LEFT 
			//WALKING FWD
			if(deltaInputY > 0 & currentInputYValue > 0 & currentInputXValue == 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingFwd", true);
				movementType = MovementType.WalkingFwd;
			}
			if(currentInputYValue > 0.5f & currentInputXValue == 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingFwd", true);
				movementType = MovementType.WalkingFwd;
			}


			//WALKING RIGHT
			if(currentInputYValue == 0 & currentInputXValue > 0 & deltaInputX > 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingRight", true);
				movementType = MovementType.WalkingRight;
			}


			//WALKING LEFT
			if(currentInputYValue == 0 & currentInputXValue < 0 & deltaInputX < 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingLeft", true);
				movementType = MovementType.WalkingLeft;
			}

			//WALKING BCKWD
			if(deltaInputY < 0 & currentInputYValue < 0 & currentInputXValue == 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingBckwd", true);
				movementType = MovementType.WalkingBckwd;
			}


		#endregion
		#region DIAGONAL
			//WALKING FWD_RIGHT
			if(deltaInputY > 0 & deltaInputX > 0 & currentInputXValue > 0 & currentInputYValue > 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingFwdR", true);
				movementType = MovementType.WalkingFwdRight;
			}

			//WALKING FWD_LEFT
			if(deltaInputY > 0 & deltaInputX < 0 & currentInputXValue < 0 & currentInputYValue > 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingFwdL", true);
				movementType = MovementType.WalkingFwdLeft;
			}

			//WALKING BCKWD_RIGHT
			if(deltaInputY < 0 & deltaInputX > 0 & currentInputXValue > 0 & currentInputYValue < 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingBckwdR", true);
				movementType = MovementType.WalkingBckwdRight;
			}

			//WALKING BCKWD_LEFT
			if(deltaInputY < 0 & deltaInputX < 0 & currentInputXValue < 0 & currentInputYValue < 0)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("wasIdle", true);
				anim.SetBool("wasMoving", false);
				anim.SetBool("isMoving", true);
				anim.SetBool("isWalkingBckwdL", true);
				movementType = MovementType.WalkingBckwdLeft;
			}


		#endregion
			break;

		#endregion

		case MovementType.WalkingFwd:
		#region WALKING FWD	
			//Define deltaInputX and deltaInputY
				defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");

			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 1f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 0f, XSpeedFactor*Time.deltaTime);

				
			//Set X and Y walk speed and Booleans for MovementTypes
			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingFwd", true);
			anim.SetBool("wasWalkingFwd", false);

				//Toggle Inactive MovementType Booleans off
				anim.SetBool("wasWalkingRight", false);
				anim.SetBool("wasWalkingLeft", false);
				anim.SetBool("wasWalkingBckwd", false);
				anim.SetBool("wasWalkingFwdR", false);
				anim.SetBool("wasWalkingFwdL", false);
				anim.SetBool("wasWalkingBckwdR", false);
				anim.SetBool("wasWalkingBckwdL", false);

			//BACK TO IDLE
			if(currentInputYValue >= 0 & currentInputYValue < 0.5f & deltaInputY <= 0 & currentInputXValue == 0)
			{
				anim.SetBool("isWalkingFwd", false);
				anim.SetBool("wasWalkingFwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}

		#region RIGHT-LEFT-BACKWARD
			//WALKING RIGHT
			//LETTING GO OF WALK FWD AND PRESSING WALK RIGHT
			if(currentInputYValue > 0.5f & currentInputYValue < 1f & deltaInputY < 0 & currentInputXValue > 0 & deltaInputX > 0)
			{
				anim.SetBool("isWalkingFwd", false);
				anim.SetBool("wasWalkingFwd", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingRight;
			}

			//WALKING FWD RIGHT
				//Holding WALK FWD AND WALK RIGHT
			if(deltaInputX >= 0 & currentInputXValue > 0 & currentInputYValue == 1f)
			{
				anim.SetBool("isWalkingFwd", false);
				anim.SetBool("wasWalkingFwd", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwdRight;
			}

			//WALKING FWD LEFT
				//Holding WALK FWD AND WALK LEFT
			if(deltaInputX < 0 & currentInputXValue < 0 & currentInputYValue == 1f)
			{
				anim.SetBool("isWalkingFwd", false);
				anim.SetBool("wasWalkingFwd", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwdLeft;
			}
				
			//RUNNING FORWARD
			if(Input.GetKey(KeyCode.LeftShift) & currentInputYValue == 1f & currentInputXValue == 0f)
			{
				anim.SetBool("isWalkingFwd", false);
				anim.SetBool("wasWalkingFwd", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningFwd;
			}
		#endregion
		#region DIAGONAL

		#endregion

			break;
		#endregion

		case MovementType.WalkingRight:
		#region WALKING RIGHT
			//Define deltaInputX and deltaInputY
				defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 0f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 1f, XSpeedFactor*Time.deltaTime);


			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingRight", true);
			anim.SetBool("wasWalkingRight", false);

			//Toggle Inactive MovementType Booleans off
			anim.SetBool("wasWalkingFwd", false);
			anim.SetBool("wasWalkingLeft", false);
			anim.SetBool("wasWalkingBckwd", false);
			anim.SetBool("wasWalkingFwdR", false);
			anim.SetBool("wasWalkingFwdL", false);
			anim.SetBool("wasWalkingBckwdR", false);
			anim.SetBool("wasWalkingBckwdL", false);


			//BACK TO IDLE
			if(currentInputXValue >= 0 & currentInputXValue < 0.5f  & deltaInputX <= 0 & currentInputYValue == 0)
			{
				anim.SetBool("isWalkingRight", false);
				anim.SetBool("wasWalkingRight", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}


			#region FOWARD-BACK-LEFT
			//WALKING FWD
				//LETTING GO OF WALK RIGHT AND PRESSING WALK FWD
			if(currentInputXValue < 1f & currentInputYValue > 0 & deltaInputY > 0)
			{
				anim.SetBool("isWalkingRight", false);
				anim.SetBool("wasWalkingRight", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwd;
			}

			//WALKING BCKWD
				//LETTING GO OF WALK RIGHT AND PRESSING WALK BCKWD
			if(currentInputXValue < 1f & currentInputYValue < 0 & deltaInputY < 0)
			{
				anim.SetBool("isWalkingRight", false);
				anim.SetBool("wasWalkingRight", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwd;
			}

			//WALKING LEFT
				//LETTING GO OF WALK RIGHT AND PRESSING WALK LEFT
			if(currentInputXValue < 0 & currentInputYValue == 0 & deltaInputX <= 0)
			{
				anim.SetBool("isWalkingRight", false);
				anim.SetBool("wasWalkingRight", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingLeft;
			}


			//RUNNING RIGHT
				//Holding Shift to RUN RIGHT
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputX >= 0 & currentInputXValue > 0 & currentInputYValue == 0)
			{
				anim.SetBool("isWalkingRight", false);
				anim.SetBool("wasWalkingRight", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningRight;
			}

			#endregion
			#region DIAGONAL
			//WALKING FWD RIGHT
				//Holding WALK FWD AND WALK RIGHT
			if(deltaInputY > 0 & currentInputXValue == 1)
			{
				anim.SetBool("isWalkingRight", false);
				anim.SetBool("wasWalkingRight", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwdRight;
			}


			//WALKING BACKWARD RIGHT
				//Holding WALK RIGHT AND WALK BACKWARD
			if(currentInputXValue == 1f & deltaInputY < 0 & currentInputYValue < 0)
			{
				anim.SetBool("isWalkingRight", false);
				anim.SetBool("wasWalkingRight", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwdRight;
			}

			#endregion

			break;
		#endregion

		case MovementType.WalkingFwdRight:
		#region WALKING FWD RIGHT
			//Define deltaInputX and deltaInputY
				defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 1f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 1f, XSpeedFactor*Time.deltaTime);


			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingFwdR", true);
			anim.SetBool("wasWalkingFwdR", false);

			//Toggle Inactive MovementType Booleans off
			anim.SetBool("wasWalkingFwd", false);
			anim.SetBool("wasWalkingLeft", false);
			anim.SetBool("wasWalkingBckwd", false);
			anim.SetBool("wasWalkingRight", false);
			anim.SetBool("wasWalkingFwdL", false);
			anim.SetBool("wasWalkingBckwdR", false);
			anim.SetBool("wasWalkingBckwdL", false);

			//BACK TO IDLE
			if(deltaInputY < 0 & deltaInputX < 0)
			{
				anim.SetBool("isWalkingFwdR", false);
				anim.SetBool("wasWalkingFwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}

			#region FORWARD-BACKWARD-RIGHT-LEFT
			//WALKING FWD
				//Letting go of WALK RIGHT to WALK FWD
			if(deltaInputX < 0 & currentInputYValue == 1f)
			{
				anim.SetBool("isWalkingFwdR", false);
				anim.SetBool("wasWalkingFwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwd;
			}


			//WALKING RIGHT
				//Letting go of WALK FWD to WALK RIGHT
			if(currentInputXValue > 0 & deltaInputY < 0 & currentInputYValue < 1f & currentInputYValue >= 0.5f)
			{
				anim.SetBool("isWalkingFwdR", false);
				anim.SetBool("wasWalkingFwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingRight;
			}
		

			//RUNNING FORWARD RIGHT
				//Holding WALK RIGHT AND RUN FORWARD
			if(Input.GetKey(KeyCode.LeftShift) & currentInputXValue == 1f & currentInputYValue > 0f)
			{
				anim.SetBool("isWalkingFwdR", false);
				anim.SetBool("wasWalkingFwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningFwdRight;
			}
			#endregion
			#region - DIAGONAL


			#endregion

			break;
			#endregion




		case MovementType.WalkingLeft:
		#region WALKING LEFT
			//Define X and Y DeltaInputs:
				defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 0f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, -1f, XSpeedFactor*Time.deltaTime);


			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingLeft", true);
			anim.SetBool("wasWalkingLeft", false);

			//Toggle Inactive MovementType Booleans off
			anim.SetBool("wasWalkingFwd", false);
			anim.SetBool("wasWalkingFwdR", false);
			anim.SetBool("wasWalkingBckwd", false);
			anim.SetBool("wasWalkingRight", false);
			anim.SetBool("wasWalkingFwdL", false);
			anim.SetBool("wasWalkingBckwdR", false);
			anim.SetBool("wasWalkingBckwdL", false);

			//BACK TO IDLE
			if(currentInputYValue == 0 & currentInputXValue <= 0 & deltaInputX > 0)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}
			if(currentInputYValue == 0 & currentInputXValue > 0 & deltaInputX > 0)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}

			#region FOWARD-BACK-RIGHT
			//RUNNING LEFT
				//Holding Shift to RUN LEFT
			if(Input.GetKey(KeyCode.LeftShift) & currentInputXValue == -1f & currentInputYValue == 0f)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningLeft;
			}

			//WALKING FWD
				//LETTING GO OF WALK LEFT AND PRESSING WALK FWD
			if(currentInputXValue > -1f & deltaInputX > 0 & currentInputYValue > 0 & deltaInputY > 0)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwd;
			}
			if(currentInputXValue > -1f & currentInputYValue > 0.9f)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwd;
			}

			//WALKING BCKWD
				//LETTING GO OF WALK LEFT AND PRESSING WALK BCKWD
			if(currentInputXValue > -1f & deltaInputX > 0 & currentInputYValue < 0 & deltaInputY < 0)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwd;
			}

			//WALKING RIGHT
				//LETTING GO OF WALK LEFT AND PRESSING WALK RIGHT
			if(!Input.GetKey(KeyCode.LeftShift) & currentInputXValue > 0 & currentInputYValue == 0 & deltaInputX > 0)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingRight;
			}

			#endregion
			#region DIAGONAL
			//WALKING FWD LEFT
				//Holding WALK FWD AND WALK LEFT
			if(currentInputXValue == -1f & deltaInputY > 0 & currentInputYValue > 0)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwdLeft;
			}


			//WALKING BACKWARD LEFT
				//Holding WALK LEFT AND WALK BACKWARD
			if(currentInputXValue == -1f & deltaInputY < 0 & currentInputYValue < 0)
			{
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("wasWalkingLeft", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwdLeft;
			}

			#endregion
			break;
			#endregion

		case MovementType.WalkingFwdLeft:
		#region WALKING FWD LEFT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 1f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, -1f, XSpeedFactor*Time.deltaTime);


			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingFwdL", true);
			anim.SetBool("wasWalkingFwdL", false);

			//Toggle Inactive MovementType Booleans off
			anim.SetBool("wasWalkingFwd", false);
			anim.SetBool("wasWalkingFwdR", false);
			anim.SetBool("wasWalkingBckwd", false);
			anim.SetBool("wasWalkingRight", false);
			anim.SetBool("wasWalkingLeft", false);
			anim.SetBool("wasWalkingBckwdR", false);
			anim.SetBool("wasWalkingBckwdL", false);

			//BACK TO IDLE
			if(deltaInputY < 0 & deltaInputX > 0)
			{
				anim.SetBool("isWalkingFwdL", false);
				anim.SetBool("wasWalkingFwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}


			//WALKING FWD
				//Letting go of WALK LEFT to WALK FWD
			if(deltaInputX > 0 & currentInputXValue > -1f & currentInputYValue == 1f)
			{
				anim.SetBool("isWalkingFwdL", false);
				anim.SetBool("wasWalkingFwdL", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingFwd;
			}


			//WALKING LEFT
				//Letting go of WALK FWD to WALK LEFT
			if(deltaInputY < 0 & currentInputYValue < 1f & currentInputXValue == -1f)
			{
				anim.SetBool("isWalkingFwdL", false);
				anim.SetBool("wasWalkingFwdL", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingLeft;
			}
				

			//RUNNING FWD LEFT
				//Holding WALK LEFT AND RUN FORWARD
			if(Input.GetKey(KeyCode.LeftShift) & currentInputXValue == -1f & currentInputYValue > 0f)
			{
				anim.SetBool("isWalkingFwdL", false);
				anim.SetBool("wasWalkingFwdL", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningFwdLeft;
			}

			break;
			#endregion

		case MovementType.WalkingBckwd:
		#region WALKING BACKWARD
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, -1f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 0f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingBckwd", true);
			anim.SetBool("wasWalkingBckwd", false);

			//Toggle Inactive MovementType Booleans off
			anim.SetBool("wasWalkingFwd", false);
			anim.SetBool("wasWalkingFwdR", false);
			anim.SetBool("wasWalkingFwdL", false);
			anim.SetBool("wasWalkingRight", false);
			anim.SetBool("wasWalkingLeft", false);
			anim.SetBool("wasWalkingBckwdR", false);
			anim.SetBool("wasWalkingBckwdL", false);

			//BACK TO IDLE
			if(Input.GetAxis ("Vertical") == 0f & Input.GetAxis ("Horizontal") == 0f)
			{
				anim.SetBool("isWalkingBckwd", false);
				anim.SetBool("wasWalkingBckwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}


			//WALKING BCKWD RIGHT
				//Holding WALK BCKWD AND WALK RIGHT
			if(deltaInputX > 0 & currentInputXValue > 0 & currentInputYValue == -1f)
			{
				anim.SetBool("isWalkingBckwd", false);
				anim.SetBool("wasWalkingBckwd", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwdRight;
			}

			//WALKING BCKWD LEFT
				//Holding WALK BCKWD AND WALK LEFT
			if(deltaInputX < 0 & currentInputXValue < 0 & currentInputYValue == -1f)
			{
				anim.SetBool("isWalkingBckwd", false);
				anim.SetBool("wasWalkingBckwd", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwdLeft;
			}


			//RUNNING BACKWARD
			if(Input.GetKey(KeyCode.LeftShift) & currentInputYValue == -1f & currentInputXValue == 0)
			{
				anim.SetBool("isWalkingBckwd", false);
				anim.SetBool("wasWalkingBckwd", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningBckwd;
			}

			break;
			#endregion




		case MovementType.WalkingBckwdRight:
		#region WALKING BCKWD RIGHT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, -1f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 1f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingBckwdR", true);
			anim.SetBool("wasWalkingBckwdR", false);

			//Toggle Inactive MovementType Booleans off
			anim.SetBool("wasWalkingFwd", false);
			anim.SetBool("wasWalkingFwdR", false);
			anim.SetBool("wasWalkingFwdL", false);
			anim.SetBool("wasWalkingRight", false);
			anim.SetBool("wasWalkingLeft", false);
			anim.SetBool("wasWalkingBckwd", false);
			anim.SetBool("wasWalkingBckwdL", false);

			//BACK TO IDLE

			if(currentInputYValue <= 0 & currentInputYValue > -0.5f & deltaInputY >= 0 & currentInputXValue == 0)
			{
				anim.SetBool("isWalkingBckwdR", false);
				anim.SetBool("wasWalkingBckwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}


			//WALKING BCKWD
				//Letting go of WALK RIGHT to WALK BACKWARD
			if(deltaInputX < 0 & currentInputYValue == -1f)
			{
				anim.SetBool("isWalkingBckwdR", false);
				anim.SetBool("wasWalkingBckwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwd;
			}


			//WALKING RIGHT
				//Letting go of WALK BCKWD to WALK RIGHT
			if(deltaInputY > 0 & currentInputXValue == 1f)
			{
				anim.SetBool("isWalkingBckwdR", false);
				anim.SetBool("wasWalkingBckwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingRight;
			}

			if(currentInputYValue == 0 & currentInputXValue == 1f)
			{
				anim.SetBool("isWalkingBckwdR", false);
				anim.SetBool("wasWalkingBckwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingRight;
			}

			//RUNNING

			//RUNNING BACKWD RIGHT
				//Holding WALK BACKWARD RIGHT and SHIFT TO RUN BCKWD RIGHT
			if(Input.GetKey(KeyCode.LeftShift) & currentInputXValue == 1f & currentInputYValue < 0)
			{
				anim.SetBool("isWalkingBckwdR", false);
				anim.SetBool("wasWalkingBckwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningBckwdRight;
			}

		break;
		#endregion

		case MovementType.WalkingBckwdLeft:
		#region WALKING BCKWD LEFT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, -1f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, -1f, XSpeedFactor*Time.deltaTime);


			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("isMoving", true);
			anim.SetBool("isWalkingBckwdL", true);
			anim.SetBool("wasWalkingBckwdL", false);

			//Toggle Inactive MovementType Booleans off
			anim.SetBool("wasWalkingFwd", false);
			anim.SetBool("wasWalkingFwdR", false);
			anim.SetBool("wasWalkingFwdL", false);
			anim.SetBool("wasWalkingRight", false);
			anim.SetBool("wasWalkingLeft", false);
			anim.SetBool("wasWalkingBckwd", false);
			anim.SetBool("wasWalkingBckwdR", false);

			//BACK TO IDLE
			if(deltaInputY > 0 & deltaInputX > 0)
			{
				anim.SetBool("isWalkingBckwdL", false);
				anim.SetBool("wasWalkingBckwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				movementType = MovementType.Idle;
			}


			//WALKING BCKWD
				//Letting go of WALK LEFT to WALK BACKWARD
			if(deltaInputX > 0 & currentInputYValue == -1f)
			{
				anim.SetBool("isWalkingBckwdL", false);
				anim.SetBool("wasWalkingBckwdL", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingBckwd;
			}


			//WALKING LEFT
				//Letting go of WALK BCKWD to WALK LEFT
			if(deltaInputY > 0 & currentInputXValue == -1f)
			{
				anim.SetBool("isWalkingBckwdL", false);
				anim.SetBool("wasWalkingBckwdL", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingLeft;
			}

			if(currentInputYValue == 0 & currentInputXValue == -1f)
			{
				anim.SetBool("isWalkingBckwdR", false);
				anim.SetBool("wasWalkingBckwdR", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.WalkingRight;
			}
			//RUNNING BCKWD LEFT
				//HOLDING SHIFT WHILE WALKING BCKWD LEFT
			if(Input.GetKey(KeyCode.LeftShift) & currentInputXValue == -1f & currentInputYValue < 0)
			{
				anim.SetBool("isWalkingBckwdL", false);
				anim.SetBool("wasWalkingBckwdL", true);
				anim.SetBool("wasMoving", true);
				movementType = MovementType.RunningBckwdLeft;
			}

		break;
		#endregion
		#endregion



		#region RUNNING
		case MovementType.RunningFwd:
		#region RUNNING FWD
			//Define deltaInputX and deltaInputY
				defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 2f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 0f, XSpeedFactor*Time.deltaTime);
		
			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningFwd", true);
			anim.SetBool("wasRunningFwd", false);

			//BACK TO WALKING FWD
			if(currentInputYValue == 1f & !Input.GetKey(KeyCode.LeftShift))
			{
				movementType = MovementType.WalkingFwd;
			}

			//BACK TO IDLE
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputY < 0 & currentInputYValue > 0 & currentInputXValue == 0)
			{
				anim.SetBool("wasRunningFwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningFwd", false);
				movementType = MovementType.Idle;
			}

			//BACK TO IDLE via Running Bckwd
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputY < 0 & currentInputYValue < 0 & currentInputXValue == 0)
			{
				anim.SetBool("wasRunningFwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningFwd", false);
				movementType = MovementType.Idle;
			}

			//RUNNING FWD RIGHT
			if(currentInputYValue == 1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputXValue > 0)
			{
				anim.SetBool("wasRunningFwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwd", false);
				movementType = MovementType.RunningFwdRight;
			}

			//RUNNING FWD LEFT
			if(currentInputYValue == 1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputXValue < 0)
			{
				anim.SetBool("wasRunningFwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwd", false);
				movementType = MovementType.RunningFwdLeft;
			}
		break;
		#endregion

		case MovementType.RunningRight:
		#region RUNNING RIGHT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 0f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 2f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningRight", true);
			anim.SetBool("wasRunningRight", false);

			//BACK TO WALKING RIGHT
			if(currentInputXValue == 1f & !Input.GetKey(KeyCode.LeftShift))
			{
				movementType = MovementType.WalkingRight;
			}

			//BACK TO IDLE
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputXValue > 0 & currentInputYValue == 0)
			{
				anim.SetBool("wasRunningRight", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningRight", false);
				movementType = MovementType.Idle;
			}

			//BACK TO IDLE via Running Left
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputXValue < 0 & currentInputYValue == 0)
			{
				anim.SetBool("wasRunningRight", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningRight", false);
				movementType = MovementType.Idle;
			}

			//RUNNING FWD RIGHT
			if(currentInputXValue == 1f & Input.GetKey(KeyCode.LeftShift) & deltaInputY > 0 & currentInputYValue > 0)
			{
				anim.SetBool("wasRunningRight", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningRight", false);
				movementType = MovementType.RunningFwdRight;
			}

			//RUNNING BCKWD RIGHT
			if(currentInputXValue == 1f & Input.GetKey(KeyCode.LeftShift) & deltaInputY < 0 & currentInputYValue < 0)
			{
				anim.SetBool("wasRunningRight", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningRight", false);
				movementType = MovementType.RunningBckwdRight;
			}
		break;
		#endregion

		case MovementType.RunningLeft:
		#region RUNNING LEFT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 0f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, -2f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningLeft", true);
			anim.SetBool("wasRunningLeft", false);

			//BACK TO WALKING Left
			if(currentInputXValue == -1f & !Input.GetKey(KeyCode.LeftShift))
			{
				movementType = MovementType.WalkingLeft;
			}

			//BACK TO IDLE
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputXValue < 0 & currentInputYValue == 0)
			{
				anim.SetBool("wasRunningLeft", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningLeft", false);
				movementType = MovementType.Idle;
			}

			//BACK TO Walking Left via Running Right
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputXValue > 0 & currentInputYValue == 0)
			{
				anim.SetBool("wasRunningLeft", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningLeft", false);
				movementType = MovementType.WalkingLeft;
			}

			//RUNNING FWD LEFT
			if(currentInputXValue == -1f & Input.GetKey(KeyCode.LeftShift) & deltaInputY > 0 & currentInputYValue > 0)
			{
				anim.SetBool("wasRunningLeft", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningLeft", false);
				movementType = MovementType.RunningFwdLeft;
			}

			//RUNNING BCKWD LEFT
			if(currentInputXValue == -1f & Input.GetKey(KeyCode.LeftShift) & deltaInputY < 0 & currentInputYValue < 0)
			{
				anim.SetBool("wasRunningLeft", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningLeft", false);
				movementType = MovementType.RunningBckwdLeft;
			}
		break;
		#endregion

		case MovementType.RunningBckwd:
		#region RUNNING BACKWARD
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, -2f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 0f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningBckwd", true);
			anim.SetBool("wasRunningBckwd", false);

			//BACK TO WALKING BCKWD
			if(currentInputYValue == -1f & !Input.GetKey(KeyCode.LeftShift))
			{
				movementType = MovementType.WalkingBckwd;
			}

			//BACK TO IDLE
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputY > 0 & currentInputYValue < 0 & currentInputXValue == 0)
			{
				anim.SetBool("wasRunningBckwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningBckwd", false);
				movementType = MovementType.Idle;
			}

			//BACK TO IDLE via Running Forward
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputY > 0 & currentInputYValue > 0 & currentInputXValue == 0)
			{
				anim.SetBool("wasRunningBckwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningBckwd", false);
				movementType = MovementType.Idle;
			}

			//RUNNING BCKWD RIGHT
			if(currentInputYValue == -1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputXValue > 0)
			{
				anim.SetBool("wasRunningBckwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwd", false);
				movementType = MovementType.RunningBckwdRight;
			}

			//RUNNING BCKWD LEFT
			if(currentInputYValue == -1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputXValue < 0)
			{
				anim.SetBool("wasRunningBckwd", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwd", false);	
				movementType = MovementType.RunningBckwdLeft;
			}

		break;
		#endregion

		case MovementType.RunningFwdRight:
		#region RUNNING FWD RIGHT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 2f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 2f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningFwdR", true);
			anim.SetBool("wasRunningFwdR", false);

			//BACK TO WALKING FWD RIGHT
			if(currentInputYValue == 1f & currentInputXValue == 1f & !Input.GetKey(KeyCode.LeftShift))
			{
				movementType = MovementType.WalkingFwdRight;
			}

			//BACK TO IDLE (Via WALKING FWD RIGHT)
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputY < 0 & currentInputYValue > 0 & currentInputXValue > 0 & deltaInputX < 0)
			{
				anim.SetBool("wasRunningFwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwdR", false);
				movementType = MovementType.WalkingFwdRight;
			
			}

			//To RUNNING FWD LEFT via RUNNING FWD
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputYValue == 1f & currentInputXValue < 0)
			{
				anim.SetBool("wasRunningFwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningFwdR", false);
				movementType = MovementType.RunningFwd;
			}

			//RUNNING FWD
			if(currentInputYValue == 1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputXValue > 0)
			{
				anim.SetBool("wasRunningFwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwdR", false);
				movementType = MovementType.RunningFwd;
			}

			//RUNNING RIGHT
			if(currentInputYValue > 0 & deltaInputY < 0 & Input.GetKey(KeyCode.LeftShift) & currentInputXValue == 1f)
			{
				anim.SetBool("wasRunningFwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwdR", false);
				movementType = MovementType.RunningRight;
			}

		break;
		#endregion

		case MovementType.RunningFwdLeft:
		#region RUNNING FWD LEFT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, 2f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, -2f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningFwdL", true);
			anim.SetBool("wasRunningFwdL", false);

			//BACK TO WALKING FWD LEFT
			if(currentInputYValue == 1f & currentInputXValue == -1f & !Input.GetKey(KeyCode.LeftShift))
			{
				anim.SetBool("wasRunningFwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwdL", false);
				movementType = MovementType.WalkingFwdLeft;
			}

			//BACK TO IDLE (Via WALKING FWD LEFT)
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputY < 0 & currentInputYValue > 0 & currentInputXValue < 0 & deltaInputX > 0)
			{
				anim.SetBool("wasRunningFwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningFwdL", false);
				movementType = MovementType.WalkingFwdLeft;

			}

			//RUNNING FWD
			if(currentInputYValue == 1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputXValue < 0)
			{
				anim.SetBool("wasRunningFwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwdL", false);
				movementType = MovementType.RunningFwd;
			}

			//RUNNING LEFT
			if(currentInputYValue > 0 & deltaInputY < 0 & Input.GetKey(KeyCode.LeftShift) & currentInputXValue == -1f)
			{
				anim.SetBool("wasRunningFwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwdL", false);
				movementType = MovementType.RunningLeft;
			}

			//To RUNNING FWD RIGHT via RUNNING FWD
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputYValue == 1f & currentInputXValue > 0)
			{
				anim.SetBool("wasRunningFwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningFwdL", false);
				movementType = MovementType.RunningFwd;
			}

		break;
		#endregion

		case MovementType.RunningBckwdRight:
		#region RUNNING BCKWD RIGHT
			//Define deltaInputX and deltaInputY
			defineDeltaInputs();

			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, -2f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, 2f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningBckwdR", true);
			anim.SetBool("wasRunningBckwdR", false);

			//BACK TO WALKING BCKWD RIGHT
			if(currentInputYValue == -1f & currentInputXValue == 1f & !Input.GetKey(KeyCode.LeftShift))
			{
				movementType = MovementType.WalkingBckwdRight;
			}

			//BACK TO IDLE (Via WALKING BCKWD RIGHT)
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputY > 0 & currentInputYValue < 0 & currentInputXValue > 0 & deltaInputX < 0)
			{
				anim.SetBool("wasRunningBckwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwdR", false);
				movementType = MovementType.WalkingBckwdRight;

			}

			//To RUNNING BCKWD LEFT via RUNNING BCKWD
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputYValue == -1f & currentInputXValue < 0)
			{
				anim.SetBool("wasRunningBckwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", false);
				anim.SetBool("isRunningBckwdR", false);
				movementType = MovementType.RunningBckwd;
			}

			//RUNNING BCKWD
			if(currentInputYValue == -1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX < 0 & currentInputXValue > 0)
			{
				anim.SetBool("wasRunningBckwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwdR", false);
				movementType = MovementType.RunningBckwd;;
			}

			//RUNNING RIGHT
			if(currentInputYValue < 0 & deltaInputY > 0 & Input.GetKey(KeyCode.LeftShift) & currentInputXValue == 1f)
			{
				anim.SetBool("wasRunningBckwdR", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwdR", false);
				movementType = MovementType.RunningRight;
			}
		break;
		#endregion

		case MovementType.RunningBckwdLeft:
		#region RUNNING BCKWD LEFT
			//Define deltaInputX and deltaInputY


			YWalkSpeed = anim.GetFloat("ySpeed");
			XWalkSpeed = anim.GetFloat("xSpeed");
			YWalkSpeed = Mathf.Lerp(YWalkSpeed, -2f, YSpeedFactor*Time.deltaTime);
			XWalkSpeed = Mathf.Lerp(XWalkSpeed, -2f, XSpeedFactor*Time.deltaTime);

			anim.SetFloat("ySpeed", YWalkSpeed);
			anim.SetFloat("xSpeed", XWalkSpeed);
			anim.SetBool("wasIdle", false);
			anim.SetBool("isRunningBckwdL", true);
			anim.SetBool("wasRunningBckwdL", false);

			//BACK TO WALKING BCKWD LEFT
			if(currentInputYValue == -1f & currentInputXValue == 1f & !Input.GetKey(KeyCode.LeftShift))
			{
				movementType = MovementType.WalkingBckwdLeft;
			}

			//BACK TO IDLE (Via WALKING BCKWD LEFT)
			if(!Input.GetKey(KeyCode.LeftShift) & deltaInputY > 0 & currentInputYValue < 0 & currentInputXValue < 0 & deltaInputX > 0)
			{
				anim.SetBool("wasRunningBckwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwdL", false);
				movementType = MovementType.WalkingBckwdLeft;

			}

			//To RUNNING BCKWD RIGHT via RUNNING BCKWD
			if(Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputYValue == -1f & currentInputXValue > 0)
			{
				anim.SetBool("wasRunningBckwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwdL", false);
				movementType = MovementType.RunningBckwd;
			}

			//RUNNING BCKWD
			if(currentInputYValue == -1f & Input.GetKey(KeyCode.LeftShift) & deltaInputX > 0 & currentInputXValue < 0)
			{
				anim.SetBool("wasRunningBckwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwdL", false);
				movementType = MovementType.RunningBckwd;;
			}

			//RUNNING LEFT
			if(currentInputYValue < 0 & deltaInputY > 0 & Input.GetKey(KeyCode.LeftShift) & currentInputXValue == -1f)
			{
				anim.SetBool("wasRunningBckwdL", true);
				anim.SetBool("wasMoving", true);
				anim.SetBool("isMoving", true);
				anim.SetBool("isRunningBckwdL", false);
				movementType = MovementType.RunningLeft;
			}
			defineDeltaInputs();
		break;
		#endregion
		#endregion

		default:
			
		break;
		
		}
				

	#endregion

	}
		
	void defineDeltaInputs()
	{
		//Define deltaInputX and deltaInputY
		currentInputYValue = Input.GetAxis("Vertical");
		deltaInputY =  currentInputYValue - previousInputYValue;
		previousInputYValue = currentInputYValue;
		//Debug.Log(deltaInputY);

		currentInputXValue = Input.GetAxis("Horizontal");
		deltaInputX =  currentInputXValue - previousInputXValue;
		previousInputXValue = currentInputXValue;
	}

	#endregion
}

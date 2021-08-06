using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offense : MonoBehaviour {


	private int layerIndex = 0;

	private float distancePlayerToBallXZ;
	private float distancePlayerToLandingTarget;
	private float distanceBallToLandingTarget;
	private float distanceBallToGround;
	private float ballXVel;
	private float ballZVel;
	private float ballXZVel;
	private float timeUntilBallHitsGround;
	private float timeUntilBallReachesPlayerXZ;
	private Rigidbody bola;

	private float playerXVel;
	private float playerZVel;
	private float playerXZVel;

	public bool willNotReturnBall;
	public bool willReturnBall;
	public bool hasReturnedBall;
	public bool oppBallAttack;
	public bool isGoingTowardLandingTarget;
	public bool hasReachedLandingTarget;
	public bool matchTargetComplete;
	public bool ballAttack;
	public bool cambio;
	public bool scoredTeamRed;
	public bool scoredTeamBlue;


	public float adjustment = 0.175f;
	public float heightReturn = 5f;
	public float gravity = 9.8f;
	public float lookIKWeight;
	public float distancePlayerReceptionTargetToBallLandingTargetXZ;
	public AudioSource audioData;


	static Animator anim;
	public GameObject playerAttack;
	public GameObject LauncherOfBalls;
	public GameObject playerRightForeArm;
	public AvatarTarget playerRightFoot;
	public GameObject playerReceptionTarget;
	public Transform someObject;
	ColocadorControls colocadorControls;
	BallLauncher ballLauncher;

	public Transform ballLandingTarget;
	public float YSpeedFactor = 3f;
	public float XSpeedFactor = 3f;
	private float numPasser_XIdle = 0;
	private float numPasser_YIdle = 0;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		colocadorControls = playerAttack.GetComponent<ColocadorControls> ();
		ballLauncher = LauncherOfBalls.GetComponent<BallLauncher> ();
		willNotReturnBall = true;
		willReturnBall = false;
		hasReturnedBall = false;
		oppBallAttack = false;
		isGoingTowardLandingTarget = false;
		hasReachedLandingTarget = false;
		matchTargetComplete = false;
		ballAttack = false;
		cambio = false;
		scoredTeamRed = false;
		scoredTeamBlue = false;

	}

	// Update is called once per frame
	void Update () {
		Attack ();


	}



	void OnAnimatorIK(int layerIndex)
	{

				anim.SetLookAtPosition (ballLauncher.transform.position);
				anim.SetLookAtWeight (1f);

	}
		


	void Attack ()
	{
		inAirPhysicsCalcs ();

		//IF BALL IS NULL
		if (ballLauncher.inGameVolleyBall == null & anim.GetBool("isUnderCarryServe") == false & anim.GetBool("isIdle") == false) 
		{
			if(Input.GetMouseButtonDown(0))
			{
				anim.SetBool ("isDigging", true);
				anim.SetTrigger("isDiggingTrig");
			}

			if( !Input.GetMouseButton(0) & anim.GetCurrentAnimatorStateInfo(1).IsName("pullBackRightLower") 
				& anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 1f)
			{
				Invoke ("AnimationManager",  0f);
			}
		}

		//IF BALL IS NOT NULL
		if (ballLauncher.inGameVolleyBall != null) 
		{
			if(Input.GetMouseButtonDown(0))
			{
				anim.SetBool ("isDigging", true);
				anim.SetTrigger("isDiggingTrig");
				Debug.Log (timeUntilBallHitsGround);
				Debug.Log (timeUntilBallReachesPlayerXZ);
			}

			if( !Input.GetMouseButton(0) & anim.GetCurrentAnimatorStateInfo(1).IsName("pullBackRightLower") 
				& anim.GetCurrentAnimatorStateInfo(1).normalizedTime > .2f)
			{
				Invoke ("AnimationManager",  timeUntilBallReachesPlayerXZ - 0.3f);
			}

			if (willReturnBall == true) 
			{
				if (ballLauncher.inGameVolleyBall.transform.position.z - playerAttack.transform.position.z < 2f) 
				{
					//BALL RETURN
					if (ballLauncher.inGameVolleyBall.transform.position.z - playerAttack.transform.position.z <= 0.2f
						& ballLauncher.inGameVolleyBall.transform.position.y - playerRightForeArm.transform.position.y <= 1.1f) 
					{
						LaunchBall (bola);
						audioData.PlayDelayed (0f);
						willReturnBall = false;
						hasReturnedBall = true;
					}
				}
			}

		}


			


			
		//RETURN GAMEPLAY MECHANICS (ATTACK POSITION)
			
		if(Vector3.Distance(playerReceptionTarget.transform.position, ballLandingTarget.position) < 0.25f)
			{

				//IDLE
				if (ballLandingTarget.position.x - playerReceptionTarget.transform.position.x > -0.25f) 
				{
					if (Input.GetMouseButtonUp(0) & colocadorControls.movementType == ColocadorControls.MovementType.Idle) 
					{						
						if (timeUntilBallReachesPlayerXZ < 1.2f & timeUntilBallReachesPlayerXZ > 0.3f) 
						{
							Debug.Log ("Hey Ma!");
							willReturnBall = true;
						}
					}
				}

			}
			

		if (Vector3.Distance (playerReceptionTarget.transform.position, ballLandingTarget.position) < 0.75f) 
		{

			//WALKING RIGHT
			if (ballLandingTarget.position.x - playerReceptionTarget.transform.position.x > -0.25f) 
			{
				if (Input.GetMouseButtonUp (0) & colocadorControls.movementType == ColocadorControls.MovementType.WalkingRight) 
				{	
					if (timeUntilBallReachesPlayerXZ < 1.2f & timeUntilBallReachesPlayerXZ > 0.3f) 
					{
						Debug.Log ("Hey Ma!");
						willReturnBall = true;
					}
				}
			}

		}


		if (Vector3.Distance (playerReceptionTarget.transform.position, ballLandingTarget.position) < 1.5f) 
		{

			//RUNNING RIGHT
			if (timeUntilBallReachesPlayerXZ < 1.2f & timeUntilBallReachesPlayerXZ > 0.2f) 
			{
				if (Input.GetMouseButtonUp (0) & colocadorControls.movementType == ColocadorControls.MovementType.RunningRight) 
				{	

					if (ballLandingTarget.position.x - playerReceptionTarget.transform.position.x > -0.3f) 
					{
						Debug.Log ("Hey Ma!");
						willReturnBall = true;
					}
				}
			}

		}






	}




	Vector3 CalculateLaunchVelocity()
	{ 
		float displacementY = ballLauncher.inGameVolleyBall.transform.position.y - ballLauncher.transform.position.y;

		Vector3 displacementXZ = new Vector3 (ballLauncher.transform.position.x - ballLauncher.inGameVolleyBall.transform.position.x, 0, ballLauncher.transform.position.z - ballLauncher.inGameVolleyBall.transform.position.z);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (2 * gravity *Mathf.Abs( (heightReturn-displacementY)));
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt (2 * heightReturn / gravity) + Mathf.Sqrt (2 * Mathf.Abs( (heightReturn - displacementY- adjustment)) / gravity));

		return velocityXZ + velocityY;

	}


	void LaunchBall (Rigidbody balon)
	{
		balon.velocity = CalculateLaunchVelocity ();
	}



	float CalculateDistances()
	{
		float distancePlayerToBallX =  playerAttack.transform.position.x - ballLauncher.inGameVolleyBall.transform.position.x;
		float distancePlayerToBallZ =  playerAttack.transform.position.z - ballLauncher.inGameVolleyBall.transform.position.z;
		distancePlayerToBallXZ = Mathf.Sqrt (distancePlayerToBallX * distancePlayerToBallX + distancePlayerToBallZ * distancePlayerToBallZ);

		distanceBallToLandingTarget = Vector3.Distance (ballLauncher.inGameVolleyBall.transform.position, ballLauncher.attackingPosition.transform.position);
		distancePlayerToLandingTarget = Vector3.Distance (playerAttack.transform.position, ballLauncher.attackingPosition.transform.position);
		distanceBallToGround = ballLauncher.inGameVolleyBall.transform.position.y;

		return distancePlayerToBallXZ;
		return distanceBallToLandingTarget;
		return distancePlayerToLandingTarget;
		return distanceBallToGround;
	}



	void inAirPhysicsCalcs ()
	{
		if (ballLauncher.inGameVolleyBall != null) 
		{

			bola = ballLauncher.inGameVolleyBall.gameObject.GetComponent<Rigidbody> ();
			ballXVel = bola.velocity.x;
			ballZVel = bola.velocity.z;
			ballXZVel = Mathf.Sqrt (ballXVel * ballXVel + ballZVel * ballZVel);
			CalculateDistances ();
			timeUntilBallHitsGround = distanceBallToLandingTarget / ballXZVel;
			timeUntilBallReachesPlayerXZ = distancePlayerToBallXZ / ballXZVel;
			float distancePlayerReceptionTargetToBallLandingTargetX = playerReceptionTarget.transform.position.x - ballLauncher.attackingPosition.transform.position.x;
			float distancePlayerReceptionTargetToBallLandingTargetZ = playerReceptionTarget.transform.position.z - ballLauncher.attackingPosition.transform.position.z;
			distancePlayerReceptionTargetToBallLandingTargetXZ = Mathf.Sqrt (distancePlayerReceptionTargetToBallLandingTargetX * distancePlayerReceptionTargetToBallLandingTargetX
				+ distancePlayerReceptionTargetToBallLandingTargetZ * distancePlayerReceptionTargetToBallLandingTargetZ);


			//COLOR BALL SHADOW
			if (ballLauncher.inGameVolleyBall != null) 
			{
				if (timeUntilBallHitsGround < 1.43f & timeUntilBallHitsGround > 0.53f) 
				{
					GameObject.FindGameObjectWithTag ("ballShadow").GetComponent<SpriteRenderer> ().color = new Color (0.9059f, 0.2667f, 0.0784f, 1f);
				} else 
				{
					GameObject.FindGameObjectWithTag ("ballShadow").GetComponent<SpriteRenderer> ().color = new Color (0.4941f, 0.9098f, 0.1529f, 1f);
				}
			}

//			if (ballLauncher.inGameVolleyBall.transform.position.z - playerAttack.transform.position.z > -0.5f) 
//			{
//				
//				if (timeUntilBallReachesPlayerXZ < 1.35f & timeUntilBallReachesPlayerXZ > 0.6f) 
//				{
//					GameObject.FindGameObjectWithTag ("ballShadow").GetComponent<SpriteRenderer> ().color = new Color (0.9059f, 0.2667f, 0.0784f, 1f);
//
//					if (distancePlayerToLandingTarget < 1f & Input.GetMouseButtonDown (0)) 
//					{
//						Invoke ("AnimationManager", timeUntilBallReachesPlayerXZ - 0.6f);
//						willNotReturnBall = false;
//						willReturnBall = true;
//
//					}
//				} else{
//					GameObject.FindGameObjectWithTag ("ballShadow").GetComponent<SpriteRenderer> ().color = new Color (0.4941f, 0.9098f, 0.1529f, 1f);
//				}
//
//
//				if (willReturnBall == true) 
//				{
//
					
//				}
//
//
//				if (hasReturnedBall == true & ballLauncher.inGameVolleyBall.transform.position.z > 0f) 
//				{
//					willNotReturnBall = true;
//					hasReturnedBall = false;
//				}
//
//			} else {
//				anim.ResetTrigger ("Return");
//				anim.SetBool ("isDigging", false);		
//			}
//
//
//			}
		}
	}










	void AnimationManager()
	{
		anim.SetBool ("isDigging", false);
		//Debug.Log (Vector3.Distance (playerAttack.transform.position, ballLauncher.inGameVolleyBall.transform.position));
		//Debug.Log (timeUntilBallReachesPlayerXZ);
	}







}

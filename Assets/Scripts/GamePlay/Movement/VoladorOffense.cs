using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoladorOffense : MonoBehaviour {

	public GameObject volador;
	public GameObject playerAttack;
	public GameObject volleyball;
	public SphereCollider ball;
	public GameObject brazoDefender;
	public float animPlayerTime = 1.4f;
	public float launchdelayTime = 0f;
	public GameObject receivingPlayer;
	static Animator anim;
	private Transform voladorPos; 
	private Transform ballPos;

	public Transform attackerTarget;
	public Transform defenderTarget;
	public Transform balonInicial;
	public float gravity = 9.8f;
	public float heightReturn = 5f;
	public float adjustment = 0.175f;
	private bool collided;
	VoladorServe voladorServe;
	public AudioSource audioData;
	public bool wait = true;
	public bool ballReceived = false;
	public bool ballReturned = false;
	public bool animStage = false;

	// Use this for initialization
	void Start () 
	{
		voladorServe = playerAttack.GetComponent<VoladorServe> ();

		anim = GetComponent<Animator> ();
		voladorPos = volador.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		animPlayerTime = -0.00256f*(Mathf.Pow(voladorServe.h, 2f)) + 0.2109f*(voladorServe.h) + 0.38888f;

		if (balonInicial != null) 
		{
			if ((balonInicial.position.y - brazoDefender.transform.position.y) <= 0.15 & wait == false) 
			{
				StartCoroutine (ReturnBall ());
				wait = true;
			}
		}

		WaitStage ();
		ResetStage ();
		BallReceivedStage ();		
		AnimStageToBallReceived ();
		ReceptionAnimation ();

	}
		

	IEnumerator AnimatePlayer()
	{
		

		yield return new WaitForSeconds (animPlayerTime);
		anim.SetBool ("IsIdle", false);
		anim.SetBool ("RaqOffBothMidR", true);

	}

	IEnumerator ReturnBall()
	{
		yield return new WaitForSeconds (launchdelayTime);
		audioData.PlayDelayed (0f);
		Launch (balonInicial.GetComponent<Rigidbody> ());
		//ballReturned = true;
	}

	public void RaquetaReceiveAnimation(Transform volleyBall)
	{
		Vector3 attackerTargetPos = attackerTarget.transform.position;
		Vector3 ReceiveDistance = attackerTarget.transform.position - receivingPlayer.transform.position;
		Debug.Log (receivingPlayer.transform.position);
		Debug.Log (attackerTargetPos);
		Debug.Log (ReceiveDistance);

		if (ReceiveDistance.x < -0.05 & ReceiveDistance.x > -0.55f & ReceiveDistance.y > 0.95f & ReceiveDistance.z < 0.85f & ReceiveDistance.z > 0.35f & anim.GetBool("RaqOffBothMidR") == false) 
			{
			collided = true;
			animStage = true;
			wait = false;
			StartCoroutine (AnimatePlayer ());
			}
	}
		
	void Launch(Rigidbody bola)
	{
		//Debug.Log (bola);
		Physics.gravity = Vector3.up * -gravity;
		//balon.useGravity = true;
		bola.velocity = CalculateLaunchVelocity();
		//print (CalculateLaunchVelocity ());
	}

	Vector3 CalculateLaunchVelocity()
	{
		//target.position = new Vector3 ((mouseXpos - 768f)/27.778f, 0.001, (mouseYpos-232f)/27.778f); 
		float displacementY = balonInicial.position.y - defenderTarget.position.y;

		Vector3 displacementXZ = new Vector3 (defenderTarget.position.x - balonInicial.position.x, 0, defenderTarget.position.z - balonInicial.position.z);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (2 * gravity *Mathf.Abs( (heightReturn-displacementY)));
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt (2 * heightReturn / gravity) + Mathf.Sqrt (2 * Mathf.Abs( (heightReturn - displacementY- adjustment)) / gravity));

		return velocityXZ + velocityY;

	}
		

	public void ReceptionAnimation()
	{
		if (ballReceived == false & animStage == false & balonInicial == null) 
		{
			GameObject balonPre = GameObject.FindGameObjectWithTag ("volleyBall");
			if (balonPre != null) 
			{
				balonInicial = balonPre.transform;
				RaquetaReceiveAnimation (balonInicial);
			}
		}
	}

	public void AnimStageToBallReceived()
	{
		if (animStage == true) 
		{
			ballReceived = true;
		}
	}

	public void BallReceivedStage()
	{
		if (ballReceived == true) 
		{
			animStage = false;
			anim.SetBool ("IsIdle", true);
			anim.SetBool ("RaqOffBothMidR", false);
		}
	}

	public void ResetStage()
	{
		if (ballReceived == true & anim.GetBool("RaqOffBothMidR") == true) 
		{
			ballReceived = false;
			anim.SetBool ("IsIdle", true);
			anim.SetBool ("RaqOffBothMidR", false);
		}
	}

	public void WaitStage()
	{
		if(balonInicial == null)
		{
			wait = true;
		}
	}

}

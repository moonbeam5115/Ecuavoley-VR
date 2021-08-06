using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoladorServeLogic : MonoBehaviour {

	public Rigidbody ballRigidBod_2;
	public Rigidbody ballRigidBod_3;

	static Animator anim;
	public GameObject ballPrefab;
	public GameObject manoDerecha;
	public Rigidbody ballRigidBod_1;
	public AudioSource audioData;
	public GameObject ServeSound;

	public Transform balonInicial;
	public GameObject volador;
	public Transform target;
	public float gravity = 9.8f;
	public float h = 5f;

	public GameObject brazoDerecho;
	public float ballxPosAdjustment = 0.085f;
	public float ballyPosAdjustment = -2.8f;

	public float mouseXPos;
	public float mouseYPos;

	public float mouseAdjust_Beta1 = 225.5f;
	public float mouseAdjust_Beta2 = 52f;
	public float mouseAdjust_gamma1 = 22.333f;
	public float mouseAdjust_gamma2 = 22.222f;

	public ScoreBoardLogic ScoreScriptBC;
	public ScoreBoardLogic ScoreScriptFC;
	public GameObject BackCourt;
	public GameObject FrontCourt;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audioData = ServeSound.GetComponent<AudioSource> ();
		balonInicial = volador.transform;
		ScoreScriptBC = BackCourt.GetComponent<ScoreBoardLogic> ();
		ScoreScriptFC = FrontCourt.GetComponent<ScoreBoardLogic> ();
	}
		

	// Update is called once per frame
	void Update () {
		EnterServeMode ();
		WhileInServeMode ();


	}


	IEnumerator BallAppear()
	{
		yield return new WaitForSeconds(1f);
		GameObject newBall = Instantiate(ballPrefab,new Vector3(manoDerecha.transform.position.x + 0.03f, manoDerecha.transform.position.y + 0.104f, manoDerecha.transform.position.z + 0.14f), Quaternion.identity);
		ballRigidBod_1 = newBall.GetComponent<Rigidbody> ();
		ballRigidBod_1.useGravity = false;
		newBall.tag = "firstVolleyball";
	}

	IEnumerator TossBallInAir()
	{
		//print(Time.time);
		yield return new WaitForSeconds(1f);
		GameObject obj2= Instantiate(ballPrefab,new Vector3(manoDerecha.transform.position.x + 0.03f, manoDerecha.transform.position.y + 0.114f, manoDerecha.transform.position.z + 0.14f), Quaternion.identity);
		ballRigidBod_2 = obj2.GetComponent<Rigidbody> ();
		obj2.tag = "volleyBall2";
		preLaunch (ballRigidBod_2);
	}

	IEnumerator ServeBall()
	{
		print(Time.time);
		yield return new WaitForSeconds(2.23f);
		GameObject obj= Instantiate(ballPrefab,new Vector3(brazoDerecho.transform.position.x+0.07f, brazoDerecho.transform.position.y + 0.1f, brazoDerecho.transform.position.z+0.2f), Quaternion.identity);
		ballRigidBod_3 = obj.GetComponent<Rigidbody> ();
		obj.tag = "servedVolleyball";
		Launch (ballRigidBod_3);
		//receiveStateCheck = true;
	}



	void EnterServeMode()
	{
		if (Input.GetKeyDown (KeyCode.B)) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isUnderCarry", true);
			ScoreScriptBC.ShouldStop = false;
			ScoreScriptFC.ShouldStop = false;
			StartCoroutine (BallAppear());
		}
	}
		
	void WhileInServeMode()
	{
		if (ballRigidBod_1 != null) 
		{
			GameObject FirstVolleyball = GameObject.FindGameObjectWithTag ("firstVolleyball");
			FirstVolleyball.transform.position = new Vector3 (manoDerecha.transform.position.x + 0.03f, manoDerecha.transform.position.y + 0.104f, manoDerecha.transform.position.z + 0.14f);
		}


		if (GUI_ServePosAnim.stage1) 
		{
			mouseXPos = Input.mousePosition.x;
			mouseYPos = Input.mousePosition.y;
			target.position = new Vector3 ((-4.5f + (mouseXPos-125f)*(9f/201f)), 0.01f, (0f + (mouseYPos-50f)*(9f/202f)));
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			anim.SetBool ("isServing", true);
			anim.SetBool ("isUnderCarry", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("RaquetaDefenseReady", true);
			Debug.Log ("startServe");
			StartCoroutine (ServeBall ());
			StartCoroutine(TossBallInAir());
			//serveStateCheck = true;
			Destroy(GameObject.FindGameObjectWithTag ("firstVolleyball"), 1.02f);
			audioData.PlayDelayed (2f);
		}
			

	}


	Vector3 CalculateLaunchVelocity()
	{
		//target.position = new Vector3 ((mouseXPos - 768f)/27.778f, 0.001f, (mouseYPos-232f)/27.778f); 
		float displacementY = 1.2f - target.position.y;
		Vector3 displacementXZ = new Vector3 ((target.position.x - balonInicial.position.x)-.2f, 0, (target.position.z - balonInicial.position.z)-.1f);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (2 * gravity * (h-displacementY));
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt (2 * h / gravity) + Mathf.Sqrt (2 * (h - displacementY) / gravity));

		return velocityXZ + velocityY;

	}

	void Launch(Rigidbody balon)
	{
		Physics.gravity = Vector3.up * -gravity;
		//balon.useGravity = true;
		balon.velocity = CalculateLaunchVelocity ();
		print (CalculateLaunchVelocity ());
	}

	void preLaunch(Rigidbody balon)
	{
		Physics.gravity = Vector3.up * -gravity;
		balon.useGravity = true;
		balon.velocity = new Vector3 (-0.075f, 6.05f, 0f);
		print (balon.velocity);
		GameObject test2 = GameObject.FindGameObjectWithTag ("volleyBall2");
		Destroy (balon, 1.212f);
		Destroy (test2, 1.212f);
	}



}

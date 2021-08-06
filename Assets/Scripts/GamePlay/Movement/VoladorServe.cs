using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VoladorServe : MonoBehaviour {

	public Rigidbody balon;
	public Rigidbody balon2;
	public Transform balonInicial;
	public Transform target;
	public GameObject prefab;
	public GameObject volador;
	public GameObject brazoDerecho;
	public GameObject manoDerecha;
	public float ballxPosAdjustment = 0.085f;
	public float ballyPosAdjustment = -2.8f;
	private Rigidbody objeto2;

	public bool serveStateCheck = false;
	public bool receiveStateCheck = false;

	public float gravity = 9.8f;
	public float h = 5f;

	static Animator anim;

	public AudioSource audioData;

	public float mouseXPos;
	public float mouseYPos;

	public float mouseAdjust_Beta1 = 225.5f;
	public float mouseAdjust_Beta2 = 52f;
	public float mouseAdjust_gamma1 = 22.333f;
	public float mouseAdjust_gamma2 = 22.222f;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
	}

	public void Awake () {
		

	
	}

	IEnumerator Example(Rigidbody balon)
	{
		print(Time.time);
		yield return new WaitForSeconds(1.661f);
		GameObject obj= Instantiate(prefab,new Vector3(brazoDerecho.transform.position.x, brazoDerecho.transform.position.y + 0.1f, brazoDerecho.transform.position.z+0.3f), Quaternion.identity);
		balon = obj.GetComponent<Rigidbody> ();
		balon.tag = "volleyBall";
		Launch (balon);
		receiveStateCheck = true;
	}

	IEnumerator Example2(Rigidbody balon2)
	{
		//print(Time.time);
		yield return new WaitForSeconds(0.65f);
		GameObject obj2= Instantiate(prefab,new Vector3(manoDerecha.transform.position.x+ 0.03f,manoDerecha.transform.position.y + 0.18f, manoDerecha.transform.position.z+ 0.06f), Quaternion.identity);
		balon2 = obj2.GetComponent<Rigidbody> ();
		obj2.tag = "volleyBall2";
		preLaunch (balon2);
	}

	IEnumerator Example3()
	{
	yield return new WaitForSeconds(1f);
		GameObject obj2= Instantiate(prefab,new Vector3(manoDerecha.transform.position.x+ 0.03f,manoDerecha.transform.position.y + 0.18f, manoDerecha.transform.position.z+ 0.06f), Quaternion.identity);
	balon2 = obj2.GetComponent<Rigidbody> ();
	balon2.useGravity = false;
		obj2.tag = "volleyBall3";
	}

	// Update is called once per frame
	void Update () {

		ballxPosAdjustment = 0.15f * (1.5f - ((target.transform.position.x) / 4.5f) * 0.5f); 
		ballyPosAdjustment = -3.35f  + (target.transform.position.z)/9f*0.65f;

		if (GUI_ServePosAnim.stage1) 
		{
			mouseXPos = Input.mousePosition.x;
			mouseYPos = Input.mousePosition.y;
			target.position = new Vector3 ((mouseXPos - mouseAdjust_Beta1) / mouseAdjust_gamma1, 1f, (mouseYPos - mouseAdjust_Beta2) / mouseAdjust_gamma2);
		}

		GameObject test = GameObject.FindGameObjectWithTag ("volleyBall3");
		GameObject test2 = GameObject.FindGameObjectWithTag ("volleyballServeSound");

		if (test != null) 
		{
			AudioSource audioData = test2.GetComponent<AudioSource> ();
		}

		if (test != null) 
		{
			test.transform.position = new Vector3(manoDerecha.transform.position.x+ 0.03f,manoDerecha.transform.position.y + 0.18f, manoDerecha.transform.position.z+ 0.06f);
		}
		else if (test == null)
		{
			
		}

		balonInicial = volador.transform;

		if (Input.GetKeyDown (KeyCode.B)) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isUnderCarry", true);
			anim.SetBool ("RaquetaDefenseReady", false);
			StartCoroutine (Example3 ());
		}

		if (Input.GetKeyDown (KeyCode.U)) 
		{
			anim.SetBool ("RaqDefBothMidR", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isUnderCarry", false);
			anim.SetBool ("isServing", false);
			anim.SetBool ("RaqOffBothCtr", false);
			anim.SetBool ("RaqOffBothMidR", false);
			anim.SetBool ("RaqOffBothMidL", false);
			anim.SetBool ("RaquetaDefenseReady", false);
		}

		if (Input.GetKeyUp (KeyCode.U)) 
		{
			anim.SetBool ("RaquetaDefenseReady", true);
			anim.SetBool ("RaqDefBothMidR", false);
			anim.SetBool ("isIdle", true);
		}

		if (Input.GetKeyDown (KeyCode.I)) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isUnderCarry", true);

		}

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isUnderCarry", true);

		}

		if (Input.GetKeyDown (KeyCode.O)) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isUnderCarry", true);

		}

		if (Input.GetMouseButtonDown (0)) 
		{
			anim.SetBool ("isServing", true);
			anim.SetBool ("isUnderCarry", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("RaquetaDefenseReady", true);
			StartCoroutine (Example (balon));
			StartCoroutine(Example2(balon2));
			serveStateCheck = true;
			Destroy(test, 0.7f);
			audioData.PlayDelayed (1.5f);

		}

		if (Input.GetMouseButtonUp (0)) 
		{
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isServing", false);
		}
	}

	public void OnPointerClick(Rigidbody balon2)
	{
		Debug.Log("OnPointerClick called.");
	}

	Vector3 CalculateLaunchVelocity()
	{
		//target.position = new Vector3 ((mouseXpos - 768f)/27.778f, 0.001, (mouseYpos-232f)/27.778f); 
		float displacementY = balonInicial.position.y - target.position.y;
		Vector3 displacementXZ = new Vector3 ((target.position.x - balonInicial.position.x)-ballxPosAdjustment , 0, target.position.z - balonInicial.position.z);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (2 * gravity * (h-displacementY));
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt (2 * h / gravity) + Mathf.Sqrt (2 * (h - displacementY - ballyPosAdjustment) / gravity));

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
		balon.velocity = new Vector3 (0f, 5.2f, 0f);
		print (balon.velocity);
		Destroy (balon, 1.01f);
		GameObject test2 = GameObject.FindGameObjectWithTag ("volleyBall2");
		Destroy (test2, 1.01f);
	}

}




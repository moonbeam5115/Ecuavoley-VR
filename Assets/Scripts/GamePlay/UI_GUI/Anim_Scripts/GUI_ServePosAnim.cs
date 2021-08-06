using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_ServePosAnim : MonoBehaviour {
	public static bool stage1 = false;
	public static bool stage2 = false;

	public Animator anim;
	public AudioSource audioData;
	public GameObject audioHolder;
	public AudioSource audioData2;
	public GameObject audioHolder2;



	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		AudioSource audioData = audioHolder.GetComponent<AudioSource> ();
		AudioSource audioData2 = audioHolder2.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () 
	{

		step1 ();
		closeGUI ();
		Serve ();
		ServeReady ();

	}

	void step1 ()
	{
		if (stage2) 
		{
			anim.SetBool ("GUI_Standby", true);
			anim.SetBool ("GUI_Close", false);
			stage2 = false;
		}
	}

	void closeGUI ()
	{
		if (anim.GetBool ("ServeReady")) 
		{
			if (Input.GetKeyDown (KeyCode.B)) 
			{ 
				anim.SetBool ("ServeReady", false);
				anim.SetBool ("GUI_Close", true);
				audioData2.PlayDelayed (0.02f);
				stage2 = true;
				stage1 = false;
			}
		}	
	}

	void Serve()
	{
		if (anim.GetBool ("ServeReady")) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{ 
				anim.SetBool ("ServeReady", false);
				anim.SetBool ("GUI_Close", true);
				audioData2.PlayDelayed (0.02f);
				stage2 = true;
				stage1 = false;
			}
		}	
	}

	void ServeReady ()
	{
		if (anim.GetBool("GUI_Standby") )
		{

			if (Input.GetKeyDown (KeyCode.B)) 
			{ 
				anim.SetBool ("ServeReady", true);
				anim.SetBool ("GUI_Standby", false);
				anim.SetBool ("GUI_Close", false);
				audioData.PlayDelayed (0f);
				stage1 = true;
				Cursor.visible = false;
				
			}
		}
	}

}

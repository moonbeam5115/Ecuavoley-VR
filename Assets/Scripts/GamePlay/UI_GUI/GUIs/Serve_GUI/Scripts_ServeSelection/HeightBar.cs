using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightBar : MonoBehaviour {

	public GameObject playerAttack;
	VoladorServe voladorServe;
	public float currentHeight;
	public float maxHeight;
	public Slider heightSlider;
	// Use this for initialization
	void Start () {

		voladorServe = playerAttack.GetComponent<VoladorServe> ();
		currentHeight = voladorServe.h;
		maxHeight = 20f;
		heightSlider.value = currentHeight / maxHeight;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentHeight = voladorServe.h;
		if (currentHeight >= 20f) 
		{
			currentHeight = 20f;
		}

		if (currentHeight <= 0f) 
		{
			currentHeight = 0f;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") >= 0 & !Input.GetButton("Left Shift")) 
		{
			changeHeight ();	
			heightSlider.value = currentHeight / maxHeight;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0 & !Input.GetButton("Left Shift")) 
		{
			changeHeight ();
			heightSlider.value = currentHeight / maxHeight;
		}
			
	}

	void changeHeight()
	{
		voladorServe.h += Input.GetAxis ("Mouse ScrollWheel")*currentHeight;
	}		

}

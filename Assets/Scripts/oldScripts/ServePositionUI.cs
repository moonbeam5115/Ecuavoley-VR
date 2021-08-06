using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServePositionUI : MonoBehaviour {

	public static bool SelectServePosition = false;

	public GameObject ServeUI;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.O)) 
		{
			if (SelectServePosition) {
				DisableUI ();
			} 
			else 
			{
				EnableUI ();
			}
		}
	}


	void DisableUI()
	{
		ServeUI.SetActive (false);
		SelectServePosition = false;
	}

	void EnableUI()
	{
		ServeUI.SetActive (true);
		SelectServePosition = true;

	}


}

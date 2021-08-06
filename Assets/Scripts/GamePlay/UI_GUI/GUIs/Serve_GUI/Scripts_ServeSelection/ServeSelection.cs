using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeSelection : MonoBehaviour {

	public GameObject crosshair;
	public GameObject GUI_ServeDisplay;
	public float mouseXpos;
	public float mouseYpos;
	public GUI_ServePosAnim gui_ServePosAnim;

	// Use this for initialization
	void Start () 
	{
		gui_ServePosAnim = crosshair.GetComponent<GUI_ServePosAnim> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (GUI_ServePosAnim.stage1) 
		{
			mouseXpos = Input.mousePosition.x;
			mouseYpos = Input.mousePosition.y;
			if (mouseXpos >= 326f) 
			{
				mouseXpos = 326f;
			}
			if (mouseXpos <= 125f) 
			{
				mouseXpos = 125f;
			}
			if (mouseYpos >= 252f) 
			{
				mouseYpos = 252f;
			}
			if (mouseYpos <= 52f) 
			{
				mouseYpos = 50f;
			}
			crosshair.transform.position = new Vector3 (mouseXpos, mouseYpos, 0);
			//Debug.Log (mouseXpos);
			//Debug.Log (mouseYpos);
			Cursor.visible = false;
		}

		if (GUI_ServePosAnim.stage1 == false) 
		{
			//Cursor.visible = true;
		}
	}
}

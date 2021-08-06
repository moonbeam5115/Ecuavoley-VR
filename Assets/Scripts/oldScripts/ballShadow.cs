using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballShadow : MonoBehaviour {

	public Transform ballProjection;
	public Transform volleyball;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		ballProjection.position = new Vector3 (volleyball.position.x, 1f, volleyball.position.z); 
	}
}

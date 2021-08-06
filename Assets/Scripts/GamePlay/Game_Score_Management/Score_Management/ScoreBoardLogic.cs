using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardLogic : MonoBehaviour {

	public Text ScoreText;
	public int ScoreNum = 0;
	public bool ShouldStop = false;


	// Use this for initialization
	void Start () {
	

	}


	void OnCollisionEnter(Collision collision)
	{
		if (ShouldStop == false) 
		{
			ScoreNum += 1;
			ScoreText.text = ScoreNum.ToString ();
		}
		Destroy(GameObject.FindGameObjectWithTag("servedVolleyball"), 3f);
		Destroy(GameObject.FindGameObjectWithTag("ballShadow"), 3f);
		ShouldStop = true;
	}


}

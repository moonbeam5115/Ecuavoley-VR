using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour {

	public AudioSource audioData;
	public  ulong delayVariable;
	private ulong samplingRate;
	public Vector3 velocity;
	public GameObject volleyball;
	public RawImage image;
	public float temp;
	public float temp2;
	public GameObject volleyballNet;
	public GameObject pole1;
	public GameObject pole2;
	public Renderer alphaContainer;
	public Renderer alphaContainerPole1;
	public Renderer alphaContainerPole2;
	float alphaValue = 0f;
	float alphaValuePole1 = 0f;
	float alphaValuePole2 = 0f;
	float alphaValue_2 = 1f;
	float alphaValuePole1_2 = 1f;
	float alphaValuePole2_2 = 1f;

	// Use this for initialization
	void Start () {
		samplingRate = 44100;
		delayVariable = 2;
		audioData = GetComponent<AudioSource> ();
		audioData.PlayDelayed (delayVariable);
		temp = 0f;
		temp2 = 1f;
		image.color = GetComponent<RawImage> ().color;
		image.color = new Color (1, 1, 1, temp);
		volleyballNet.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, alphaValue);
		pole1.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, alphaValuePole1);
		pole2.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, alphaValuePole2);
	}
	
	// Update is called once per frame
	void Update () {
		if (temp < 1) {
			Invoke ("changeAlphaUp", 3.5f);
		}
		if (alphaValue < 1) {
			Invoke ("changeAlphaNetUp", 5.5f);
		}
		if (alphaValuePole1 < 1) {
		Invoke ("changeAlphaPole1Up", 5.5f);
		}
		if (alphaValuePole2 < 1) {
		Invoke ("changeAlphaPole2Up", 5.5f);
		}

		Invoke ("changeAlphaDown", 14f);
		Invoke ("changeAlphaNetDown", 14f);
		Invoke ("changeAlphaPole1Down", 14f);
		Invoke ("changeAlphaPole2Down", 14f);
	}

	void changeAlphaNetUp(){
		alphaValue += 0.0075f;
		if (alphaValue >= 1f) {
			alphaValue = 1f;
		}
		alphaContainer = volleyballNet.GetComponent<Renderer> ();
		alphaContainer.material.color = new Color (1, 1, 1, alphaValue);
	}

	void changeAlphaPole1Up(){
		alphaValuePole1 += 0.0075f;
		if (alphaValuePole1 >= 1f) {
			alphaValuePole1 = 1f;
		}
		alphaContainerPole1 = pole1.GetComponent<Renderer> ();
		alphaContainerPole1.material.color = new Color (1, 1, 1, alphaValuePole1);
	}

	void changeAlphaPole2Up(){
		alphaValuePole2 += 0.0075f;
		if (alphaValuePole2 >= 1f) {
			alphaValuePole2 = 1f;
		}
		alphaContainerPole2 = pole2.GetComponent<Renderer> ();
		alphaContainerPole2.material.color = new Color (1, 1, 1, alphaValuePole2);
	}


	void changeAlphaUp () {
		temp += 0.005f;
		if (temp >= 1f) {
			temp = 1f;
		}
		image.color = new Color (1, 1, 1, temp);
	}

	void changeAlphaDown () {
		temp2 -= 0.005f;
		image.color = new Color (1, 1, 1, temp2);
	}

	void changeAlphaNetDown () {
		alphaValue_2 -= 0.005f;
		volleyballNet.GetComponent<Renderer>().material.color = new Color (1, 1, 1, alphaValue_2);
	}

	void changeAlphaPole1Down () {
		alphaValuePole1_2 -= 0.005f;
		pole1.GetComponent<Renderer>().material.color = new Color (1, 1, 1, alphaValuePole1_2);
	}

	void changeAlphaPole2Down () {
		alphaValuePole2_2 -= 0.005f;
		pole2.GetComponent<Renderer>().material.color = new Color (1, 1, 1, alphaValuePole2_2);
	}

}

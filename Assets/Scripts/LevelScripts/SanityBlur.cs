using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SanityBlur : MonoBehaviour {

	BlurOptimized blur;
	// Use this for initialization
	void Start () {
		blur = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized> ();
		blur.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void setBlur(bool value) {
		blur.enabled = value;
	}
}

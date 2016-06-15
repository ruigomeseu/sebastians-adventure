using UnityEngine;
using System.Collections;

public class ThrowObjectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ThrowRock(){
		GetComponent<Animator>().SetBool (Animator.StringToHash ("Throwing"), true);
	}
}

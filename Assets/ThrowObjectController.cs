﻿using UnityEngine;
using System.Collections;

public class ThrowObjectController : MonoBehaviour {

	public GameObject rock;
	private GameObject rockClone;
	private Rigidbody rockRigidBody;
	public GameObject parentBone;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ThrowRock(){
		GetComponent<Animator>().SetBool (Animator.StringToHash ("Throwing"), true);

		Debug.Log ("ATIRAR PEDRA");

	}



	public void CreateRock(){
		if (this.rockClone == null) {
			this.rockClone = Instantiate (rock) as GameObject; 
			this.rockClone.transform.position = parentBone.transform.position;
			this.rockClone.transform.parent = parentBone.transform;
			rockRigidBody = this.rockClone.GetComponent<Rigidbody> ();
			rockRigidBody.useGravity = false;
		}
	}

	public void ReleaseRock(){
		this.rockClone.transform.parent = null;
		rockRigidBody.useGravity = true;
		this.rockClone.transform.rotation = parentBone.transform.rotation;
		rockRigidBody.AddForce (transform.forward * 1000);

		this.rockClone = null;
		Destroy (this.rockClone);
	}
}

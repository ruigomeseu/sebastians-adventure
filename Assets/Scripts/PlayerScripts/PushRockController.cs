using UnityEngine;
using System.Collections;

public class PushRockController : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float pushPower = 2.0F;

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject == null || hit.gameObject.tag != "Player")
			return;

		if (hit.gameObject.GetComponent<PlayerControl> ().isSprinting ()) {
			GetComponent<Rigidbody> ().isKinematic = true;
			return;
		}
		GetComponent<Rigidbody> ().isKinematic = false;

		Rigidbody body = hit.gameObject.GetComponent<Rigidbody>();
		if (body == null || body.isKinematic)
			return;

		Vector3 pushDir = hit.gameObject.GetComponent<PlayerControl> ().currentDirection;
		body.velocity = pushDir;

		Debug.Log (pushDir.x + " , " + pushDir.y + " , " + pushDir.z);
		hit.gameObject.GetComponent<PlayerControl>().anim.SetBool (hit.gameObject.GetComponent<PlayerControl>().pushingBool, true);
	}


	void OnCollisionStay(Collision hit) {
		if (hit.gameObject == null || hit.gameObject.tag != "Player")
			return;
		if (hit.gameObject.GetComponent<PlayerControl> ().isSprinting ()) {
			GetComponent<Rigidbody> ().isKinematic = true;
			return;
		}
		GetComponent<Rigidbody> ().isKinematic = false;

		Rigidbody body = hit.gameObject.GetComponent<Rigidbody>();
		if (body == null || body.isKinematic)
			return;

		Vector3 pushDir = hit.gameObject.GetComponent<PlayerControl> ().currentDirection;
		body.velocity = pushDir;

		Debug.Log (pushDir.x + " , " + pushDir.y + " , " + pushDir.z);
	}
}

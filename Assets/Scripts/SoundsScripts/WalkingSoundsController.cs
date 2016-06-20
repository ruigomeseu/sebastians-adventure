using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkingSoundsController : MonoBehaviour {

	private AudioSource _footstepSource;
	private AudioSource _breathSource;

	public List<AudioClip> footsteps;
	public List<AudioClip> breathsWalk;
	public List<AudioClip> breathsRun;

	public void Start(){

		_footstepSource = gameObject.AddComponent<AudioSource>();
		_footstepSource.volume = 0.3f;
		_breathSource = gameObject.AddComponent<AudioSource>();
		_breathSource.volume = 1f;

		//walkSound = (AudioClip)Resources.Load ("Sounds/Footsteps/grass_footstep");
	}

	public void StopWalkingSound(){
		_footstepSource.Stop ();
		_breathSource.Stop ();
	}

	public void PlayWalkingSound(){
		FootstepPlay ();

		if (_breathSource.isPlaying)
			return;
		if (GetComponent<PlayerControl> ().sprint) {
			Debug.Log ("breath run");
			BreathRun ();
		} else {
			Debug.Log ("breath walk");
			BreathWalk ();
		}
	}


	public void FootstepPlay(){
		int rand = Random.Range (0, footsteps.Count);
		_footstepSource.clip = footsteps [rand];
		_footstepSource.Play ();
	}


	public void BreathRun(){
		int rand = Random.Range (0, breathsRun.Count);
		Debug.Log (breathsRun [rand].name);
		_breathSource.clip = breathsRun [rand];
		_breathSource.Play ();
	}


	public void BreathWalk(){
		int rand = Random.Range (0, breathsWalk.Count);
		_breathSource.clip = breathsWalk [rand];
		_breathSource.Play ();
	}


}

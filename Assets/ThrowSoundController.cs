using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowSoundController : MonoBehaviour {
	private AudioSource _throwRockSource;

	public List<AudioClip> throwRockClips;

	// Use this for initialization
	void Start () {

		_throwRockSource = gameObject.AddComponent<AudioSource>();
		_throwRockSource.volume = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayThrowRockSound(){
		int rand = Random.Range (0, throwRockClips.Count);
		_throwRockSource.clip = throwRockClips [rand];
		_throwRockSource.Play ();
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlienSoundsController : MonoBehaviour {

	private AudioSource _audioSource;

	public AudioClip dieClip;
	public List<AudioClip> damageClips;
	public List<AudioClip> attackClips;

	// Use this for initialization
	void Awake () {
		_audioSource = gameObject.AddComponent<AudioSource>();
		_audioSource.enabled = true;
		_audioSource.volume = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DieSound(){
		_audioSource.clip = dieClip;
		_audioSource.Play ();
	}

	public void TakeDamageSound(){
		int rand = Random.Range (0, damageClips.Count);
		_audioSource.clip = damageClips [rand];
		_audioSource.Play ();
	}

	public void AttackSound(){
		int rand = Random.Range (0, attackClips.Count);
		_audioSource.clip = attackClips [rand];
		_audioSource.Play ();
	}

	public void StopSound(){
		_audioSource.Stop ();
	}
}

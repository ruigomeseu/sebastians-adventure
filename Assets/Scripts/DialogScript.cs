using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogScript : MonoBehaviour {

	public List<AudioClip> dialogAudioList;
	public AudioClip currentAudioClip;
	public int currentAudioInt;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void NextDialog(){
		currentAudioInt++;
		if (currentAudioInt >= dialogAudioList.Count) {
            this.GetComponent<MainStoryScript>().ShowObjective();
            this.GetComponent<PlayerControl> ().IsMovementActivated = true;
			return;
		}

		currentAudioClip = dialogAudioList[currentAudioInt];
		audioSource.clip = currentAudioClip;
		audioSource.Play ();

		Invoke ("NextDialog", currentAudioClip.length);
	}


	public void StartDialog(List<AudioClip> dialogList){
		dialogAudioList = dialogList;
		currentAudioInt = 0;

		this.GetComponent<PlayerControl> ().IsMovementActivated = false;

		currentAudioClip = dialogAudioList[currentAudioInt];
		audioSource.clip = currentAudioClip;
		audioSource.Play ();
		Invoke ("NextDialog", currentAudioClip.length + 2);
	}
}

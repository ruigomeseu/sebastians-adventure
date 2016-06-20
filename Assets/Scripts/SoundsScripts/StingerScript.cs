using UnityEngine;
using System.Collections;

public class StingerScript : MonoBehaviour {

    public MusicManager.GameState Type;
	public AudioClip Clip;
	private AudioSource _audioSource;
	// Use this for initialization
	void Start () 
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.clip = Clip;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void PlayMute()
	{
		_audioSource.volume = 0;
		_audioSource.Play();
	}
	
    public void Play()
	{
		_audioSource.volume = 1;
		_audioSource.Play();
	}
	
	public void Rewind()
	{
		_audioSource.Stop();
	}
}

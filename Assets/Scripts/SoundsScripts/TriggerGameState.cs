using UnityEngine;
using System.Collections;

public class TriggerGameState : MonoBehaviour {

	public string Tag;
	[Header("Trigger Game State To")]
	public MusicManager.GameState State;
	public int Level;
	[Header("On")]
	public bool TagCollisonEnter;
	public bool TagCollisionExit;
    public bool TagTriggerEnter;
	public bool TagTriggerExit;
	public bool TagTriggerDestroy;
	
	private MusicManager _audioManager;
	private GameManager _gameManager;
	// Use this for initialization
	void Start () {
	
	_audioManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
	_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnCollisionEnter(Collision c)
	{
		if(c.gameObject.tag == Tag && TagCollisonEnter)
		{
			_gameManager.State = State;
			_gameManager.Level = Level;
		}
	}
	void OnCollisionExit(Collision c)
	{
		if(c.gameObject.tag == Tag && TagCollisionExit)
		{
			_gameManager.State = State;
			_gameManager.Level = Level;
		}
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == Tag && TagTriggerEnter)
		{
			_gameManager.State = State;
			_gameManager.Level = Level;
		}
	}
	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.tag == Tag && TagTriggerExit)
		{
			_gameManager.State = State;
			_gameManager.Level = Level;
		}
	}
	void OnDestroy()
	{
		if(TagTriggerDestroy)
		{
			_gameManager.State = State;
			_gameManager.Level = Level;
		}
	}
}

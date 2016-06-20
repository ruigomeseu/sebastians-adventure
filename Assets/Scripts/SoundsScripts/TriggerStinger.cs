using UnityEngine;
using System.Collections;

public class TriggerStinger : MonoBehaviour {

	public string Tag;
	[Header("Trigger Stinger of Type")]
	public MusicManager.GameState Type;
	[Header("On")]
	public bool TagCollisonEnter;
	public bool TagCollisionExit;
	public bool TagTriggerEnter;
	public bool TagTriggerExit;
	public bool TagDestroy;
	
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
			_audioManager.FireStingerOfType(Type);
		}
	}
	void OnCollisionExit(Collision c)
	{
		if(c.gameObject.tag == Tag && TagCollisionExit)
		{
			_audioManager.FireStingerOfType(Type);
		}
	}
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == Tag && TagTriggerEnter)
		{
			_audioManager.FireStingerOfType(Type);
		}
	}
	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.tag == Tag && TagTriggerExit)
		{
			_audioManager.FireStingerOfType(Type);
		}
	}

	void OnDestroy()
	{
		if(TagDestroy)
		{
			_audioManager.FireStingerOfType(Type);
		}
	}
}

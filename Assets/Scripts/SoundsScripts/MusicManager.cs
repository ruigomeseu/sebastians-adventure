using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MusicManager : MonoBehaviour{

public enum GameState 
{
    Stealth, Stress, Fight, None    
}
    public float FadeTime;
    public List<AudioClip> StealthClips;
    public List<AudioClip> StressClips;
    public List<AudioClip> FightClips;
    
    public List<AudioClip> StealthStingers;
    public List<AudioClip> StressStingers;
    public List<AudioClip> FightStingers;
    
    private List<GameObject> StealthStingerGameObjects = new List<GameObject>();
    private List<GameObject> StressStingerGameObjects = new List<GameObject>();
    private List<GameObject> FightStingerGameObjects = new List<GameObject>();
    
    public GameObject StingerPrefab; 
    
    private bool _isFadingIn;
    private bool _isFadingOut;
    private bool _hasFadedOut;
    private bool _hasFadedIn;
    
    private AudioSource _audio;
    private float t = 0;

    private GameState _audioState = GameState.None;
    private int _audioLevel = 1;
    private float _audioTime = 0;
    private GameManager _gameManager;

	// Use this for initialization
	void Start (){
	    _isFadingOut = false;
        _hasFadedOut = false;
        _isFadingIn = false;
        _hasFadedIn = false;
        _audio = GetComponent<AudioSource>();
	    _audio.volume = 0;
        _audio.Play();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InitializeStingers(GameState.Stealth, StealthStingers);
        InitializeStingers(GameState.Stress, StressStingers);
        InitializeStingers(GameState.Fight, FightStingers);
        
	}
    
    void InitializeStingers(GameState State, List<AudioClip> StingerContainer)
    {
        foreach (var stingerClip in StingerContainer)
        {
            var go = (GameObject) GameObject.Instantiate(StingerPrefab, transform.position, Quaternion.identity);
            go.name = State.ToString() + " Stinger";
            var script = go.GetComponent<StingerScript>();
            script.Clip = stingerClip;
            script.Type = State;
            if(State == GameState.Stealth)
            {
                StealthStingerGameObjects.Add(go);
            }
             if(State == GameState.Stress)
            {
                StressStingerGameObjects.Add(go);
            }
             if(State == GameState.Fight)
            {
                FightStingerGameObjects.Add(go);
            }
        }
    }
    
    public void FireStingerOfType(GameState State)
    {
        if(State == GameState.Stealth)
        {
            StealthStingerGameObjects[Random.Range(0,StealthStingerGameObjects.Count)].GetComponent<StingerScript>().Play();
        }
        if(State == GameState.Stress)
        {
            StressStingerGameObjects[Random.Range(0,StressStingerGameObjects.Count)].GetComponent<StingerScript>().Play();
        }
        if(State == GameState.Fight)
        {
            FightStingerGameObjects[Random.Range(0,FightStingerGameObjects.Count)].GetComponent<StingerScript>().Play();
        }
    }
    
    void Update()
    {
        if(_gameManager.State == _audioState)
        {
            if(_gameManager.Level != _audioLevel)
            {
                if(_gameManager.State == GameState.Stealth)
                {
                    ImmediateTransitionToState(StealthClips);
                }
                if(_gameManager.State == GameState.Fight)
                {
                    ImmediateTransitionToState(FightClips);
                }
                if(_gameManager.State == GameState.Stress)
                {
                    ImmediateTransitionToState(StressClips);
                }
            }
        }
        if (_gameManager.State == GameState.Stealth && _audioState != GameState.Stealth)
        {
                 TransitionToState(GameState.Stealth, StealthClips);
        }
        if (_gameManager.State == GameState.Stress && _audioState != GameState.Stress)
        {
                 TransitionToState(GameState.Stress, StressClips);
        }
        if (_gameManager.State == GameState.Fight && _audioState != GameState.Fight)
        {
                 TransitionToState(GameState.Fight, FightClips);
        }          
    }
    
    void ImmediateTransitionToState(List<AudioClip> Container)
    {
        _audioLevel = _gameManager.Level;
        _audioTime = _audio.time;
        _audio.clip = Container[Mathf.Min(Container.Count, _audioLevel)-1];
        _audio.time = _audioTime;
        _audio.Play();
    }
    
    void TransitionToState(GameState NewState, List<AudioClip> Container)
    {
        if(!_audio.isPlaying)
        _audio.Play();
        
        if(_hasFadedOut == false && _hasFadedIn == false)
            {
                Debug.Log("1");
                FadeOut();
            }
            else if(_hasFadedOut == true && _hasFadedIn == false && _isFadingIn == false)
            {
                Debug.Log("2");
                _audioLevel = _gameManager.Level;
                _audio.clip = Container[Mathf.Min(Container.Count, _audioLevel)-1];
                foreach (var item in StealthStingerGameObjects)
                {
                    item.GetComponent<StingerScript>().Rewind();
                    item.GetComponent<StingerScript>().PlayMute();
                }
                FadeIn();                
            }
            else if(_isFadingIn)
            {
                Debug.Log("3");
                FadeIn();                
            }
            else if (_hasFadedOut && _hasFadedIn)
            {
                Debug.Log("4");
                _audioState=NewState;
                _isFadingOut = false;
                _hasFadedOut = false;
                _isFadingIn = false;
                _hasFadedIn = false;
            }
    }

    public void FadeOut() {
        FadeOutSound();
    }

    public void FadeIn(){
        FadeInSound();
    }

    private void FadeOutSound(){
        _hasFadedOut = false;
        _isFadingOut = true;
        float initVol = 1;
        while (t < 1){
            _audio.volume = Mathf.Lerp(initVol, 0, t);
            t += Time.deltaTime/FadeTime;
            return;
        }
        t= 0;
        _isFadingOut = false;
        _hasFadedOut = true;
    }

    private void FadeInSound(){
        _isFadingIn = true;
        _hasFadedIn = false;
        float initVol = 0;
        while (t < 1){
            _audio.volume = Mathf.Lerp(initVol, 1, t);
            t += Time.deltaTime/FadeTime;
            return;
        }
        t=0;
        _audio.volume = 1;
        _isFadingIn = false;
        _hasFadedIn = true;
    }

    
}

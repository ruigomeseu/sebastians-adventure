using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStoryScript : MonoBehaviour {

	public Dictionary<string, string> StoryStates = new Dictionary<string, string>(){
		{"find_house" , "Encontra a casa para obteres orientações do habitante"},
		{"talk_npc" , "Fala com o habitante local"},
		{"find_map" , "Encontra o mapa"},
		{"find_pilot", "Encontra o piloto para terminar o jogo"}
	};
	public string CurrentStoryState;

	public bool ObjectiveShown = false;
	Rect objectiveRect;
	Texture2D objectiveTexture;

	private AudioSource audioSource;
	private AudioClip audioClip;

	private float ShowTime = 0f;

	private GameObject npcPlayer;


	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource>();
		CurrentStoryState = "find_house";
		objectiveRect = new Rect ((Screen.width / 3), (Screen.height / 3) * 2 , (Screen.width/3), 80);

		objectiveTexture = new Texture2D(1, 1);
		objectiveTexture.SetPixel(0,0, new Color(0f,0f,0f, 1f));
		objectiveTexture.Apply();

		npcPlayer = GameObject.FindGameObjectWithTag ("NPCPlayer");
	}

	public void SetStoryState(string state)
	{
		if (state != CurrentStoryState) {
			CurrentStoryState = state;
		}
	}

	public void ShowObjective(){
		this.ObjectiveShown = false;
	}

	void TalkNPC(){
		/*
		this.GetComponent<PlayerControl> ().IsMovementActivated = false;	

		audioClip = (AudioClip) Resources.Load("Sounds/pickItem_teste");
		audioSource.clip = audioClip;
		audioSource.Play ();
		this.GetComponent<PlayerControl> ().IsMovementActivated = true;
		Invoke ("ShowObjective", audioClip.length);*/

		List<AudioClip> audioList = new List<AudioClip> ();

		audioList.Add((AudioClip) Resources.Load("Sounds/Dialogs/Player/dialoguePlayer_1"));
		audioList.Add((AudioClip) Resources.Load("Sounds/Dialogs/NPC/dialogueNpc_1"));
		audioList.Add((AudioClip) Resources.Load("Sounds/Dialogs/Player/dialoguePlayer_2"));
		audioList.Add((AudioClip) Resources.Load("Sounds/Dialogs/NPC/dialogueNpc_2"));
		audioList.Add((AudioClip) Resources.Load("Sounds/Dialogs/Player/dialoguePlayer_3"));

		this.GetComponent<DialogScript> ().StartDialog (audioList);
	}

	void Update () {
		switch(CurrentStoryState){
			case "find_house":
				if (Vector3.Distance (this.transform.position, npcPlayer.transform.position) < 8) {
					this.SetStoryState ("talk_npc");
					this.ShowObjective ();
				}
				break;
			case "talk_npc":
				if (Vector3.Distance (this.transform.position, npcPlayer.transform.position) < 2) {
                    Debug.Log("TALK NPC OBJECTIVE");
					if (!GetComponent<DialogScript>().audioSource.isPlaying) {
						TalkNPC ();
						this.SetStoryState ("find_map");
                    }

				}
				break;
			case "find_map":
                Debug.Log("A");
                if (GetComponent<CharacterInventory> ().pickedMap) {
					this.SetStoryState ("find_pilot");
					this.ShowObjective ();
				}	
				break;	
			case "find_pilot":
				
				break;	
		}

	}

	void OnGUI(){
		if (!ObjectiveShown) {
			GUI.DrawTexture (objectiveRect, objectiveTexture);
			GUI.Label (objectiveRect, StoryStates[CurrentStoryState] );
			if (Time.realtimeSinceStartup - ShowTime > 3) {
				ObjectiveShown = true;
			}
		} else {
			ShowTime = Time.realtimeSinceStartup;
		}
	}

	public void ObjectPicked(GameObject obj)
	{
		
	}


}

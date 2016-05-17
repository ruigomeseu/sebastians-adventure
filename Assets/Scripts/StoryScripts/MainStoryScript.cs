using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStoryScript : MonoBehaviour {

	public Dictionary<string, string> StoryStates = new Dictionary<string, string>(){
		{"find_house" , "Encontra a casa para obteres orientações do habitante"},
		{"talk_npc" , "Fala com o habitante local"},
		{"find_map" , "Encontra o mapa"},
		{"find_beach", "Encontra a praia" }
	};
	public string CurrentStoryState;

	public bool ObjectiveShown = false;
	Rect objectiveRect;
	Texture2D objectiveTexture;

	private float ShowTime = 0f;

	private GameObject npcPlayer;


	// Use this for initialization
	void Start () {
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
			this.ObjectiveShown = false;
		}
	}


	void Update () {
		switch(CurrentStoryState){
			case "find_house":
				if (Vector3.Distance (this.transform.position, npcPlayer.transform.position) < 2) {
					this.SetStoryState ("talk_npc");
				}
				break;
			case "talk_npc":
				if (Vector3.Distance (this.transform.position, npcPlayer.transform.position) < 2) {
					this.SetStoryState ("find_map");
				}
				break;
			case "find_map":
				Debug.Log ("Inventory Objects:");
				Debug.Log (GetComponent<CharacterInventory> ().inventoryObjectsNames.Count );
				if (GetComponent<CharacterInventory> ().inventoryObjectsNames.Contains ("Map")) {
					this.SetStoryState ("find_beach");
				}
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

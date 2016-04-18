using UnityEngine;
using System.Collections;

public class CharacterInventory : MonoBehaviour {
    
	public ArrayList inventoryObjects;

	public ArrayList inventory;
	public ArrayList inventoryTextureArray;
	Rect inventoryRect;
	Texture2D inventoryTexture;


	public bool opened = false;

	float inventoryX = (Screen.width / 20);
	float inventoryY = (Screen.height / 12)*9.5f;

	float inventoryWidth = Screen.width / 2;
	float inventoryHeight = Screen.height / 10;

	/**
	 * Picked objects variables
	 */
	public bool picked = false;
	Texture2D pickedObjectTexture;
	Rect pickedObjectRect;

	// Use this for initialization
	void Start () {
		
		inventory = new ArrayList ();
		inventoryTextureArray = new ArrayList ();
		inventoryObjects = new ArrayList ();
	
	}


	void showInventoryOnPanel()
	{
		int col = 0;
		int i = 0;
		for( ; i < inventoryObjects.Count; i++)
		{
			float offset = 2;
			Rect boxInv = new Rect (inventoryX + ((inventoryWidth/9)+offset)*col + offset, 
				inventoryY,
				inventoryWidth/9,
				inventoryHeight);
			Texture2D boxInvTexture = (Texture2D)inventoryObjects [i];
			inventory.Add (boxInv);
			inventoryTextureArray.Add (boxInvTexture);

			col++;
		}
	}

	void createInventoryPanel()
	{

		inventoryRect = new Rect (inventoryX, inventoryY, inventoryWidth, inventoryHeight);
		inventoryTexture = new Texture2D (1, 1);
		inventoryTexture.SetPixel (0, 0, new Color (236 / 255f, 208 / 255f, 120 / 255f, 0.4f));
		inventoryTexture.Apply ();


		showInventoryOnPanel ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.I)) {
			if (opened) {
				opened = false;
			} else {
				opened = true;
				createInventoryPanel ();
			}
		}


		if(Input.GetKeyUp (KeyCode.E)) {
			if (picked) {
				picked = false;
				return;
			}

			GameObject[] objectsInWorld = GameObject.FindGameObjectsWithTag ("InventoryObject");

			foreach( GameObject go in objectsInWorld)
			{
				float distance =Vector3.Distance(transform.position,go.transform.position);


				if (distance < 2) {
					//pick object
					PickableObjectBehaviour temp = go.GetComponent<PickableObjectBehaviour>();
					inventoryObjects.Add (temp.objTexture);
		
					picked = true; // sets object to appear
					pickedObjectTexture = temp.objTexture; //sets texture to appear
					pickedObjectRect = new Rect(temp.pickedObjX,temp.pickedObjY,temp.pickedObjWidth,temp.PickedObjHeight);
					Debug.Log (temp.pickedObjX);
					Debug.Log (temp.pickedObjY);
					Destroy (go);
				}
			}
		}

	}


	void OnGUI(){
		if (opened) {
			GUI.DrawTexture (inventoryRect, inventoryTexture);

			int i;
			for (i = 0; i < inventoryTextureArray.Count; i++) {
				GUI.DrawTexture ((Rect)inventory[i], (Texture2D) inventoryTextureArray[i]);
			}
		}

		if (picked) {
			GUI.DrawTexture (pickedObjectRect, pickedObjectTexture,ScaleMode.ScaleToFit);
		}

	}
}

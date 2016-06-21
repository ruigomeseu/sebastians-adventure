using UnityEngine;
using System.Collections;

public class CharacterInventory : MonoBehaviour {
	public ArrayList inventoryObjectsNames;
	public ArrayList inventoryObjects;

	public ArrayList inventory;
	public ArrayList inventoryTextureArray;
	Rect inventoryRect;
	Texture2D inventoryTexture;

	public ArrayList inventoryObjectsPickable;

	public bool opened = true;

	private AudioSource audioSource;
	private AudioClip pickedItemSound;


	private GameObject normalCamera;
	private GameObject zoomCamera;


	// images from the inventory and interface

	public Texture hpText;
	public Texture hpBar;
	public Texture staminaText;
	public Texture staminaBar;
	public Texture binocularImage;
	public Texture binocularNonPickedImage;
	public Texture rockImage;
	public Texture mapImage;
	public Texture mapNonPickedImage;
	public Texture mapImageShow;


	private bool pickedBinoc=false;
	private bool pickedMap=false;

	private bool showMap=false;

	//Scripts
	private PlayerSanity playerSanityScript;
	private Stamina playerStaminaScript;

	// Use this for initialization
	void Start () {
		inventoryObjectsNames = new ArrayList ();
		inventoryObjectsPickable = new ArrayList ();
		inventory = new ArrayList ();
		inventoryTextureArray = new ArrayList ();
		inventoryObjects = new ArrayList ();

		audioSource = gameObject.AddComponent<AudioSource>();
		pickedItemSound = (AudioClip) Resources.Load("Sounds/pickItem_teste");

		normalCamera = GameObject.Find ("Main Camera");
		zoomCamera = GameObject.Find ("Zoom Camera");

		normalCamera.SetActive (true);
		zoomCamera.SetActive (false);

		playerSanityScript = GetComponent<PlayerSanity> ();
		playerStaminaScript = GetComponent<Stamina> ();
	}

	// Update is called once per frame
	void Update () {
		//pick object
		if(Input.GetKeyUp (KeyCode.E)) {


			GameObject map = GameObject.FindGameObjectWithTag ("Map");
			if (map != null) {
			 
				float distanceToMap = Vector3.Distance (transform.position, map.transform.position);
				if(distanceToMap < 2) {
					GetComponent<PlayerControl>().anim.SetBool (GetComponent<PlayerControl>().pickingBool, true);
					audioSource.clip = pickedItemSound;
					audioSource.Play ();
					pickedMap = true;
					map.SetActive(false);
				}
			}

			GameObject binoc = GameObject.FindGameObjectWithTag ("Binoc");
			if(binoc != null) {

				float distanceToBinoc = Vector3.Distance (transform.position, binoc.transform.position);
				if (distanceToBinoc < 2) {
					GetComponent<PlayerControl>().anim.SetBool (GetComponent<PlayerControl>().pickingBool, true);
					audioSource.clip = pickedItemSound;
					audioSource.Play ();
					pickedBinoc = true;
					binoc.SetActive(false);
				}
			}
		}


		//select object from inventory
		if (Input.GetKeyDown (KeyCode.Alpha2) && pickedMap) {
			showMap = true;
		} else if (Input.GetKeyUp (KeyCode.Alpha2) && pickedMap) {
			showMap = false;
		}

		if (Input.GetKeyDown (KeyCode.Alpha1) && pickedBinoc) {
			switchCameras ();
		}
		else if (Input.GetKeyUp (KeyCode.Alpha1) && pickedBinoc) {
			switchCameras ();
		}
	}


	void OnGUI(){
		drawInventory ();
		drawInterfaceBars ();

		if (showMap) {
			drawMap ();
		}

		drawInterfaceBars ();
	}


	void switchCameras() {
		normalCamera.SetActive (!normalCamera.activeSelf);
		zoomCamera.SetActive (!zoomCamera.activeSelf);
	}

	void drawInterfaceBars() {
		//draw the HP bar
		GUI.DrawTexture (new Rect(20, Screen.height - 50, (float)25*4.54f, 25f), hpText, ScaleMode.ScaleToFit);
		//draw the Stamina bar
		GUI.DrawTexture (new Rect(20, Screen.height - 90, (float)25*4.54f, 25f), staminaText, ScaleMode.ScaleToFit);

		//draw the hp bars
		float numHpBars = Mathf.Round((float)(playerSanityScript.sanity*50f)/1000f);

		for (int i = 0; i < numHpBars; i++) {
			GUI.DrawTexture (new Rect(28*4.54f + i*8 , Screen.height - 50, 12.39f, 12.39f*2.017f), hpBar, ScaleMode.ScaleToFit);
		}


		//draw the stamina bars
		float numStaminaBars = Mathf.Round((float)(playerStaminaScript.getCurrentStamina()*50f)/10f);

		for (int j = 0; j < numStaminaBars; j++) {
			GUI.DrawTexture (new Rect(28*4.54f + j*8 , Screen.height - 90, 12.39f, 12.39f*2.017f), staminaBar, ScaleMode.ScaleToFit);
		}

	}

	void drawMap() {
		GUI.DrawTexture (new Rect ((Screen.width - mapImageShow.width)/2,((Screen.height - mapImageShow.height)/2),mapImageShow.width,mapImageShow.height), mapImageShow,ScaleMode.ScaleToFit);
	}


	void drawInventory() {
		if (!pickedBinoc) {
			GUI.DrawTexture (new Rect (20, 10, 70,101), binocularNonPickedImage);

		} else {
			GUI.DrawTexture (new Rect (20, 10, 70,101), binocularImage);

		}
		if (!pickedMap) {
			GUI.DrawTexture (new Rect (100, 10, 70,101), mapNonPickedImage);

		} else {
			GUI.DrawTexture (new Rect (100, 10, 70,101), mapImage);
		}

		GUI.DrawTexture (new Rect (180, 10, 70,101), rockImage);
	}
}

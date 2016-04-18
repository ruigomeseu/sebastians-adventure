using UnityEngine;
using System.Collections;

public class PickableObjectBehaviour : MonoBehaviour {

	public Color color;
	public Texture2D objTexture;

	public float pickedObjX;
	public float pickedObjY;

	public float pickedObjWidth;
	public float PickedObjHeight;

	// Use this for initialization
	void Start () {
		
		pickedObjX = (Screen.width / 2) - (objTexture.width/2);
		pickedObjY = (Screen.height / 2) - (objTexture.height/2);
		pickedObjWidth = objTexture.width;
		PickedObjHeight = objTexture.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

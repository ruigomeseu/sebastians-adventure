using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour {
	
	private Canvas canvas;
	private GameObject player;

	// Use this for initialization
	void Start () {	
		canvas = this.GetComponent<Canvas> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<PlayerSanity>().enabled = false;
		player.GetComponent<Stamina>().enabled = false;
		this.canvas.enabled = true;
		Time.timeScale = 0;
		Screen.lockCursor = false;
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		this.canvas.enabled = false;
		Screen.lockCursor = true;
		player.GetComponent<PlayerSanity>().enabled = true;
		player.GetComponent<Stamina>().enabled = true;
	}

	public void QuitGame() {
		Application.Quit ();
	}
	// Update is called once per frame
	void Update () {
	}
}

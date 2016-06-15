using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour {

	private bool gamePaused;
	private Canvas canvas;

	// Use this for initialization
	void Start () {	
		canvas = this.GetComponent<Canvas> ();

		this.canvas.enabled = true;
		this.gamePaused = true;
		Time.timeScale = 0;
		Screen.lockCursor = false;
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		this.canvas.enabled = false;
		this.gamePaused = false;
		Screen.lockCursor = true;
	}

	public void QuitGame() {
		Application.Quit ();
	}
	// Update is called once per frame
	void Update () {
	}
}

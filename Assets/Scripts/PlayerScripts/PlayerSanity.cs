using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerSanity : MonoBehaviour {

	private double sanity;
	private double maxSanityLevel = 1000;
	private double sanityCouncern=100;
	private SanityBlur sanityBlurScript;
	private double lastAttackReceived=0f;
	private double minTimeBetweenRecoveries=5f;
	private double recoveryRate=50;

	Rect sanityRect;
	Texture2D sanityTexture;

	void Start () {
		sanity = maxSanityLevel;
		sanityBlurScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SanityBlur> ();
		sanityRect = new Rect((Screen.width / 20), (Screen.height / 10) * 9f, Screen.width / 3, Screen.height / 50);
		sanityTexture = new Texture2D(1, 1);
		sanityTexture.SetPixel(0, 0, new Color(254 / 256f, 208 / 62, 120 / 72f, 1f));
		sanityTexture.Apply();
	}


	// Update is called once per frame
	void Update () {
		if (sanity < sanityCouncern) {
			sanityBlurScript.setBlur (true);
		} else {
			sanityBlurScript.setBlur (false);
		}

		if (Time.time > (minTimeBetweenRecoveries + lastAttackReceived)) {
			lastAttackReceived = Time.time;
			sanity = System.Math.Min (sanity+recoveryRate, maxSanityLevel);
		}

	}

	public void decreaseSanity(int damage) {
		sanity -= damage;
		if (sanity <= 0) {
			//TODO meter a personagem a morrer xD
			Debug.Log ("muck felo dansgame xD");
		}
		lastAttackReceived = Time.time;
	}

	void OnGUI()
	{
		double ratio = sanity / maxSanityLevel;
		double rectWidth = ratio * Screen.width / 3;
		sanityRect.width = (float)rectWidth;
		GUI.DrawTexture(sanityRect, sanityTexture);

	}
}

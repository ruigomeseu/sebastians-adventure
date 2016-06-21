using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerSanity : MonoBehaviour {

	public bool enabled = true;

	public double sanity;
	private double maxSanityLevel = 1000;
	private double sanityCouncern=100;
	private SanityBlur sanityBlurScript;
	private double lastAttackReceived=0f;
	private double minTimeBetweenRecoveries=5f;
	private double recoveryRate=75f;

	Rect sanityRect;
	Texture2D sanityTexture;

	void Start () {
		sanity = maxSanityLevel;
		sanityBlurScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SanityBlur> ();
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
			Debug.Log ("Sanity: " + sanity);
			sanity = System.Math.Min (sanity+recoveryRate, maxSanityLevel);
            sanity = System.Math.Max(0, sanity);
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

}

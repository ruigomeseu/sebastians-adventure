using UnityEngine;
using System.Collections;

public class EnemyLightController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float currentTimeOfDay = 
			GameObject.FindGameObjectWithTag ("MainScripts").GetComponent<DayNightController> ().currentTimeOfDay;


		if (currentTimeOfDay > 0.7 || currentTimeOfDay < 0.4) {
			GetComponent<Light> ().intensity = 8;
		} else {
			GetComponent<Light> ().intensity = 0;
		}
	}
}

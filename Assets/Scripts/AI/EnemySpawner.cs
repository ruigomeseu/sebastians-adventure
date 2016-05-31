using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	//enemies array list
	private ArrayList enemies;

	private int maxEnemies = 5;
	public GameObject prefab;

	//scripts 
	public DayNightController dayNightScript;

	// Use this for initialization
	void Start () {
		enemies = new ArrayList ();
		dayNightScript = GetComponent<DayNightController> ();

	}

	// Update is called once per frame
	void Update () {
		Debug.Log (enemies.Count);

		if (enemies.Count < maxEnemies) {
			Instantiate(prefab,new Vector3(2.0f, 20f, 0f), Quaternion.identity);
			enemies.Add (prefab);
		}

	}
}

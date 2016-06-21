using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	//enemies array list
	private ArrayList enemies;

	public int maxEnemies = 50;
	public List<GameObject> prefabs;

	//scripts 
	public double currentTimeOfTheDay;
	public float lastSpawnTime;
	public float minimumSpawnTime;

	// Use this for initialization
	void Start () {
		enemies = new ArrayList ();
		lastSpawnTime = Time.time - 1000;
		minimumSpawnTime = 10f;
	}

	// Update is called once per frame
	void Update () {
		currentTimeOfTheDay = GetComponent<DayNightController> ().currentTimeOfDay;
		if (enemies.Count < maxEnemies) {
			if ((Time.time - lastSpawnTime) > minimumSpawnTime) {
				Debug.Log ("Time to spawn");
				if (Random.Range (0, 100) > currentTimeOfTheDay*100) {
					lastSpawnTime = Time.time;
                    GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];    
					Instantiate(prefab,generateValidSpawnPosition(), Quaternion.identity);
					enemies.Add (prefab);
				}
			}
		}
	}

	Vector3 generateValidSpawnPosition() {
		double x = Random.Range (0, 140);
		x -= 70f;
		double y = Random.Range (7, 14);
		double z = Random.Range (0, 140);
		z -= 70f;

		// the multiplication by 2 is to see the effect of the alliens falling of the sky
		return new Vector3 ((float)x, (float)y, (float)z);
	}
}

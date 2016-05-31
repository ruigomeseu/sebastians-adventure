using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	GameObject playerChar;
	NavMeshAgent nav;
	private int damage=10;
	private double minimum_attack_distance= 20f;
	private float lastAttack;
	private float timeBetweenAttacks;

	// Use this for initialization
	void Start () {
		playerChar = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent<NavMeshAgent>();
		lastAttack = 0f;
		timeBetweenAttacks = 2f;
	}

	// Update is called once per frame
	void Update () {

		//if the enemy is close enough to be attacked
		if (Vector3.Distance (nav.transform.position, playerChar.transform.transform.position) < minimum_attack_distance) 
		{
			if ((lastAttack + timeBetweenAttacks) < Time.time) {
				playerChar.GetComponent<PlayerSanity> ().decreaseSanity (damage);
				lastAttack = Time.time;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	GameObject playerChar;
	NavMeshAgent nav;
	public int damage=10;
	public double minimum_attack_distance= 20f;
	public float lastAttack;
	public float timeBetweenAttacks;

	public int attackingBool;
	public int movingBool;
	Animator animator;

	// Use this for initialization
	void Start () {
		playerChar = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator> ();
		attackingBool = Animator.StringToHash ("isAttacking");
		movingBool = Animator.StringToHash ("isMoving");


		lastAttack = 0f;
		timeBetweenAttacks = 5f;
	}

	// Update is called once per frame
	void Update () {

		//if the enemy is close enough to be attacked
		if (Vector3.Distance (nav.transform.position, playerChar.transform.transform.position) < minimum_attack_distance) 
		{
			if ((lastAttack + timeBetweenAttacks) < Time.time) {
				playerChar.GetComponent<PlayerSanity> ().decreaseSanity (damage);
				lastAttack = Time.time;
				animator.SetBool (movingBool, false);
				animator.SetBool (attackingBool, true);

			}
		}

		//checkAttackAnimation ();
	}

	void checkAttackAnimation() {
		if (Time.time > (lastAttack + 2f)) {
			animator.SetBool (attackingBool, false);
		}
	}
}

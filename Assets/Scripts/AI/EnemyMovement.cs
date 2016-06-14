using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;                 // Reference to the player's position.
    //PlayerHealth playerHealth;        // Reference to the player's health.
    //EnemyHealth enemyHealth;          // Reference to this enemy's health.
    NavMeshAgent nav;                 // Reference to the nav mesh agent.
    public float minimum_distance = 50f;

	private int movingBool;
	private Animator animator;

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator> ();
		movingBool = Animator.StringToHash ("isMoving");
    }


    void Update()
    {
		double lastAttack = GetComponent<EnemyAttack> ().lastAttack;

		float currentDistance = Vector3.Distance (nav.transform.position, player.transform.position);

		// If the enemy is close enough
		if (currentDistance <= 140 / 100) {
			animator.SetBool (movingBool, false);
		} else if (currentDistance < minimum_distance) {
			// ... set the destination of the nav mesh agent to the player.
			if ((lastAttack + 3f) < Time.time) {
				nav.SetDestination (player.position);
				animator.SetBool (movingBool, true);
			} else {
				nav.SetDestination (nav.transform.position);
				animator.SetBool (movingBool, false);

			}
		} else {
			// ... disable the nav mesh agent.
			//nav.enabled = false;
			nav.SetDestination (nav.transform.position);
			animator.SetBool (movingBool, false);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "rock")
		{
			Destroy(this);
		}
	}

} 
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
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator> ();
		movingBool = Animator.StringToHash ("isMoving");
    }


    void Update()
    {
		float currentDistance = Vector3.Distance (nav.transform.position, player.transform.position);

		// If the enemy is close enough
		if (currentDistance <= 140/100) {
			animator.SetBool (movingBool, false);
		}
		else if (currentDistance < minimum_distance)
        {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
			animator.SetBool (movingBool, true);
        }
        else
        {
            // ... disable the nav mesh agent.
            //nav.enabled = false;
			nav.SetDestination(nav.transform.position);
			animator.SetBool (movingBool, false);
        }
    }
} 
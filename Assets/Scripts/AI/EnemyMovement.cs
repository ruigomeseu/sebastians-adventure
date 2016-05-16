using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;                 // Reference to the player's position.
    //PlayerHealth playerHealth;        // Reference to the player's health.
    //EnemyHealth enemyHealth;          // Reference to this enemy's health.
    NavMeshAgent nav;                 // Reference to the nav mesh agent.
    public float minimum_distance = 50f;


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        // If the enemy is close enough
        if (Vector3.Distance(nav.transform.position, player.transform.position) < minimum_distance)
        {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
        }
        else
        {
            // ... disable the nav mesh agent.
            //nav.enabled = false;
            nav.SetDestination(nav.transform.position);
        }
    }
} 
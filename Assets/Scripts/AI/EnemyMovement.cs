using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;                 // Reference to the player's position.
    //PlayerHealth playerHealth;        // Reference to the player's health.
    //EnemyHealth enemyHealth;          // Reference to this enemy's health.
    NavMeshAgent nav;                 // Reference to the nav mesh agent.
    public float minimum_distance = 10f;

	private int movingBool;
	private int attackingBool;
	private int deadBool;
	private double dead=0f;
	private Animator animator;
	private float lastStopAnimation;


    void Awake()
    {
        // Set up the references.
		lastStopAnimation = Time.time;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator> ();
		movingBool = Animator.StringToHash ("isMoving");
		attackingBool = Animator.StringToHash ("isAttacking");
		deadBool = Animator.StringToHash ("isDead");

    }


    void Update()
    {
		double lastAttack = GetComponent<EnemyAttack> ().lastAttack;

		float currentDistance = Vector3.Distance (nav.transform.position, player.transform.position);

        if(!animator.GetBool(deadBool))
        {
            if (currentDistance < minimum_distance)
            {
                if (currentDistance <= 140 / 100)
                {
                    // se estiver perto suficiente do jogador
                    animator.SetBool(movingBool, false);
                    animator.SetBool(attackingBool, true);
                    nav.SetDestination(nav.transform.position);
                    lastStopAnimation = Time.time;

                }
                else if (currentDistance > (140 / 100))
                {
                    //está a distância de correr atrás do jogador
                    animator.SetBool(movingBool, false);
                    nav.SetDestination(nav.transform.position);

                    if ((lastAttack + 2.1f) < Time.time)
                    {
                        //já atacou e tem de voltar a seguir o jogador
                        animator.SetBool(attackingBool, false);
                        animator.SetBool(movingBool, true);
                        nav.SetDestination(player.position);

                    }
                    else
                    {
                        //tem de parar porque a animação de correr ainda está a ser executada
                        animator.SetBool(movingBool, false);
                        nav.SetDestination(nav.transform.position);
                    }
                }
            }
            else
            {
                // nao está a distancia minima do jogador
                nav.SetDestination(nav.transform.position);
            }
        }

		

	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "ThrowingRock")
		{
			animator.SetBool (deadBool, true);
		}
	}

    void Die()
    {
        gameObject.SetActive(false);

		GetComponent<AlienSoundsController> ().DieSound ();
    }

}

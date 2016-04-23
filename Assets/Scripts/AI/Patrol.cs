using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    Animator anim;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;
        anim.SetBool("Grounded", true);
        anim.SetBool("Aim", false);
        anim.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    // Update is called once per frame
    void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}

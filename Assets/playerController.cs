using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] Destination;  // Array of destination points
    public Animator animator;

    public float normalSpeed = 3.5f; // Normal walking speed
    public float attackSpeed = 1.5f; // Speed when the agent is attacking

    private Transform currentDestination;

    void Start()
    {
        // Set the agent's speed to normal speed initially
        agent.speed = normalSpeed;

        // Set an initial random destination when the game starts
        SetRandomDestination();
    }

    void Update()
    {
        // Check if the agent has reached the destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Trigger the "attack" animation and slow down the agent when reaching destination
            animator.SetBool("attack", true);
            agent.speed = attackSpeed; // Reduce speed during attack
        }
        else
        {
            // Ensure the "attack" animation is turned off while the agent is moving
            animator.SetBool("attack", false);
            agent.speed = normalSpeed; // Restore normal speed while moving
        }
    }

    // Method to set a random destination from the array
    void SetRandomDestination()
    {
        int randomIndex = Random.Range(0, Destination.Length);  // Get a random index
        currentDestination = Destination[randomIndex];          // Set the current destination
        agent.SetDestination(currentDestination.position);      // Move agent to the random destination
    }
}

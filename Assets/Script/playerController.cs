using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3[] destinations;  // Array of destination points (using Vector3 instead of Transform)
    public Animator animator;

    public float normalSpeed = 3.5f; // Normal walking speed
    public float attackSpeed = 1.5f; // Speed when the agent is attacking

    [SerializeField] private int health = 3; // Player health

    private Vector3 currentDestination;

    void Start()
    {
        // Ensure that the agent and destinations are set
        if (agent == null || destinations.Length == 0)
        {
            Debug.LogError("NavMeshAgent or Destination array not set.");
            return;
        }

        // Set the agent's speed to normal speed initially
        agent.speed = normalSpeed;

        // Set an initial random destination when the game starts
        SetRandomDestination();
    }

    void Update()
    {
        if (agent == null) return;

        // Check if the agent has reached the destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Trigger the "attack" animation and slow down the agent when reaching the destination
            animator.SetBool("attack", true);
            agent.speed = attackSpeed; // Reduce speed during attack
        }
        else
        {
            // Ensure the "attack" animation is turned off while the agent is moving
            animator.SetBool("attack", false);
            agent.speed = normalSpeed; // Restore normal speed while moving
        }

        // Optionally, check if we should move to a new random destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !animator.GetBool("attack"))
        {
            SetRandomDestination(); // Pick another random destination after the attack
        }
    }

    // Method to set a random destination from the array of Vector3 positions
    void SetRandomDestination()
    {
        if (destinations.Length == 0) return; // Avoid error if no destinations are set

        int randomIndex = Random.Range(0, destinations.Length);  // Get a random index
        currentDestination = destinations[randomIndex];          // Set the current destination
        agent.SetDestination(currentDestination);                 // Move agent to the random destination
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Check if the collided object is tagged as "Bullet"
        {
            Debug.Log("Hit by bullet!");
            health--;

            if (health <= 0) // If health reaches zero, destroy the player
            {
                Debug.Log("Player's health is 0");
                GameEvents.eventss.bullethiting(); // using observer pattern
                Destroy(gameObject); // Destroy this player object
            }
        }
    }
}

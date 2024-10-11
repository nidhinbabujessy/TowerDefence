using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3[] destinations;
    [SerializeField] private Animator animator;
    [SerializeField] private int health = 3;
    [SerializeField] private float normalSpeed = 1f;
   
    private const float attackSpeed = 1.5f;

    private void Start()
    {
        if (agent == null || destinations.Length == 0)
        {
            Debug.LogError("Missing components or destinations.");
            return;
        }

        agent.speed = normalSpeed;
        SetRandomDestination();
    }

    private void Update()
    {
        HandleMovement();
        HandleHealth();
    }

    private void HandleMovement()
    {
        if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance) return;

        // Animation and speed adjustment
        animator.SetBool("attack", true);
        agent.speed = attackSpeed;

        if (agent.remainingDistance <= agent.stoppingDistance && !animator.GetBool("attack"))
        {
            SetRandomDestination();
        }
    }

    private void HandleHealth()
    {
        // Handle player health logic
    }

    private void SetRandomDestination()
    {
        int randomIndex = Random.Range(0, destinations.Length);
        agent.SetDestination(destinations[randomIndex]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Debug.Log("Player defeated.");
            Score.Instance.ScoreIncrease();
            Destroy(gameObject);
        }
    }
}

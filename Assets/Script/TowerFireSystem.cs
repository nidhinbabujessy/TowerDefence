using UnityEngine;

public class TowerFireSystem : MonoBehaviour
{
    public GameObject bulletPrefab;       // Bullet prefab to shoot
    public Transform firePoint;           // Point from where the bullet will be shot
    public float fireRate = 1f;           // Time between shots
    public float fireRadius = 0.54f;      // Radius to detect enemies
    public LayerMask enemyLayer;          // Layer mask to filter enemies
    public float bulletSpeed = 10f;       // Speed of the fired bullet

    private float nextFireTime = 0f;      // Timer for firing control
    private Transform targetEnemy = null; // Targeted enemy

    void Update()
    {
        FindTargetEnemy();

        // If there's a target enemy and it's time to fire
        if (targetEnemy != null && Time.time >= nextFireTime)
        {
            ShootAtEnemy();
            nextFireTime = Time.time + 1f / fireRate; // Reset fire time
        }
    }

    // Method to find the nearest enemy within the fire radius
    void FindTargetEnemy()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, fireRadius, enemyLayer);

        if (hitEnemies.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;

            // Loop through all enemies in range and find the closest one
            foreach (Collider enemy in hitEnemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.transform;
                }
            }

            targetEnemy = closestEnemy;

            // Look at the closest enemy
            if (targetEnemy != null)
            {
                Vector3 direction = (targetEnemy.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
        else
        {
            targetEnemy = null; // No enemies in range
        }
    }

    // Method to shoot a bullet at the target enemy
    void ShootAtEnemy()
    {
        // Instantiate bullet and set its direction towards the target
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null && targetEnemy != null)
        {
            Vector3 direction = (targetEnemy.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }

    // Draw the detection radius in the editor for visualization
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }
}

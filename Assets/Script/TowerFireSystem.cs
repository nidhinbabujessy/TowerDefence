using UnityEngine;

public class TowerFireSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireRadius = 0.54f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float bulletSpeed = 10f;

    private float nextFireTime;
    private Transform targetEnemy;

    private void Update()
    {
        FindTargetEnemy();
        TryToShoot();
    }

    private void FindTargetEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, fireRadius, enemyLayer);
        if (enemiesInRange.Length == 0) return;

        targetEnemy = GetClosestEnemy(enemiesInRange);
        RotateTowardsTarget();
    }

    private Transform GetClosestEnemy(Collider[] enemiesInRange)
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closest = enemy.transform;
                closestDistance = distance;
            }
        }

        return closest;
    }

    private void RotateTowardsTarget()
    {
        if (targetEnemy == null) return;

        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    private void TryToShoot()
    {
        if (targetEnemy != null && Time.time >= nextFireTime)
        {
            ShootAtTarget();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void ShootAtTarget()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (targetEnemy.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }
}

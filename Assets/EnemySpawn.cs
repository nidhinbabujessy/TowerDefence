using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int totalEnemiesToSpawn = 10;  // Set a default value for the total number of enemies
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 1f;  // Time between spawns
    [SerializeField] private GameObject playButton;

    private int spawnedEnemyCount = 0;  // Track how many enemies have been spawned

    public void StartSpawning()
    {
      // TowerPlacement.Instance.DisableTowerPlacement();
        // Deactivate the Play button to prevent multiple spawns
        playButton.SetActive(false);

        // Start the coroutine that spawns enemies over time
        StartCoroutine(SpawnEnemiesCoroutine());
    } 

    // Coroutine to spawn enemies at regular intervals
    private IEnumerator SpawnEnemiesCoroutine()
    {
        // Spawn enemies until the specified limit is reached
        while (spawnedEnemyCount < totalEnemiesToSpawn)
        {
            // Instantiate the enemy at the spawn point
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Increment the count of spawned enemies
            spawnedEnemyCount++;

            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }

        // Optional: Add logic here if you want something to happen when all enemies are spawned
        Debug.Log("All enemies spawned!");
    }
}

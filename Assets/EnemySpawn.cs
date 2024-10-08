using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;    // Enemy prefab to spawn
    [SerializeField] int enemyCount;  // Total number of enemies to spawn
    [SerializeField] Transform spawnPosition;  // Position to spawn enemies

    private int currentEnemyCount = 0;  // To keep track of how many enemies have spawned

    public void SpawnEnemy()
    {
        // Start spawning enemies when the game starts
        StartCoroutine(SpawnEnemy2());
    } 

    IEnumerator SpawnEnemy2()
    {
        while (currentEnemyCount < enemyCount)  // Spawn until the specified number of enemies are reached
        {
            // Spawn an enemy at the given position
            Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);

            currentEnemyCount++;  // Increment the enemy count

            // Wait for 1 second before spawning the next enemy
            yield return new WaitForSeconds(1f);
        }
    }
}

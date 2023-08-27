using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : WaveObserver
{
    public GameObject[] enemyPrefabs;
    public Transform spawnArea;
    public float cooldownTimer;

    private int numberOfEnemiesSpawned;
    private bool hasSpawned;
    private WaveManager waveManager;
    private GameObject[] enemies;
    public GameObject coolDownTimer;
    Vector3 spawnPosition;

    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        hasSpawned = false;
        numberOfEnemiesSpawned = 0;
        cooldownTimer = 10f;
    }

    void Update()
    {
        if (!hasSpawned)
        {
            // Spawn one enemy of each type
            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                SpawnEnemy(enemyPrefabs[i]);
                numberOfEnemiesSpawned++;
            }

            // If there are still enemies to spawn, use RNG to spawn the rest
            while (numberOfEnemiesSpawned < waveManager.enemyNumber)
            {
                int enemyToSpawn = Random.Range(0, enemyPrefabs.Length);
                SpawnEnemy(enemyPrefabs[enemyToSpawn]);
                numberOfEnemiesSpawned++;
            }

            hasSpawned = true;
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        coolDownTimer.GetComponent<TextMeshProUGUI>().text = ((int)cooldownTimer).ToString();

        if (hasSpawned)
        {
            if (enemies.Length <= 0)
            {
                coolDownTimer.SetActive(true);
                cooldownTimer -= Time.deltaTime;
            }

            if (cooldownTimer <= 0)
            {
                hasSpawned = false;
                cooldownTimer = 10f;
                coolDownTimer.SetActive(false);
                numberOfEnemiesSpawned = 0;
                notifyChangeWave();
                notifyChangeEnemyCount();
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        
        bool isValidSpawnPosition = false;
        int maxAttempts = 10; // To avoid infinite loops if no valid spawn position is found.

        for (int i = 0; i < maxAttempts; i++)
        {
            // Generate a random spawn position within the spawn area.
            spawnPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                spawnArea.position.y,
                Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
            );

            // Check if the spawn position is clear of other enemies and the player.
            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 1.0f); // Adjust the radius as needed.

            bool isClear = true;

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy") || col.CompareTag("Player"))
                {
                    isClear = false;
                    break;
                }
            }

            if (isClear)
            {
                isValidSpawnPosition = true;
                break; 
            }
        }

        if (isValidSpawnPosition)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = spawnArea;
        }
        
    }

}

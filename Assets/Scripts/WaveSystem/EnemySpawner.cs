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
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
            spawnArea.position.y,
            Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
        );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.transform.parent = spawnArea;
    }
}

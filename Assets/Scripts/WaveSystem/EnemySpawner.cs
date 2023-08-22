using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : WaveObserver
{
    public GameObject[] enemyPrefabs;



    public Transform spawnArea;
    float cooldownTimer;
    int numberOfEnemiesSpawned;
    bool hasSpwned;

    [HideInInspector]
    public WaveManager waveManager;

    [HideInInspector]
    public GameObject[] enemies;

    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        hasSpwned = false;
        numberOfEnemiesSpawned = 0;
        cooldownTimer = 10f;

    }
    

    void Update()
    {
        if (!hasSpwned)
        {
            int enemyToSpawn = Random.Range(0,enemyPrefabs.Length);
            SpawnEnemy(enemyPrefabs[enemyToSpawn]);
            numberOfEnemiesSpawned++;

            if (numberOfEnemiesSpawned >= waveManager.enemyNumber)
            { 
                hasSpwned=true;
            }
            
        }


        enemies = GameObject.FindGameObjectsWithTag("Enemy");


       
       

        if (hasSpwned) 
        {

            if (enemies.Length <= 0)
            { 
                cooldownTimer -= Time.deltaTime; 
            }
   
            

            if (cooldownTimer <= 0)
            {
                hasSpwned = false;
                cooldownTimer = 10f;
                numberOfEnemiesSpawned = 0;
                notifyChangeWave();
                notifyChangeEnemyCount();

            }


        }
        
    }
    void SpawnEnemy(GameObject enemyPrefab)
    {

        Debug.Log("SpawnEnemy function called");

        Vector3 spawnPosition = new Vector3(
        Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
        spawnArea.position.y,
        Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
        );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.transform.parent = spawnArea;

        

        

    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : WaveObserver
{
    public GameObject[] enemyPrefabs;



    public Transform spawnArea;
    
    [HideInInspector]
    public float cooldownTimer;

    int numberOfEnemiesSpawned;
    bool hasSpwned;

    [HideInInspector]
    public WaveManager waveManager;

    [HideInInspector]
    public GameObject[] enemies;

    public GameObject coolDownTimer;

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


       coolDownTimer.GetComponent<TextMeshProUGUI>().text = ((int)cooldownTimer).ToString();
       

        if (hasSpwned) 
        {

            if (enemies.Length <= 0)
            {
                coolDownTimer.SetActive(true);
                cooldownTimer -= Time.deltaTime; 
            }
   
            

            if (cooldownTimer <= 0)
            {
                hasSpwned = false;
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

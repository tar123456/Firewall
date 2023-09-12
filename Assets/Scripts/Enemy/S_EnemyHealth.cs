using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_EnemyHealth : WaveObserver
{
    public float currentHealth;

  
    public string EnemyType;
    public GameObject PowerUp;
    public float probablity;
    float randomValue;
    public GameObject destroy;



    private void Update()
    {
        
        if (currentHealth <= 0)
        {
            if (SceneManager.GetActiveScene().name != "TutorialScene")
            {
                notifyChangeScore(EnemyType);
                Debug.Log("Notified for score");
            }

            AudioManager.instance.playSound("Enemy Defeat");
            SpawnObject();
            SpawnDestroy();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            currentHealth--;

        }
    }


    private void SpawnObject()
    {
         randomValue = Random.value;

        if (randomValue < probablity && SceneManager.GetActiveScene().name != "TutorialScene")
        {
            Instantiate(PowerUp, transform.position, Quaternion.identity);
        }
        
    }
    private void SpawnDestroy()
    {
        Instantiate(destroy, transform.position, Quaternion.identity);
    }


    

}

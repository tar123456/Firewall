using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyHealth : WaveObserver
{
    public float currentHealth;

  
    public string EnemyType;

    

    private void Update()
    {
        
        if (currentHealth <= 0)
        {
            notifyChangeScore(EnemyType);
         
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


    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    
    void Start()
    {
       
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log(currentHealth);
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

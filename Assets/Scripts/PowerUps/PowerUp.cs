using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [HideInInspector]
    public GameObject healthBar;

    float timer;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<S_PlayerHealth>().currentHealth<3)
            {
                other.gameObject.GetComponent<S_PlayerHealth>().currentHealth++;
                healthBar.GetComponent<HeartHealth>().ModifyHealth(1);
                AudioManager.instance.playSound("Power Up");
                Destroy(gameObject);

            }

            

        }
    }
}

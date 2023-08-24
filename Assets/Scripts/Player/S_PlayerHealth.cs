using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    float currentHealth= 0;

    public GameObject gameOverObject;

    public float maxHealth;
    public bool gameOver;
    public GameObject healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        
        healthBar.GetComponent<HeartHealth>().maxHealth = (int)maxHealth;
        healthBar.GetComponent<HeartHealth>().currentHealth = healthBar.GetComponent<HeartHealth>().maxHealth;
        Debug.Log(currentHealth);
        healthBar.GetComponent<HeartHealth>().UpdateHealth();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameOverObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            gameOver = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            currentHealth--;
            healthBar.GetComponent<HeartHealth>().currentHealth = (int)currentHealth+1;
            healthBar.GetComponent<HeartHealth>().ModifyHealth(-1);
        }
    }
}

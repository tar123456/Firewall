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

    void Start()
    {
        currentHealth = maxHealth;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameOverObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("GameOver");
            gameOver = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            currentHealth--;

            Debug.Log(currentHealth);
        }
    }
}

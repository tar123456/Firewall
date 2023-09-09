using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerHealth : MonoBehaviour
{
    [HideInInspector]
    public float currentHealth = 0;

    public GameObject gameOverObject;
    public GameObject ControlObject;
    public float maxHealth;
    public bool gameOver;
    public GameObject healthBar;

    private Material originalMaterial;
    private Renderer renderer;
    private bool isTakingDamage = false;
    private float damageDuration = 0.1f;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;

        healthBar.GetComponent<HeartHealth>().maxHealth = (int)maxHealth;
        healthBar.GetComponent<HeartHealth>().currentHealth = healthBar.GetComponent<HeartHealth>().maxHealth;
        healthBar.GetComponent<HeartHealth>().UpdateHealth();
        gameOver = false;

        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
        originalColor = originalMaterial.color; // Store the original color
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            gameOverObject.SetActive(true);
            ControlObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            gameOver = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            currentHealth--;
            healthBar.GetComponent<HeartHealth>().currentHealth = (int)currentHealth + 1;
            healthBar.GetComponent<HeartHealth>().ModifyHealth(-1);

            if (!isTakingDamage)
            {
                // Change the material color to red temporarily
                renderer.material.color = Color.red;

                // Start a coroutine to return the color to the original after a brief duration
                StartCoroutine(ReturnToOriginalColor());
            }

            if (currentHealth > 0)
            {
                AudioManager.instance.playSound("Player hurt");
            }
            else if (currentHealth == 0)
            {
                AudioManager.instance.playSound("Game Over");
            }
        }
    }

    private IEnumerator ReturnToOriginalColor()
    {
        isTakingDamage = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(damageDuration);

        // Restore the original material color
        renderer.material.color = originalColor;

        isTakingDamage = false;
    }
}

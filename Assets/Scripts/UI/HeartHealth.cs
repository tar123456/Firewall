using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealth : MonoBehaviour
{
    public Image[] health;
    public int maxHealth;
    public int currentHealth;
   

   
    public void UpdateHealth()
    {
        for (int i = 0; i < health.Length; i++)
        {
            if (i < currentHealth)
            {
               
                health[i].enabled = true;
            }
            else
            {
               
                health[i].enabled = false;
            }
        }
    }

    public void ModifyHealth(int amount)
    {
        Debug.Log("Called");
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealth();
    }
}

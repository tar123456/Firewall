using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{

    public TextMeshProUGUI text;

    private void Update()
    {
        if (PlayerPrefs.HasKey("Auto-Shoot"))
        {
            if (PlayerPrefs.GetInt("Auto-Shoot") == 0)
            {
                text.text = "Auto-Shoot: off";
            }
            else if (PlayerPrefs.GetInt("Auto-Shoot")==1)
            {
                text.text = "Auto-Shoot: on";
            }
        }
        else 
        {
            text.text = "Auto-Shoot: off";
        }
        
    }
}

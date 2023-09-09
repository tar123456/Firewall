using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideShoot : MonoBehaviour
{
    void Update()
    {
        if (PlayerPrefs.HasKey("Auto-Shoot") && PlayerPrefs.GetInt("Auto-Shoot") == 1)
        {
            gameObject.SetActive(false);
        }

        else if (!PlayerPrefs.HasKey("Auto-Shoot") || PlayerPrefs.GetInt("Auto-Shoot") == 0)
        {
            gameObject.SetActive(true);
        }

    }
}

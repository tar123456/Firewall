using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void AutoShoot()
    {
        if (!PlayerPrefs.HasKey("Auto-Shoot") || PlayerPrefs.GetInt("Auto-Shoot") == 0)
        {
            PlayerPrefs.SetInt("Auto-Shoot", 1);
        }
        else if (PlayerPrefs.HasKey("Auto-Shoot") && PlayerPrefs.GetInt("Auto-Shoot") == 1)
        {
            PlayerPrefs.SetInt("Auto-Shoot", 0);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void loadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}

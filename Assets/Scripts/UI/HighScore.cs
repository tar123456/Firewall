using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public static HighScore instance { get; private set; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int getHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
        
            int hightscore =  PlayerPrefs.GetInt("HighScore");
            return hightscore;
        }
        else
        {
            return 0;
        }
        
    }
    public void setHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore",score);
        return;
    }
}

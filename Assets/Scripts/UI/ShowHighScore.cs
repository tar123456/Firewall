using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    
    void Start()
    {
       highScoreText.text =  HighScore.instance.getHighScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveNumber : MonoBehaviour
{
    TextMeshProUGUI text;

    public WaveManager waveManager;

    public int textType;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {

        switch (textType)
        {
            case 1:
                text.text = waveManager.waveNo.ToString();
                break;
            case 2:
                text.text = waveManager.score.ToString();
                break;

        }

    }
}

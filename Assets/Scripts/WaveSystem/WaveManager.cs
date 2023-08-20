using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour,IWaveObserver
{
    [SerializeField] WaveObserver waveObserver;

    [HideInInspector]
    public int waveNo;
    [HideInInspector]
    public int enemyNumber;
    [HideInInspector]
    public int score;


    int initialEnemyNumber;

    Dictionary<string, int> enemyTypes = new Dictionary<string, int>();

    private void Awake()
    {
        enemyTypes.Add("Tower", 1);
        enemyTypes.Add("Cylenderical",2);
        enemyTypes.Add("Mover",3);
        
    }
    private void Start()
    {
        waveNo = 1;
        initialEnemyNumber = 5;
        enemyNumber = initialEnemyNumber;
        score = 0;
    }

    private void OnEnable()
    {
        waveObserver.addObserver(this);
    }
    private void OnDisable()
    {
       waveObserver.removeObserver(this);
    }


    

    void IWaveObserver.ChangeWave()
    {
        waveNo++;
    }

    void IWaveObserver.ChangeEnemyCount()
    {
        enemyNumber = initialEnemyNumber + (2*(waveNo-1)); 
    }

    void IWaveObserver.ChangeScore(string EnemyType)
    {
        int type = enemyTypes[EnemyType];
        switch (type)
        {
            case 1:
                score += 10;
                break;
            case 2:
                score += 20; 
                break;
            case 3:
                score += 30;
                break;
        };
    }
}

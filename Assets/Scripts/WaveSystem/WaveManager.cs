using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [HideInInspector]
    public List<GameObject> enemies;

    List<GameObject> enemiesToRemove;

    Dictionary<string, int> enemyTypes = new Dictionary<string, int>();

    private void Awake()
    {
        enemyTypes.Add("Tower", 1);
        enemyTypes.Add("Cylenderical", 2);
        enemyTypes.Add("Mover", 3);
        
    }
    private void Start()
    {
        waveNo = 1;
        initialEnemyNumber = 3;
        enemyNumber = initialEnemyNumber;
        score = 0;
        enemiesToRemove = new List<GameObject>();
    }

    private void OnEnable()
    {
        waveObserver.addObserver(this);
    }
    private void OnDisable()
    {
       waveObserver.removeObserver(this);
    }

    private void Update()
    {
        Debug.Log(score);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                enemy.GetComponent<S_EnemyHealth>().addObserver(this);
            }
        }

       
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
               
                enemiesToRemove.Add(enemies[i]);
                enemies.RemoveAt(i); 
            }
        }

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
                score += 20;
                break;
            case 2:
                score += 30; 
                break;
            case 3:
                score += 60;
                break;
        };
        if (score > HighScore.instance.getHighScore())
        {
            HighScore.instance.setHighScore(score);
            Debug.Log("HighScore Set");
            return;
        }
        else
        {
            return;
        }
    }
   
   
}

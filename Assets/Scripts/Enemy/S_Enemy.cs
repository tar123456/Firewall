using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy : MonoBehaviour
{

    public Transform[] spawnpoints;
    public GameObject Projectile;
    public float spawnSpeed;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if (timer > spawnSpeed)
        {
            foreach (var spawnpoint in spawnpoints)
            {
                Instantiate(Projectile, spawnpoint.position, spawnpoint.rotation);
            }
            timer = 0; 
        }

    }
}

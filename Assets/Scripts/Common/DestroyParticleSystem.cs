using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer +=Time.deltaTime;
        if (timer >= 0.5)
        {
            Destroy(gameObject);
        }
    }
}

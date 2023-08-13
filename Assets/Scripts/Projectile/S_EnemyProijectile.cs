using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyProijectile : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;
    float movementSpeed;
    void Start()
    {
        timer = 0;
        movementSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position += transform.right * Time.deltaTime * movementSpeed;

        if (timer >= 2)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

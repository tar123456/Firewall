using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Projectile : MonoBehaviour
{

    // Start is called before the first frame update
    float timer;
    float movementSpeed;
    bool canCheckCollision;

    void Start()
    {
        timer = 0;
        movementSpeed = 10;


        StartCoroutine(EnableCollisionCheck());
    }

    IEnumerator EnableCollisionCheck()
    {
        // Wait for a short delay before enabling collision checks
        yield return new WaitForSeconds(0.1f);
        canCheckCollision = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position += transform.forward * Time.deltaTime * movementSpeed;

        if (timer >=5) 
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (!canCheckCollision)
            return;
 
        Destroy(gameObject);
        

    }
}

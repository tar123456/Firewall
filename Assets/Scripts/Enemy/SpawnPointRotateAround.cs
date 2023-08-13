using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointRotateAround : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform OrbitCenter;
    public float RotateSpeed = 20f;
    

    // Update is called once per frame
    void Update()
    {
        if (OrbitCenter != null)
        {
            transform.RotateAround(OrbitCenter.position,Vector3.up,RotateSpeed*Time.deltaTime);
        }
    }
}

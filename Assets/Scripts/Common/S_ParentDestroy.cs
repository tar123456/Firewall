using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ParentDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    int childTransform;

    // Update is called once per frame
    void Update()
    {
        childTransform = transform.childCount;

        if (childTransform <= 0)
        {
            Destroy(gameObject);
        }
    }
}

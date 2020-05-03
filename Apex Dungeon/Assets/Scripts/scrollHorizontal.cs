using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollHorizontal : MonoBehaviour
{
    public float speed = 1;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
        if (transform.position.x > 1280*1.5f)
        {
            transform.position = new Vector3(0 - (1280/2), transform.position.y, 0);
        }
    }
}

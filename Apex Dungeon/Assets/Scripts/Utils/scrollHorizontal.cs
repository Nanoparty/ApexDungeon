using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollHorizontal : MonoBehaviour
{
    public float speed = 1;
    public Vector3 resetPosition;
    public float endPosition = 1280;
    
    void Start()
    {
        resetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject cloud = GetComponent().rect.width;
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
        if (transform.position.x > endPosition*1.5f)
        {
            transform.position = resetPosition;
        }
    }
}

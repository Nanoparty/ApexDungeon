using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollHorizontal : MonoBehaviour
{
    public float speed = 1;
    public Vector3 resetPosition;
    public float endPosition;
    
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + speed * Time.deltaTime, transform.localPosition.y, 0);
        if (transform.localPosition.x > endPosition)
        {
            transform.localPosition = resetPosition;
        }
    }
}

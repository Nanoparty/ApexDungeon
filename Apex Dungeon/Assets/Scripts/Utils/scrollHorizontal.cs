using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollHorizontal : MonoBehaviour
{
    public float speed = 1;
    public Vector3 resetPosition;
    public float endPosition;
    
    void Start()
    {
        //resetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject cloud = GetComponent().rect.width;
        transform.localPosition = new Vector3(transform.localPosition.x + speed * Time.deltaTime, transform.localPosition.y, 0);
        Debug.Log(transform.localPosition.x);
        if (transform.localPosition.x > endPosition)
        {
            transform.localPosition = resetPosition;
        }
    }
}

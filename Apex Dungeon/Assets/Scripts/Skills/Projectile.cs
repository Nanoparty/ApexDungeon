using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Sprite image;
    public GameObject explosion;

    public Vector2 targetPosition;
    public bool target;
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        if (target)
        {

                transform.position =Vector3.Lerp(
                    transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPosition) < 0.5)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(int row, int col)
    {
        targetPosition = new Vector2(col, row);
        target = true;
    }
}

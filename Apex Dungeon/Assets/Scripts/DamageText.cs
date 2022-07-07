using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float speed = 0.01f;
    public float maxDistance = 0.5f;
    public float start;
    public float lifespan = 0;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0);
        if (lifespan > 0.8f)
        {
            Destroy(this.gameObject);
        }
    }
}

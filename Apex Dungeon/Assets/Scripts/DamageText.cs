using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float speed = 0.7f;
    private float maxDistance = 0.5f;
    private float start;

    void Start()
    {
        start = transform.localPosition.y;
    }

    void Update()
    {
        if (GameManager.gmInstance.Dungeon == null) return;

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime, 0);
        if (transform.localPosition.y - start > maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
}

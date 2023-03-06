using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    Vector3 localScale;
    float width;
    float height;
    float initial;

    void Start()
    {
        localScale = transform.localScale;
        initial = localScale.x;
    }

    void Update()
    {
        MovingEntity parent = transform.parent.gameObject.GetComponent<MovingEntity>();
        localScale.x =(float) initial*((float)parent.GetHP() / (float)parent.GetMaxHP());
        transform.localScale = localScale;
    }
}

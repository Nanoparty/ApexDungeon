using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    Vector3 localScale;
    float width;
    float height;
    float initial;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        initial = localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        MovingEntity parent = transform.parent.gameObject.GetComponent<MovingEntity>();
        localScale.x =(float) initial*((float)parent.getHP() / (float)parent.getMaxHP());
        //Debug.Log(localScale.x);
        transform.localScale = localScale;
    }
}

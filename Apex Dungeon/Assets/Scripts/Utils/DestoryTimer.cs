using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTimer : MonoBehaviour
{
    public float time = 2f;

    private void Start()
    {
        Destroy(this.gameObject, time);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud_scroll : MonoBehaviour
{
    float scrollSpeed = 0.1f;
    MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}

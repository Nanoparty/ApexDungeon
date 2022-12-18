using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    private void Start()
    {
        scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}

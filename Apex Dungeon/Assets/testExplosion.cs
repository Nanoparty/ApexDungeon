using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testExplosion : MonoBehaviour
{
    public ParticleSystem explosion;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            explosion.Play();
            //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        }
    }
}

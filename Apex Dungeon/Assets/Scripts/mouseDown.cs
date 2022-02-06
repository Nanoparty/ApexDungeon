using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDown : MonoBehaviour
{
    public bool mouseD = false;
    private void OnMouseDown()
    {
        mouseD = true;
    }
}

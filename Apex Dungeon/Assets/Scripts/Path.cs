using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public Queue<Vector2> nodes;

    public Path()
    {
        nodes = new Queue<Vector2>();
    }
}

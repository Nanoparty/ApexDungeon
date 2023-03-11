using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class expression_test : MonoBehaviour
{

    public GameObject first(int i)
    {
        Debug.Log("FIRST");
        return new GameObject();
    }

    public GameObject second(int i)
    {
        Debug.Log("SECOND");
        return new GameObject();
    }
    
    public void ExecuteExpressions()
    {
        List<Expression<Func<GameObject>>> shields = new List<Expression<Func<GameObject>>>();
        shields.Add(() => first(1));
        shields.Add(() => second(1));


        Expression<Func<GameObject>> function = shields[UnityEngine.Random.Range(0, shields.Count)];
        GameObject o = function.Compile().Invoke();
        Debug.Log("FINISHED");
    }
}

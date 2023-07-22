using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectContainer", menuName = "ScriptableObjects/Object Container")]
public class ObjectContainer : ScriptableObject
{
    public GameObject pickup;
    public GameObject gold;
}

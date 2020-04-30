using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion :  Item
{
    public HealthPotion()
    {

    }

    public HealthPotion(Sprite s)
    {
        img = s;
    }

    public override void useItem()
    {
        Debug.Log("USE HEALTH POTION");
    }
}

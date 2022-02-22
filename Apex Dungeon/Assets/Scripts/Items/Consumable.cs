using System;
using UnityEngine;

public class Consumable : Item
{

    public void SetStats(string n, string f, string d, Sprite s)
    {
        itemName = n;
        flavorText = f;
        description = d;
        image = s;
    }

    public override void Create()
    {
        
    }

    public override void UseItem()
    {
        
    }
}

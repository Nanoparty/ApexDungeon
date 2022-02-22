using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion :  Item
{
    private void Start()
    {
        itemName = "Health Potion";
        flavorText = "Tastes like cherry!";
        description = "Instantly recovers 20 hp";
    }

    public HealthPotion(Sprite s)
    {
        image = s;
        itemName = "Health Potion";
        flavorText = "Tastes like cherry!";
        description = "Instantly recovers 20 hp";
    }

    public override void UseItem()
    {
        Debug.Log("USE HEALTH POTION");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        p.takeDamage(20);
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }

 
}

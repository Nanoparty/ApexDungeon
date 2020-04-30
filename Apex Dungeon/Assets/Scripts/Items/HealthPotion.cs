using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion :  Item
{



    private void Start()
    {
        name = "Health Potion";
        flavor = "Tastes like cherry!";
        details = "Instantly recovers 20 hp";
    }



    public HealthPotion(Sprite s)
    {
        img = s;
    }

    public override void useItem()
    {
        Debug.Log("USE HEALTH POTION");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        p.takeDamage(20);
    }
}

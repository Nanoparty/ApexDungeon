using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Item
{

    private void Start()
    {
        name = "Mana Potion";
        flavor = "Tastes like Blueberry!";
        details = "Instantly recovers 20 mp";
    }


    public override void useItem()
    {
        Debug.Log("USE MANA POTION");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        p.takeDamage(20);
        p.addMP(20);
    }
}

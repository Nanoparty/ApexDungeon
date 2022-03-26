using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Item
{

    private void Start()
    {
        itemName = "Mana Potion";
        flavorText = "Tastes like Blueberry!";
        description = "Instantly recovers 20 mp";
    }


    public override void UseItem()
    {
        Debug.Log("USE MANA POTION");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.takeDamage(20);
        p.addMP(20);
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

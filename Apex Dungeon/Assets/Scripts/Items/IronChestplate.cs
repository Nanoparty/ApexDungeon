using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronChestplate : Item
{
    private void Start()
    {
        name = "Iron Chestplate";
        flavor = "Gives the appearance of abs";
        details = "Standard issue chestplate that offers +50 defense";
    }

    public IronChestplate(Sprite s)
    {
        img = s;
        name = "Iron Chestplate";
        flavor = "Gives the appearance of abs";
        details = "Standard issue chestplate that offers +50 defense";
    }

    public override void useItem()
    {
        Debug.Log("Equip Iron Chestplate");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }
}

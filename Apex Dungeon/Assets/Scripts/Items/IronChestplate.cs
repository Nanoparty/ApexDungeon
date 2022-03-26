using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronChestplate : Item
{
    private void Start()
    {
        itemName = "Iron Chestplate";
        flavorText = "Gives the appearance of abs";
        description = "Standard issue chestplate that offers +50 defense";
    }

    public IronChestplate(Sprite s)
    {
        image = s;
        itemName = "Iron Chestplate";
        flavorText = "Gives the appearance of abs";
        description = "Standard issue chestplate that offers +50 defense";
    }

    public override void UseItem()
    {
        Debug.Log("Equip Iron Chestplate");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

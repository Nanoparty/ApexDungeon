using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBoots : Item
{
    private void Start()
    {
        itemName = "Iron Boots";
        flavorText = "Can't stub your toe";
        description = "Standard issue boots that offers +15 defense";
    }

    public IronBoots(Sprite s)
    {
        image = s;
        itemName = "Iron Boots";
        flavorText = "Can't stub your toe";
        description = "Standard issue boots that offers +15 defense";
    }

    public override void UseItem()
    {
        Debug.Log("Equip Iron Boots");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

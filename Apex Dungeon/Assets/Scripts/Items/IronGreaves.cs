using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGreaves : Item
{
    private void Start()
    {
        itemName = "Iron Greaves";
        flavorText = "Junk in the trunk";
        description = "Standard issue greaves that offers +40 defense";
    }

    public IronGreaves(Sprite s)
    {
        image = s;
        itemName = "Iron Greaves";
        flavorText = "Junk in the trunk";
        description = "Standard issue greaves that offers +40 defense";
    }

    public override void UseItem()
    {
        Debug.Log("Equip Iron Greaves");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

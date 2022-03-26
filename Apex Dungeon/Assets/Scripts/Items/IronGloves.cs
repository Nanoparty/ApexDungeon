using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGloves : Item
{
    private void Start()
    {
        itemName = "Iron Gloves";
        flavorText = "Firm Handshakes";
        description = "Standard issue gloves that offers +20 defense";
    }

    public IronGloves(Sprite s)
    {
        image = s;
        itemName = "Iron Gloves";
        flavorText = "Firm Handshakes";
        description = "Standard issue gloves that offers +20 defense";
    }

    public override void UseItem()
    {
        Debug.Log("Equip Iron Gloves");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

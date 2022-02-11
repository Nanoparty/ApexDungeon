using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGloves : Item
{
    private void Start()
    {
        name = "Iron Gloves";
        flavor = "Firm Handshakes";
        details = "Standard issue gloves that offers +20 defense";
    }

    public IronGloves(Sprite s)
    {
        img = s;
        name = "Iron Gloves";
        flavor = "Firm Handshakes";
        details = "Standard issue gloves that offers +20 defense";
    }

    public override void useItem()
    {
        Debug.Log("Equip Iron Gloves");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }
}

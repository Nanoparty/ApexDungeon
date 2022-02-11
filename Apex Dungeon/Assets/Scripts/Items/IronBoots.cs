using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBoots : Item
{
    private void Start()
    {
        name = "Iron Boots";
        flavor = "Can't stub your toe";
        details = "Standard issue boots that offers +15 defense";
    }

    public IronBoots(Sprite s)
    {
        img = s;
        name = "Iron Boots";
        flavor = "Can't stub your toe";
        details = "Standard issue boots that offers +15 defense";
    }

    public override void useItem()
    {
        Debug.Log("Equip Iron Boots");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }
}

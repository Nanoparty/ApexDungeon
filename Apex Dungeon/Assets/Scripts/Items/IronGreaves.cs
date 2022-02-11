using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGreaves : Item
{
    private void Start()
    {
        name = "Iron Greaves";
        flavor = "Junk in the trunk";
        details = "Standard issue greaves that offers +40 defense";
    }

    public IronGreaves(Sprite s)
    {
        img = s;
        name = "Iron Greaves";
        flavor = "Junk in the trunk";
        details = "Standard issue greaves that offers +40 defense";
    }

    public override void useItem()
    {
        Debug.Log("Equip Iron Greaves");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }
}

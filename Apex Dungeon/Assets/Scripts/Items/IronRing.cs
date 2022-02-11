using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronRing : Item
{
    private void Start()
    {
        name = "Iron Ring";
        flavor = "Its the thought that counts";
        details = "Basic accessory that offers +10 Intelligence";
    }

    public IronRing(Sprite s)
    {
        img = s;
        name = "Iron Ring";
        flavor = "Its the thought that counts";
        details = "Basic accessory that offers +10 Intelligence";
    }

    public override void useItem()
    {
        Debug.Log("Equip Iron Ring");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }
}

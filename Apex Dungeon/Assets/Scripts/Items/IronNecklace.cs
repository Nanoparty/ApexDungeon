using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronNecklace : Item
{
    private void Start()
    {
        name = "Iron Necklace";
        flavor = "Almost Classy";
        details = "Basic accessory that offers +10 Intelligence";
    }

    public IronNecklace(Sprite s)
    {
        img = s;
        name = "Iron Necklace";
        flavor = "Almost Classy";
        details = "Basic accessory that offers +10 Intelligence";
    }

    public override void useItem()
    {
        Debug.Log("Equip Iron Necklace");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }
}

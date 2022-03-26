using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronNecklace : Item
{
    private void Start()
    {
        itemName = "Iron Necklace";
        flavorText = "Almost Classy";
        description = "Basic accessory that offers +10 Intelligence";
    }

    public IronNecklace(Sprite s)
    {
        image = s;
        itemName = "Iron Necklace";
        flavorText = "Almost Classy";
        description = "Basic accessory that offers +10 Intelligence";
    }

    public override void UseItem()
    {
        Debug.Log("Equip Iron Necklace");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

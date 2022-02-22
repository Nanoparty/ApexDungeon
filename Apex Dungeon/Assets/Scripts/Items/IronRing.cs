using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronRing : Item
{
    private void Start()
    {
        itemName = "Iron Ring";
        flavorText = "Its the thought that counts";
        description = "Basic accessory that offers +10 Intelligence";
    }

    public IronRing(Sprite s)
    {
        image = s;
        itemName = "Iron Ring";
        flavorText = "Its the thought that counts";
        description = "Basic accessory that offers +10 Intelligence";
    }

    public override void UseItem()
    {
        Debug.Log("Equip Iron Ring");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

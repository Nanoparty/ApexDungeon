using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronHelmet : Item
{
    private void Start()
    {
        itemName = "Iron Helmet";
        flavorText = "Should have worn this as a baby";
        description = "Standard issue helmet that offers +25 defense";
    }

    public IronHelmet(Sprite s)
    {
        image = s;
        itemName = "Iron Helmet";
        flavorText = "Should have worn this as a baby";
        description = "Standard issue helmet that offers +25 defense";
    }

    public override void UseItem()
    {
        Debug.Log("Equip Iron Helmet");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

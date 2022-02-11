using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronHelmet : Item
{
    private void Start()
    {
        name = "Iron Helmet";
        flavor = "Should have worn this as a baby";
        details = "Standard issue helmet that offers +25 defense";
    }

    public IronHelmet(Sprite s)
    {
        img = s;
        name = "Iron Helmet";
        flavor = "Should have worn this as a baby";
        details = "Standard issue helmet that offers +25 defense";
    }

    public override void useItem()
    {
        Debug.Log("Equip Iron Helmet");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //p.equip(this)
    }
}

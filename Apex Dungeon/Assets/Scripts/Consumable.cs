using UnityEngine;

public class Consumable : Item
{
    public Consumable(string name, string flavor, string desc, Sprite img){
        itemName = name;
        flavorText = flavor;
        description = desc;
        image = img;
    }
    public void SetStats(string n, string f, string d, Sprite s)
    {
        itemName = n;
        flavorText = f;
        description = d;
        image = s;
    }

    public override void Create()
    {
    }

    public override void UseItem()
    {
        if(itemName == "Health Potion"){
            Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            p.addHP(20);
        }
        if(itemName == "Mana Potion"){
            Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            p.addMP(20);
        }
    }
}

using UnityEngine;

public class ManaPotion : Item
{
    private void Start()
    {
        itemName = "Mana Potion";
        flavorText = "Tastes like Blueberry!";
        description = "Instantly recovers 20 mp";
    }

    public override void UseItem()
    {
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        p.addMP(20);
    }

    public override void Create()
    {
        throw new System.NotImplementedException();
    }
}

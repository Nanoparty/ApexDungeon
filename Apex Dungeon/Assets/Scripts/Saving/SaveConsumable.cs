using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveConsumable
{
    public int itemLevel;
    public int tier;
    public string itemName;
    public string rank;
    public string description;
    public string id;

    public SaveConsumable(Consumable e)
    {
        itemLevel = e.level;
        tier = e.tier;
        itemName = e.itemName;
        rank = e.flavorText;
        description = e.description;
        id = e.id;
    }
}

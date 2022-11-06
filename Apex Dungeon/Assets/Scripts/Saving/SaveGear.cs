using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGear
{
    public int attack;
    public int hpBoost;
    public int crit;
    public int evade;
    public int itemLevel;
    public string type;
    public int tier;
    public string itemName;
    public string rank;
    public string description;
    public string id;
    public bool empty;

    public SaveGear(Equipment e)
    {
        if(e == null)
        {
            empty = true;
            return;
        }
        empty = false;
        attack = e.attack;
        hpBoost = e.defense;
        crit = e.crit;
        evade = e.evade;
        itemLevel = e.level;
        type = e.type;
        tier = e.tier;
        itemName = e.itemName;
        rank = e.flavorText;
        description = e.description;
    }
}

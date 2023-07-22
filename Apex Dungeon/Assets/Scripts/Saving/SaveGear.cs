using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Equipment;

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
    public int spriteIndex;
    public string equipType;
    public int range;
    //public string subType;

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
        spriteIndex = e.spriteIndex;
        equipType = e.etype.ToString();
        range = e.range;
        
        //if (e.etype == EquipType.HELMET)
        //{
        //    subType = ((Helmet)e).helmetType.ToString();
        //}
        //if (e.etype == EquipType.CHESTPLATE)
        //{
        //    subType = ((Chestplate)e).chestplateType.ToString();
        //}
        //if (e.etype == EquipType.SHIELD)
        //{
        //    subType = ((Shield)e).shieldType.ToString();
        //}
        //if (e.etype == EquipType.BOOTS)
        //{
        //    subType = ((Boots)e).bootType.ToString();
        //}
        //if (e.etype == EquipType.GLOVES)
        //{
        //    subType = ((Gloves)e).gloveType.ToString();
        //}
        
    }
}

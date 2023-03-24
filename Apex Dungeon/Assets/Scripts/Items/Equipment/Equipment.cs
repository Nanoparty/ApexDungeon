using UnityEngine;
using System;
using System.Collections.Generic;

public class Equipment : Item
{
    //Parameters
    public string type;
    public EquipType etype = EquipType.NONE;

    //stats
    public int defense;
    public int attack;
    public int intelligence;
    public int crit;
    public int evade;
    public int range;

    public string modifier;

    public List<StatusEffect> inflictions;

    public enum EquipType
    {
        HELMET,
        BOOTS,
        GLOVES,
        CHESTPLATE,
        LEGS,
        NECKLACE,
        RING,
        SHIELD,
        SWORD,
        AXE,
        SPEAR,
        DAGGER,
        BOW,
        WAND,
        NONE
    }

    public virtual Sprite GetImage()
    {
        return null;
    }

    public Equipment(){

    }

    public Equipment(int lvl, string type, int tier, Sprite image, int hpBoost, int attackBoost, int critBoost, int evadeBoost, int range = 1)
    {
        setStats(lvl, type, tier, image, hpBoost, attackBoost, critBoost, evadeBoost, range);
    }

    public Equipment(int lvl, string type, int tier, Sprite image, string name, string description, int hp, int attack, int crit, int evade)
    {
        this.level = lvl;
        this.type = type;
        this.tier = tier;
        this.image = image;
        this.itemName = name;

        string descriptionText = "";

        if (attack > 0) descriptionText += String.Format("\nAttack Damage +{0}", attack);
        if (hp > 0) descriptionText += String.Format("\nHP Increase +{0}", hp);
        if (crit > 0) descriptionText += String.Format("\nCritical Hit Chance +{0}", crit);
        if (evade > 0) descriptionText += String.Format("\nEvade Chance +{0}", evade);

        this.description = descriptionText;
        flavorText = description;
        this.image = image;
    }

    public Equipment(SaveGear gear, ImageLookup il)
    {
        attack = gear.attack;
        defense = gear.hpBoost;
        type = gear.type;
        tier = gear.tier;
        level = gear.itemLevel;
        itemName = gear.itemName;
        description = gear.description;
        flavorText = gear.rank;
        spriteIndex = gear.spriteIndex;
        etype = Enum.Parse<EquipType>(gear.equipType);
        image = il.getEquipmentImage(tier, type);
        range = gear.range;
        
    }

    public Equipment(Equipment e){
        level = e.level;
        type = e.type;
        tier = e.tier;
        defense = e.defense;
        attack = e.attack;
        intelligence = e.intelligence;
        crit = e.crit;
        modifier = e.modifier;
        image = e.image;
        itemName = e.itemName;
        flavorText = e.flavorText;
        description = e.description;
    }

    public void setStats(int lvl, string type, int tier, Sprite img, int hpBoost, int attackDamage, int critBoost, int evadeBoost, int range = 1)
    {
        this.level = lvl;
        this.type = type;
        this.tier = tier;
        this.defense = hpBoost;
        this.attack = attackDamage;
        this.intelligence = 0;
        this.crit = critBoost;
        this.evade = evadeBoost;
        this.range = range;

        string nameText = "";
        if(tier == 1)
        {
            if (type == "helmet") nameText = "Leather Helmet";
            else if (type == "gloves") nameText = "Leather Gloves";
            else if (type == "legs") nameText = "Leather Greaves";
            else if (type == "boots") nameText = "Leather Boots";
            else if (type == "weapon" && range > 1) nameText = "Old Slingshot";
            else if (type == "weapon") nameText = "Rusty Dagger";
            else if (type == "shield") nameText = "Leather Shield";
            else if (type == "necklace") nameText = "Copper Necklace";
            else if (type == "ring") nameText = "Copper Ring";
        }
        else if (tier == 2)
        {
            if (type == "helmet") nameText = "Iron Helmet";
            else if (type == "gloves") nameText = "Iron Gloves";
            else if (type == "legs") nameText = "Iron Greaves";
            else if (type == "boots") nameText = "Iron Boots";
            else if (type == "weapon" && range > 1) nameText = "Shortbow";
            else if (type == "weapon") nameText = "Long Sword";
            else if (type == "shield") nameText = "Iron Shield";
            else if (type == "necklace") nameText = "Iron Necklace";
            else if (type == "ring") nameText = "Iron Ring";
        }
        else if (tier == 3)
        {
            if (type == "helmet") nameText = "Gold Helmet";
            else if (type == "gloves") nameText = "Gold Gloves";
            else if (type == "legs") nameText = "Gold Greaves";
            else if (type == "boots") nameText = "Gold Boots";
            else if (type == "weapon" && range > 1) nameText = "Longbow";
            else if (type == "weapon") nameText = "Battle Hammer";
            else if (type == "shield") nameText = "Gold Shield";
            else if (type == "necklace") nameText = "Gold Necklace";
            else if (type == "ring") nameText = "Gold Ring";
        }
        else if (tier == 4){
            if (type == "helmet") nameText = "Diamond Helmet";
            else if (type == "gloves") nameText = "Diamond Gloves";
            else if (type == "legs") nameText = "Diamond Greaves";
            else if (type == "boots") nameText = "Diamond Boots";
            else if (type == "weapon" && range > 1) nameText = "Militia Crossbow";
            else if (type == "weapon") nameText = "Great Axe";
            else if (type == "shield") nameText = "Diamond Shield";
            else if (type == "necklace") nameText = "Diamond Necklace";
            else if (type == "ring") nameText = "Diamond Ring";
        }

        nameText += " Lvl " + level;

        string descriptionText = "";

        if(attackDamage > 0) descriptionText += String.Format("Attack Damage +{0}\n", attackDamage);
        if(hpBoost > 0) descriptionText += String.Format("HP Increase +{0}\n", hpBoost);
        if(critBoost > 0) descriptionText += String.Format("Critical Hit Chance +{0}\n", critBoost);
        if(evadeBoost > 0) descriptionText += String.Format("Evade Chance +{0}\n", evadeBoost);

        itemName = nameText;
        description = descriptionText;
        flavorText = "Something Funny";
        image = img;
    }

    public override void Create()
    {
    }

    public override void UseItem()
    {
    }

    public void AddInfliction(StatusEffect e)
    {
        inflictions.Add(e);
    }

    public int getTier(){
        return tier;
    }
}

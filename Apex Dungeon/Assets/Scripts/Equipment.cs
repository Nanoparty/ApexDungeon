using UnityEngine;
using System;

public class Equipment : Item
{
    //Parameters
    public int level;
    public Article article;
    public string type;
    public int tier;

    //stats
    public int defense;
    public int attack;
    public int intelligence;
    public int crit;

    public string modifier;
    public Equipment(){

    }
    public Equipment(int lvl, string type, int tier, Sprite image, int def, int atk, int intel, int crit, string mod)
    {
        this.level = lvl;
        this.type = type;
        this.tier = tier;
        this.image = image;
        this.defense = def;
        this.attack = atk;
        this.intelligence = intel;
        this.crit = crit;
        this.modifier = mod;

        setStats(lvl, type, tier, image, def, atk, intel, crit, mod);
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

    public void setStats(int lvl, string type, int tier, Sprite img, int def, int atk, int intel, int crit, string mod)
    {
        this.level = lvl;
        this.type = type;
        this.tier = tier;
        this.defense = def;
        this.attack = def;
        this.intelligence = intel;
        this.crit = crit;
        this.modifier = mod;

        string nameText = "";
        if(tier == 1)
        {
            if (type == "helmet") nameText = "Leather Helmet";
            else if (type == "chestplate") nameText = "Leather Chestplate";
            else if (type == "gloves") nameText = "Leather Gloves";
            else if (type == "legs") nameText = "Leather Greaves";
            else if (type == "boots") nameText = "Leather Boots";
        }
        else if (tier == 2)
        {
            if (type == "helmet") nameText = "Iron Helmet";
            else if (type == "chestplate") nameText = "Iron Chestplate";
            else if (type == "gloves") nameText = "Iron Gloves";
            else if (type == "legs") nameText = "Iron Greaves";
            else if (type == "boots") nameText = "Iron Boots";
        }
        else if (tier == 3)
        {
            if (type == "helmet") nameText = "Diamond Helmet";
            else if (type == "chestplate") nameText = "Diamond Chestplate";
            else if (type == "gloves") nameText = "Diamond Gloves";
            else if (type == "legs") nameText = "Diamond Greaves";
            else if (type == "boots") nameText = "Diamond Boots";
        }

        string descriptionText = "";

        if(atk > 0) descriptionText += String.Format("Attack +{0}\n", atk);
        if(def > 0) descriptionText += String.Format("Defense +{0}\n", def);
        if(intel > 0) descriptionText += String.Format("Intelligence +{0}\n", intel);
        if(crit > 0) descriptionText += String.Format("Critical +{0}", crit);


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
}

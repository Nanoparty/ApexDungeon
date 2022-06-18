﻿using UnityEngine;
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
    public int evade;

    public string modifier;
    public Equipment(){

    }
    public Equipment(int lvl, string type, int tier, Sprite image, int hpBoost, int attackBoost, int critBoost, int evadeBoost)
    {
        // this.level = lvl;
        // this.type = type;
        // this.tier = tier;
        // this.image = image;
        // this.defense = def;
        // this.attack = atk;
        // this.intelligence = intel;
        // this.crit = crit;
        // this.modifier = mod;

        setStats(lvl, type, tier, image, hpBoost, attackBoost, critBoost, evadeBoost);
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

    public void setStats(int lvl, string type, int tier, Sprite img, int hpBoost, int attackDamage, int critBoost, int evadeBoost)
    {
        this.level = lvl;
        this.type = type;
        this.tier = tier;
        this.defense = hpBoost;
        this.attack = attackDamage;
        this.intelligence = 0;
        this.crit = critBoost;
        this.evade = evadeBoost;

        string nameText = "";
        if(tier == 1)
        {
            if (type == "helmet") nameText = "Leather Helmet";
            else if (type == "chestplate") nameText = "Leather Chestplate";
            else if (type == "gloves") nameText = "Leather Gloves";
            else if (type == "legs") nameText = "Leather Greaves";
            else if (type == "boots") nameText = "Leather Boots";
            else if (type == "weapon") nameText = "Rusty Dagger";
            else if (type == "shield") nameText = "Leather Shield";
            else if (type == "necklace") nameText = "Copper Necklace";
            else if (type == "ring") nameText = "Copper Ring";
        }
        else if (tier == 2)
        {
            if (type == "helmet") nameText = "Iron Helmet";
            else if (type == "chestplate") nameText = "Iron Chestplate";
            else if (type == "gloves") nameText = "Iron Gloves";
            else if (type == "legs") nameText = "Iron Greaves";
            else if (type == "boots") nameText = "Iron Boots";
            else if (type == "weapon") nameText = "Long Sword";
            else if (type == "shield") nameText = "Iron Shield";
            else if (type == "necklace") nameText = "Iron Necklace";
            else if (type == "ring") nameText = "Iron Ring";
        }
        else if (tier == 3)
        {
            if (type == "helmet") nameText = "Gold Helmet";
            else if (type == "chestplate") nameText = "Gold Chestplate";
            else if (type == "gloves") nameText = "Gold Gloves";
            else if (type == "legs") nameText = "Gold Greaves";
            else if (type == "boots") nameText = "Gold Boots";
            else if (type == "weapon") nameText = "Battle Hammer";
            else if (type == "shield") nameText = "Gold Shield";
            else if (type == "necklace") nameText = "Gold Necklace";
            else if (type == "ring") nameText = "Gold Ring";
        }
        else if (tier == 4){
            if (type == "helmet") nameText = "Diamond Helmet";
            else if (type == "chestplate") nameText = "Diamond Chestplate";
            else if (type == "gloves") nameText = "Diamond Gloves";
            else if (type == "legs") nameText = "Diamond Greaves";
            else if (type == "boots") nameText = "Diamond Boots";
            else if (type == "weapon") nameText = "Great Axe";
            else if (type == "shield") nameText = "Diamond Shield";
            else if (type == "necklace") nameText = "Diamond Necklace";
            else if (type == "ring") nameText = "Diamond Ring";
        }

        nameText += " Lvl " + level;

        string descriptionText = "";

        if(attackDamage > 0) descriptionText += String.Format("Attack Damage +{0}\n", attackDamage);
        if(hpBoost > 0) descriptionText += String.Format("HP Increase +{0}\n", hpBoost);
        //if(intel > 0) descriptionText += String.Format("Intelligence +{0}\n", intel);
        if(critBoost > 0) descriptionText += String.Format("Critical Hit Chance +{0}", critBoost);
        if(evadeBoost > 0) descriptionText += String.Format("Evade Chance +{0}", evadeBoost);

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

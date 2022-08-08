using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayer
{
    public string name;
    public int level;
    public int floor;

    public int strength;
    public int defense;
    public int evasion;
    public int critical;

    public int hp;
    public int maxHp;
    public int exp;
    public int maxExp;

    public int gold;

    public SaveGear helmet;
    public SaveGear chestplate;
    public SaveGear legs;
    public SaveGear feet;
    public SaveGear weapon;
    public SaveGear shield;
    public SaveGear necklace;
    public SaveGear ring;

    public List<SaveGear> equipment;
    public List<SaveConsumable> consumables;

    public SavePlayer(CharacterData data)
    {
        name = data.name;
        level = data.expLevel;
        floor = data.floor;

        strength = data.strength;
        defense = data.defense;
        evasion = data.evade;
        critical = data.crit;

        hp = data.hp;
        maxHp = data.maxHp;
        exp = data.exp;
        maxExp = data.maxExp;

        gold = data.gold;

        helmet = new SaveGear(data.gear.Helmet);
        chestplate = new SaveGear(data.gear.Chestplate);
        legs = new SaveGear(data.gear.Legs);
        feet = new SaveGear(data.gear.Feet);
        weapon = new SaveGear(data.gear.Weapon);
        shield = new SaveGear(data.gear.Secondary);
        necklace = new SaveGear(data.gear.Necklace);
        ring = new SaveGear(data.gear.Ring);

        equipment = new List<SaveGear>();
        foreach(Equipment e in data.equipment)
        {
            equipment.Add(new SaveGear(e));
        }
        consumables = new List<SaveConsumable>();
        foreach(Consumable c in data.consumables)
        {
            consumables.Add(new SaveConsumable(c));
        }
    }
}

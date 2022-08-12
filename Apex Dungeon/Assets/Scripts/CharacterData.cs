using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public new string name;
    public int floor;
    public int level;
    public int gold;
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;
    public int exp;
    public int maxExp;
    public int expLevel;
    public int strength;
    public int intelligence;
    public int defense;
    public int crit;
    public int evade;
    public int block;
    public PlayerGear gear;
    public List<Equipment> equipment;
    public List<Consumable> consumables;

    public CharacterData(){
        name = "";
        floor = 1;
        level = 1;        
    }

    public CharacterData(string n){
        name = n;
        floor = 1;
        level = 1;
        gold = 0;
        hp = 100;
        maxHp = 100;
        mp = 100;
        maxMp = 100;
        exp = 0;
        maxExp = 100;
        expLevel = 1;
        strength = 10;
        intelligence = 10;
        defense = 10;
        crit = 10;
        evade = 10;
        block = 10;
        gear = new PlayerGear();
        equipment = new List<Equipment>();
        consumables = new List<Consumable>();
    }

    public CharacterData(string name, int floor, int expLevel, int gold, int strength,
        int defense, int evade, int crit, int hp, int maxHp, int exp, int maxExp, PlayerGear gear,
        List<Equipment> equipment, List<Consumable> consumables)
    {
        this.name = name;
        this.floor = floor;
        this.expLevel = expLevel;
        this.gold = gold;
        this.strength = strength;
        this.defense = defense;
        this.evade = evade;
        this.crit = crit;
        this.hp = hp;
        this.maxHp = maxHp;
        this.exp = exp;
        this.maxExp = maxExp;
        this.gear = gear;
        this.equipment = equipment;
        this.consumables = consumables;
    }
}

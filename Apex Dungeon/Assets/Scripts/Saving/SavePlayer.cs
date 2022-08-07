using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayer
{
    string name;
    int level;
    int floor;

    int strength;
    int defense;
    int evasion;
    int critical;

    int hp;
    int maxHp;
    int exp;
    int maxExp;

    int gold;

    SaveGear helmet;
    SaveGear chestplate;
    SaveGear legs;
    SaveGear feet;
    SaveGear weapon;
    SaveGear shield;
    SaveGear necklace;
    SaveGear ring;

    SaveInventory inventory;
}

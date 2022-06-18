using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public string name;
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

    public CharacterData(){
        name = "";
        floor = 0;
        level = 0;        
    }

    public CharacterData(string name){
        this.name = name;
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
    }
}

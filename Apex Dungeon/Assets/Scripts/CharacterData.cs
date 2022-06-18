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
}

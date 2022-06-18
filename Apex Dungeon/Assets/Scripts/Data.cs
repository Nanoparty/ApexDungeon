using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class Data
{
    public static CharacterMenu charMenu;
    public static bool inProgress;
    //scores Data
    public static List<(string, int)> scores;
    public static List<string> names;
    public static List<CharacterData> charData;

    public static string activeCharacter;

    //Character Data
    public static string playerName;
    public static int gold;
    public static int hp;
    public static int maxHp;
    public static int mp;
    public static int maxMp;
    public static int exp;
    public static int maxExp;
    public static int expLevel;
    public static int strength;
    public static int intelligence;
    public static int defense;
    public static int crit;
    public static int evade;
    public static int block;
    public static PlayerGear gear;

    public static void reset(){
        
    }

    public static void LoadActiveData(){
        CharacterData current = charData.Where(cd => cd.name == activeCharacter).FirstOrDefault();

        if(current == null){
            Debug.Log("Cannot find active character data");
            return;
        }

        playerName = current.name;
        gold = current.gold;
        hp = current.hp;
        maxMp = current.maxMp;
        exp = current.exp;
        maxExp = current.maxExp;
        expLevel = current.expLevel;
        strength = current.strength;
        intelligence = current.intelligence;
        defense = current.defense;
        crit = current.defense;
        evade = current.evade;
        block = current.block;
        gear = current.gear;
    }
}

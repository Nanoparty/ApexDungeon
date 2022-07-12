using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class Data
{
    //settings
    public static bool music = true;
    public static bool sound = true;
    public static float musicVolume = 1f;
    public static float soundVolume = 1f;

    public static CharacterMenu charMenu;
    public static bool inProgress;

    //scores Data
    public static List<(string, int)> scores;
    public static List<string> names;
    public static List<CharacterData> charData;

    public static string activeCharacter;
    public static bool loadData;

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

        playerName = activeCharacter;
        gold = current.gold;
        hp = current.hp;
        maxHp = current.maxHp;
        mp = current.mp;
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

    public static void SaveCharacter(){

        if(charData == null) {
            charData = new List<CharacterData>();
            activeCharacter = "bob";
            charData.Add(new CharacterData(activeCharacter));
            playerName = activeCharacter;
            Debug.Log("NULL CHARACTER DATA");
        }
        Debug.Log("Loading Character -> "+activeCharacter);

        CharacterData current = charData.Where(cd => cd.name == activeCharacter).First();
        Debug.Log("Setting charData name to "+ playerName);
        current.name = playerName;
        current.gold = gold;
        current.hp = hp;
        current.maxHp = maxHp;
        current.mp = mp;
        current.maxMp = maxMp;
        current.exp = exp;
        current.maxExp = maxExp;
        current.expLevel = expLevel;
        current.strength = strength;
        current.intelligence = intelligence;
        current.defense = defense;
        current.evade = evade;
        current.block = block;
        current.gear = gear;
    }

    public static void RemoveActive(){
        if (charData == null)
        {
            charData = new List<CharacterData>();
            activeCharacter = "bob";
            charData.Add(new CharacterData(activeCharacter));
            playerName = activeCharacter;
            Debug.Log("NULL CHARACTER DATA");
        }
        if (activeCharacter == "") return;
        CharacterData current = charData.Where(cd => cd.name == activeCharacter).First();
        charData.Remove(current);
        activeCharacter = "";
    }
}

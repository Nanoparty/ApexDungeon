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

    public static bool inProgress;

    //scores Data
    public static List<(string, int)> scores;
    public static List<string> names;
    public static List<CharacterData> charData;

    public static string activeCharacter;
    public static bool loadData;

    //inventory data
    public static List<Equipment> equipment;
    public static List<Consumable> consumables;

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
    public static int floor = 1;
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
        floor = current.floor;

        //TODO set active character inventory
        equipment = current?.equipment ?? new List<Equipment>();
        consumables = current?.consumables ?? new List<Consumable>(); 
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
        current.floor = floor;
        current.equipment = equipment;
        current.consumables = consumables;
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

    public static void SaveToFile()
    {
        SaveSystem.SaveData();
    }

    public static void LoadFromFile(ImageLookup il)
    {
        SaveData data = SaveSystem.LoadData();
        if (data == null) return;
        music = data.musicOn;
        sound = data.soundOn;
        musicVolume = data.musicVolume;
        soundVolume = data.soundVolume;
        scores = data.scores;
        names = data.usedNames;

        List<CharacterData> loadCharData = new List<CharacterData>();

        if (data.players == null) return;

        foreach(SavePlayer p in data.players)
        {
            //Equipment helmet = new Equipment(p.helmet);
            //Equipment chest = new Equipment(p.chestplate);
            //Equipment legs = new Equipment(p.legs);
            //Equipment feet = new Equipment(p.feet);
            //Equipment weapon = new Equipment(p.weapon);
            //Equipment shield = new Equipment(p.shield);
            //Equipment necklace = new Equipment(p.necklace);
            //Equipment ring = new Equipment(p.ring);

            //PlayerGear gear = new PlayerGear(chest, helmet, legs, feet, weapon, shield, necklace, ring);
            PlayerGear gear = new PlayerGear(p, il);

            List<Equipment> equips = new List<Equipment>();
            foreach(SaveGear g in p.equipment)
            {
                Equipment e = new Equipment(g, il);
                equips.Add(e);
            }

            List<Consumable> consumes = new List<Consumable>();
            foreach(SaveConsumable c in p.consumables)
            {
                Consumable item = new Consumable(c);
                if(item.image == null)
                {
                    Debug.Log(item.id);
                    item.image = il.getImage(item.id);
                }
                consumes.Add(item);
            }

            CharacterData cd = new CharacterData(p.name, p.floor, p.level, p.gold, p.strength, p.defense,
                p.evasion, p.critical, p.hp, p.maxHp, p.exp, p.maxExp, gear, equips, consumes);

            loadCharData.Add(cd);
        }
        charData = loadCharData;
    }
}

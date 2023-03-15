using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CharacterClass;
using static Equipment;
using static Skill;
using static StatusEffect;

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
    public static List<StatusEffect> statusEffects;
    public static List<Skill> skills;

    //Character Data
    public static string playerName;
    public static int gold;
    public static int baseHp;
    public static int hp;
    public static int maxHp;
    public static int mp;
    public static int maxMp;
    public static int exp;
    public static int maxExp;
    public static int expLevel;
    public static int strength;
    public static int attack;
    public static int intelligence;
    public static int defense;
    public static int crit;
    public static int evade;
    public static int block;
    public static float strengthScale;
    public static float defenseScale;
    public static float criticalScale;
    public static float evadeScale;
    public static int floor = 1;
    public static PlayerGear gear;
    public static ClassType characterClass;

    public static void reset(){
        
    }

    public static void LoadActiveData(){
        CharacterData current = charData.Where(cd => cd.name == activeCharacter).FirstOrDefault();

        playerName = activeCharacter;
        characterClass = current.classType;
        gold = current.gold;
        baseHp = current.baseHp;
        hp = current.hp;
        maxHp = current.maxHp;
        mp = current.mp;
        maxMp = current.maxMp;
        exp = current.exp;
        maxExp = current.maxExp;
        expLevel = current.expLevel;
        strength = current.strength;
        attack = current.attack;
        intelligence = current.intelligence;
        defense = current.defense;
        crit = current.defense;
        evade = current.evade;
        block = current.block;
        strengthScale = current.strengthScale;
        defenseScale = current.defenseScale;
        criticalScale = current.criticalScale;
        evadeScale = current.evadeScale;
        gear = current.gear;
        floor = current.floor;

        equipment = current?.equipment ?? new List<Equipment>();
        consumables = current?.consumables ?? new List<Consumable>(); 
        statusEffects = current?.statusEffects ?? new List<StatusEffect>();
        skills = current?.skills ?? new List<Skill>();
    }

    public static void SaveCharacter(){

        if(charData == null) {
            charData = new List<CharacterData>();
            activeCharacter = "bob";
            charData.Add(new CharacterData(activeCharacter, ClassType.Archer));
            playerName = activeCharacter;
        }

        CharacterData current = charData.Where(cd => cd.name == activeCharacter).First();
        current.name = playerName;
        current.gold = gold;
        current.baseHp = baseHp;
        current.hp = hp;
        current.maxHp = maxHp;
        current.mp = mp;
        current.maxMp = maxMp;
        current.exp = exp;
        current.maxExp = maxExp;
        current.expLevel = expLevel;
        current.strength = strength;
        current.attack = attack;
        current.intelligence = intelligence;
        current.defense = defense;
        current.evade = evade;
        current.block = block;
        current.strengthScale = strengthScale;
        current.defenseScale = defenseScale;
        current.criticalScale = criticalScale;
        current.evadeScale = evadeScale;
        current.gear = gear;
        current.floor = floor;
        current.equipment = equipment;
        current.consumables = consumables;
        current.statusEffects = statusEffects;
        current.classType = characterClass;
        current.skills = skills;
    }

    public static void RemoveActive(){
        if (charData == null)
        {
            charData = new List<CharacterData>();
            activeCharacter = "bob";
            charData.Add(new CharacterData(activeCharacter, ClassType.Archer));
            playerName = activeCharacter;
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

    public static void LoadFromFile(ImageLookup il, SkillGenerator skillGenerator = null)
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
            PlayerGear gear = new PlayerGear(p, il);

            List<Equipment> equips = new List<Equipment>();
            foreach(SaveGear g in p.equipment)
            {
                Equipment e = null;
                EquipType etype = Enum.Parse<EquipType>(g.equipType);
                if (etype == EquipType.HELMET)
                {
                    e = new Helmet(g); 
                }
                if (etype == EquipType.CHESTPLATE)
                {
                    e = new Chestplate(g);
                }
                if (etype == EquipType.SHIELD)
                {
                    e = new Shield(g);
                }
                if (etype == EquipType.BOOTS)
                {
                    e = new Boots(g);
                }
                if (etype == EquipType.GLOVES)
                {
                    e = new Gloves(g);
                }
                else
                {
                    e = new Equipment(g, il);
                }
                equips.Add(e);
            }

            List<Consumable> consumes = new List<Consumable>();
            foreach(SaveConsumable c in p.consumables)
            {
                Consumable item;
                if (c.id == "food")
                {
                    item = new Food(c);
                }
                else
                {
                    item = new Consumable(c);
                    item.image = il.getImage(item.id, item.spriteIndex);
                }
                consumes.Add(item);
            }
            List<StatusEffect> effects = new List<StatusEffect>();
            foreach (SaveStatusEffect sse in p.statusEffects)
            {
                EffectType type = Enum.Parse<EffectType>(sse.effectId);
                EffectOrder order = Enum.Parse<EffectOrder>(sse.effectOrder);
                effects.Add(new StatusEffect(type, sse.duration, order));
            }

            List<Skill> skillList = new List<Skill>();
            foreach (SaveSkill s in p.skills)
            {
                SkillType sType = Enum.Parse<SkillType>(s.skillType);
                if (GameManager.gmInstance == null)
                {
                    skillList.Add(skillGenerator.GetSkill(sType));
                }
                else
                {
                    skillList.Add(GameManager.gmInstance.SkillGenerator.GetSkill(sType));
                }
                
            }

            ClassType classType = Enum.Parse<ClassType>(p.classType ?? "Archer");
            if (classType == null) classType = ClassType.Archer;

            CharacterData cd = new CharacterData(p.name, p.floor, p.level, p.gold, p.strength, p.attack, p.defense,
                p.evasion, p.critical, p.baseHp, p.hp, p.maxHp, p.exp, p.maxExp, gear, equips, consumes, classType, 
                p.strengthScale, p.defenseScale, p.criticalScale, p.evadeScale, effects, skillList, p.mp, p.maxMp);

            loadCharData.Add(cd);
        }
        charData = loadCharData;
    }
}

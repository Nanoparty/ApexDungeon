using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass
{

    public enum ClassType
    {
        Knight,
        Warrior,
        Priest,
        Mage,
        Archer,
        Necromancer,
        Paladin,
        Bard,
        Swordsman,
        Druid,
        Monk,
        Thief
    }

    public struct ClassStats
    {
        public int strength;
        public int defense;
        public int critical;
        public int evasion;
        public List<Equipment> equipment;
        public List<Skill> skills;
    }

    public static ClassStats GetClassStats(ClassType ct)
    {
        EquipmentGenerator eg = Resources.Load<EquipmentGenerator>("ScriptableObjects/EquipmentGenerator");
        SkillGenerator sg = Resources.Load<SkillGenerator>("ScriptableObjects/SkillGenerator");

        if (ct == ClassType.Knight)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2, false) },
                skills = new List<Skill>() { sg.GetSkill(Skill.SkillType.Fireball) },
            };
        }
        if (ct == ClassType.Archer)
        {
            return new ClassStats
            {
                strength = 11,
                defense = 11,
                critical = 11,
                evasion = 11,
                equipment = new List<Equipment>() { },
                skills = new List<Skill>() { },
            };
        }

        return new ClassStats { };
    }
    
}

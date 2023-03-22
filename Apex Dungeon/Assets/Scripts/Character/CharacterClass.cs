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
                equipment = new List<Equipment>() { 
                    eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2),
                    eg.GenerateEquipOfTypeNoPickup(1, "shield", 1)
                },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Thrust), 
                    sg.GetSkill(Skill.SkillType.ArmorPolish) 
                },
            };
        }
        if (ct == ClassType.Archer)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 3, true) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Trap), 
                    sg.GetSkill(Skill.SkillType.IceShard) 
                },
            };
        }
        if (ct == ClassType.Swordsman)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2, false) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Slash), 
                    sg.GetSkill(Skill.SkillType.FieldDress) 
                },
            };
        }
        if (ct == ClassType.Druid)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2, false) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Bind), 
                    sg.GetSkill(Skill.SkillType.PoisonSpike) 
                },
            };
        }
        if (ct == ClassType.Bard)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 1, true) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Hypnosis),
                    sg.GetSkill(Skill.SkillType.Pound)
                },
            };
        }
        if (ct == ClassType.Thief)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 1, false) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Stealth), 
                    sg.GetSkill(Skill.SkillType.Lacerate) 
                },
            };
        }
        if (ct == ClassType.Priest)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 1, false) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Bless), 
                    sg.GetSkill(Skill.SkillType.Cleanse) 
                },
            };
        }
        if (ct == ClassType.Paladin)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() {
                    eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2),
                    eg.GenerateEquipOfTypeNoPickup(1, "shield", 1)
                },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Restore),
                    sg.GetSkill(Skill.SkillType.Bash)
                },
            };
        }
        if (ct == ClassType.Warrior)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 3) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Headbutt), 
                    sg.GetSkill(Skill.SkillType.Berserk) 
                },
            };
        }
        if (ct == ClassType.Necromancer)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2, false) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.LifeDrain), 
                    sg.GetSkill(Skill.SkillType.Plague) },
            };
        }
        if (ct == ClassType.Mage)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2, false) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.Teleport), 
                    sg.GetSkill(Skill.SkillType.MagicMissile) 
                },
            };
        }
        if (ct == ClassType.Monk)
        {
            return new ClassStats
            {
                strength = 10,
                defense = 10,
                critical = 10,
                evasion = 10,
                equipment = new List<Equipment>() { eg.GenerateEquipOfTypeNoPickup(1, "weapon", 2, false) },
                skills = new List<Skill>() { 
                    sg.GetSkill(Skill.SkillType.WhirlwindStrike), 
                    sg.GetSkill(Skill.SkillType.ManaDrain) 
                },
            };
        }

        return new ClassStats { };
    }
    
}

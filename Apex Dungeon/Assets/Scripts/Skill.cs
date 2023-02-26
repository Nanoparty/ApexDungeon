using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public enum SkillType
    {
        Fireball,
        Snowball,
        LightningBolt,
        Hypnosis,
        Lacerate,
        Stun,
        ArmorPolish,
        WhirlwindStrike,
        FieldDress,
        Berserk,
        Thrust,
        Stomp,
        Bite,
        Slash,
        Scratch,
        Pound,
        Stealth,
        Trap,
        Taunt,
        MagicMissle,
        Invisibility,
        Teleport,
        Restore,
        Cleanse,
        HealthDrain,
        ManaDrain,
        BloodCurse,
        Bind,
        Silence,
        Bless,
        Plague,
        FlamePalm,
        IcePalm,
        StaticPalm
    }

    public SkillType type;
    public string name;
    public string description;
    public Sprite image;
    public int manaCost;
    public int healthCost;
    public int range;

    public Skill(SkillType type, string name, string description, Sprite image)
    {
        this.type = type;
        this.name = name;
        this.description = description;
        this.image = image;

        switch (type)
        {
            case SkillType.Fireball:
                manaCost = 10;
                range = 3;
                break;
            case SkillType.Restore:
                manaCost = 20;
                range = 1;
                break;
        }
    }

    public bool Activate(MovingEntity caster, MovingEntity target)
    {
        switch (type)
        {
            case SkillType.Restore:
                if (caster.getMP() < manaCost) return false;

                float restoreAmount = target.getMaxHP() * 0.5f;
                target.addHp((int)restoreAmount);
                caster.addMp(-manaCost);
                return true;
                break;
        }

        return false;
    }
}

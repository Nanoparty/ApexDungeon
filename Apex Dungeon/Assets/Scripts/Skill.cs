using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatusEffect;

public class Skill
{
    public enum SkillType
    {
        //Attacks
        Fireball,
        IceSpike,
        LightningBolt,
        PoisonJet,
        WhirlwindStrike,
        Thrust,
        Bash,
        Stomp,
        Bite,
        Slash,
        Scratch,
        Pound,
        Headbutt,
        Trap,
        MagicMissle,
        LifeDrain,
        ManaDrain,
        BloodCurse,
        FlamePalm,
        IcePalm,
        StaticPalm,

        //Status Effects
        Hypnosis,
        Lacerate,
        Stun,
        ArmorPolish,
        FieldDress,
        Berserk,
        Bind,
        Silence,
        Bless,
        Plague,
        Stealth,
        Taunt,
        Invisibility,
        Teleport,
        Restore,
        Cleanse,
        
       //Other
        
    }

    public SkillType type;
    public string skillName;
    public string description;
    public Sprite image;
    public int manaCost;
    public int healthCost;
    public int range;
    public bool canTargetSelf;
    public Sprite projectile;

    public Skill(SkillType type, string name, string description, Sprite image, Sprite projectile)
        : this(type, name, description, image)
    {
        this.projectile = projectile;
    }

    public Skill(SkillType type, string name, string description, Sprite image)
    {
        this.type = type;
        this.skillName = name;
        this.description = description;
        this.image = image;
        this.canTargetSelf = true;

        switch (type)
        {
            case SkillType.Fireball:
                manaCost = 10;
                range = 3;
                break;

            case SkillType.IceSpike:
                manaCost = 10;
                range = 3;
                break;

            case SkillType.LightningBolt:
                manaCost = 10;
                range = 3;
                break;

            case SkillType.Restore:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.Cleanse:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.Plague:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.Lacerate:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.Bless:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.Teleport:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.ArmorPolish:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.Berserk:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.FieldDress:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.LifeDrain:
                manaCost = 20;
                range = 3;
                break;

            case SkillType.ManaDrain:
                manaCost = 20;
                range = 3;
                break;
        }
    }

    public bool Activate(MovingEntity caster, MovingEntity target)
    {
        if (caster.getMP() < manaCost) return false;
        caster.addMp(-manaCost);

        switch (type)
        {
            case SkillType.Restore:
                float restoreAmount = target.getMaxHP() * 0.5f;
                target.takeDamage(restoreAmount, ColorManager.HEAL);
                break;

            case SkillType.Fireball:
                float damage = target.getMaxHP() * 0.2f;
                target.takeDamage(-damage, ColorManager.FIRE);
                target.AddStatusEffect(new StatusEffect(EffectType.burn, 5, EffectOrder.End));
                break;

            case SkillType.IceSpike:
                damage = target.getMaxHP() * 0.2f;
                target.takeDamage(-damage, ColorManager.ICE);
                target.AddStatusEffect(new StatusEffect(EffectType.freeze, 5, EffectOrder.End));
                break;

            case SkillType.LightningBolt:
                damage = target.getMaxHP() * 0.2f;
                target.takeDamage(-damage, ColorManager.LIGHTNING);
                target.AddStatusEffect(new StatusEffect(EffectType.electric, 5, EffectOrder.End));
                break;

            case SkillType.Cleanse:
                System.Array values = System.Enum.GetValues(typeof(EffectType));
                foreach (EffectType effect in values)
                {
                    target.RemoveAllStatusEffect(effect);
                }
                break;

            case SkillType.Plague:
                target.AddStatusEffect(new StatusEffect(EffectType.poison, 5, EffectOrder.End));
                break;

            case SkillType.Lacerate:
                target.AddStatusEffect(new StatusEffect(EffectType.bleed, 5, EffectOrder.End));
                break;

            case SkillType.Bless:
                target.AddStatusEffect(new StatusEffect(EffectType.health_regen, 5, EffectOrder.Start));
                break;

            case SkillType.Teleport:
                Vector2 pos = GameManager.gmInstance.Dungeon.getRandomUnoccupiedTile();
                target.setPosition((int)pos.x, (int)pos.y);
                break;

            case SkillType.ArmorPolish:
                target.AddStatusEffect(new StatusEffect(EffectType.defense_up, 20, EffectOrder.Status));
                break;

            case SkillType.Berserk:
                target.AddStatusEffect(new StatusEffect(EffectType.strength_up, 20, EffectOrder.Status));
                break;

            case SkillType.FieldDress:
                restoreAmount = target.getMaxHP() * 0.3f;
                target.takeDamage(restoreAmount, ColorManager.HEAL);
                break;

            case SkillType.LifeDrain:
                float amount = target.getMaxHP() * 0.2f;
                target.takeDamage(-amount, ColorManager.DAMAGE);
                caster.takeDamage(amount, ColorManager.HEAL);
                break;

            case SkillType.ManaDrain:
                amount = target.getMaxMP() * 0.2f;
                target.addMp((int)-amount);
                caster.addMp((int)amount);
                break;

            default: return false;
        }

        return true;
    }

    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(1);
    }
}

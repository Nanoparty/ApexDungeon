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

    public Skill(SkillType type, Sprite image, Sprite projectile)
        : this(type, image)
    {
        this.projectile = projectile;
    }

    public Skill(SkillType type, Sprite image)
    {
        this.type = type;
        //this.skillName = name;
        //this.description = description;
        this.image = image;
        this.canTargetSelf = true;

        switch (type)
        {
            case SkillType.Fireball:
                manaCost = 20;
                range = 3;
                skillName = "Fireball";
                description = "Cast fireball at target. Causes Burn.";
                break;

            case SkillType.IceSpike:
                manaCost = 20;
                range = 3;
                skillName = "Ice Spike";
                description = "Cast ice spike ata target. Causes Frostbite.";
                break;

            case SkillType.LightningBolt:
                manaCost = 20;
                range = 3;
                skillName = "Lightning Bolt";
                description = "Cast lightning bolt at target. Causes Electrified.";
                break;

            case SkillType.Restore:
                manaCost = 30;
                range = 0;
                skillName = "Restore";
                description = "Heal target for 50% max HP.";
                break;

            case SkillType.Cleanse:
                manaCost = 20;
                range = 0;
                skillName = "Cleanse";
                description = "Cleanse target of all status effects.";
                break;

            case SkillType.Plague:
                manaCost = 20;
                range = 3;
                skillName = "Plague";
                description = "Afflict target with poison.";
                break;

            case SkillType.Lacerate:
                manaCost = 20;
                range = 3;
                skillName = "Lacerate";
                description = "Afflict target with bleeding.";
                break;

            case SkillType.Bless:
                manaCost = 20;
                range = 0;
                skillName = "Bless";
                description = "Grant target health regeneration.";
                break;

            case SkillType.Teleport:
                manaCost = 30;
                range = 3;
                skillName = "Teleport";
                description = "Teleport target to random location on floor.";
                break;

            case SkillType.ArmorPolish:
                manaCost = 10;
                range = 0;
                skillName = "Armor Polish";
                description = "Increase target defense.";
                break;

            case SkillType.Berserk:
                manaCost = 10;
                range = 0;
                skillName = "Berserk";
                description = "Increase target attack.";
                break;

            case SkillType.FieldDress:
                manaCost = 10;
                range = 0;
                skillName = "Field Dress";
                description = "Heal target by 20% of max HP.";
                break;

            case SkillType.LifeDrain:
                manaCost = 30;
                range = 1;
                skillName = "Life Drain";
                description = "Absorb HP from target.";
                break;

            case SkillType.ManaDrain:
                manaCost = 0;
                range = 1;
                skillName = "Mana Drain";
                description = "Absorb MP from target.";
                break;

            case SkillType.Hypnosis:
                manaCost = 30;
                range = 1;
                skillName = "Hypnosis";
                description = "Puts target to sleep.";
                break;

            case SkillType.Silence:
                manaCost = 10;
                range = 2;
                skillName = "Silence";
                description = "Prevents target from using skills.";
                break;

            case SkillType.Stun:
                manaCost = 10;
                range = 2;
                skillName = "Stun";
                description = "Aflicts target with paralysis.";
                break;
        }
    }

    public bool Activate(MovingEntity caster, MovingEntity target)
    {
        if (caster.getMP() < manaCost) return false;
        if (caster.silenced) return false;

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

            case SkillType.Hypnosis:
                target.AddStatusEffect(new StatusEffect(EffectType.sleep, 3, EffectOrder.Start));
                break;

            case SkillType.Silence:
                target.AddStatusEffect(new StatusEffect(EffectType.silence, 5, EffectOrder.Status));
                break;

            case SkillType.Stun:
                target.AddStatusEffect(new StatusEffect(EffectType.paralysis, 5, EffectOrder.Start));
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

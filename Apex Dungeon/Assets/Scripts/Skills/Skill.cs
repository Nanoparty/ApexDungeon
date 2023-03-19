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
        IceShard,
        LightningBolt,
        PoisonSpike,
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
        MagicMissile,
        LifeDrain,
        ManaDrain,
        BloodCurse,
        FlamePalm,
        IcePalm,
        StaticPalm,
        PoisonPalm,

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
        //Invisibility,
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
    public bool canTargetLocation;
    public Sprite projectile;
    public GameObject spawn;
    public bool hasProjectile;

    public Skill(SkillType type, Sprite image, Sprite projectile)
        : this(type, image)
    {
        this.projectile = projectile;
    }

    public Skill(SkillType type, Sprite image, GameObject spawn)
        : this(type, image)
    {
        this.spawn = spawn;
    }

    public GameObject GetProjectile()
    {
        if (type == SkillType.Fireball)
        {
            return Resources.Load<GameObject>("Projectiles/Fireball");
        }
        return null;
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
                canTargetSelf = false;
                skillName = "Fireball";
                description = "Cast fireball at target. Chance to cause Burn.";
                hasProjectile = true;
                break;

            case SkillType.IceShard:
                manaCost = 20;
                range = 3;
                canTargetSelf = false;
                skillName = "Ice Shard";
                description = "Cast ice shard at target. Chance to cause Frostbite.";
                break;

            case SkillType.LightningBolt:
                manaCost = 20;
                range = 3;
                canTargetSelf = false;
                skillName = "Lightning Bolt";
                description = "Cast lightning bolt at target. Chance to cause Electrified.";
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
                manaCost = 10;
                range = 1;
                skillName = "Lacerate";
                description = "Afflict target with bleeding.";
                break;

            case SkillType.BloodCurse:
                manaCost = 20;
                range = 3;
                skillName = "Blood Curse";
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

            case SkillType.PoisonSpike:
                manaCost = 20;
                canTargetSelf = false;
                range = 3;
                skillName = "Poison Spike";
                description = "Fires a poison spike at target. Chance to inflicts Poison.";
                break;

            case SkillType.WhirlwindStrike:
                manaCost = 30;
                range = 0;
                skillName = "Whirlwind Strike";
                description = "Attack all enemies within 1 tile.";
                break;

            case SkillType.Slash:
                manaCost = 10;
                range = 1;
                canTargetSelf = true;
                skillName = "Slash";
                description = "Slash target with blade. Chance to bleed.";
                break;

            case SkillType.Scratch:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Scratch";
                description = "Scratch target with claws. Chance to bleed.";
                break;

            case SkillType.Pound:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Pound";
                description = "Pound target with body. Chance to cause paralysis.";
                break;

            case SkillType.Trap:
                manaCost = 10;
                range = 2;
                canTargetSelf = false;
                canTargetLocation = true;
                skillName = "Trap";
                description = "Place a bear trap at target location.";
                break;

            case SkillType.FlamePalm:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Flame Palm";
                description = "Attack target with flaming strike. Chance to cause Burn.";
                break;

            case SkillType.IcePalm:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Ice Palm";
                description = "Attack target with freezing strike. Chance to cause Frostbite.";
                break;

            case SkillType.StaticPalm:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Static Palm";
                description = "Attack target with electric strike. Chance to cause Electrified.";
                break;

            case SkillType.PoisonPalm:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Poison Palm";
                description = "Attack target with poison strike. Chance to cause Poison.";
                break;

            case SkillType.MagicMissile:
                manaCost = 5;
                range = 3;
                canTargetSelf = false;
                skillName = "Magic Missile";
                description = "Fire magical bolt at target.";
                break;

            case SkillType.Taunt:
                manaCost = 5;
                range = 5;
                canTargetSelf = false;
                skillName = "Taunt";
                description = "Draw target's aggro.";
                break;

            case SkillType.Bind:
                manaCost = 10;
                range = 3;
                skillName = "Bind";
                description = "Lock's target in place.";
                break;

            case SkillType.Stealth:
                manaCost = 10;
                range = 0;
                skillName = "Stealth";
                description = "Reduces enemy aggro range.";
                break;

            //case SkillType.Invisibility:
            //    manaCost = 30;
            //    range = 0;
            //    skillName = "Invisibility";
            //    description = "Prevents enemies from detecting caster. Effect is lost on attacking.";
            //    break;

            case SkillType.Bash:
                manaCost = 20;
                range = 1;
                canTargetSelf = false;
                skillName = "Bash";
                description = "Bash target with weapon. Knocks target back 1 tile.";
                break;

            case SkillType.Headbutt:
                manaCost = 20;
                range = 1;
                canTargetSelf = false;
                skillName = "Headbutt";
                description = "Ram target with your head and deal heavy damage. Inflicts recoil on user.";
                break;

            case SkillType.Thrust:
                manaCost = 20;
                range = 2;
                canTargetSelf = false;
                skillName = "Thrust";
                description = "Thrust weapon at target.";
                break;

            case SkillType.Bite:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Bite";
                description = "Bite target. Chance to cause Poison or Bleed.";
                break;

            case SkillType.Stomp:
                manaCost = 10;
                range = 1;
                canTargetSelf = false;
                skillName = "Stomp";
                description = "Stomp on target. Chance to cause Paralysis.";
                break;

        }
    }

    public bool Activate(MovingEntity caster, int row, int col)
    {
        if (caster.GetMP() < manaCost)
        {
            return false;
        }
        if (caster.silenced)
        {
            return false;
        }

        MovingEntity target = null;

        if (caster.GetRow() == row && caster.GetCol() == col) {
            target = caster;
        }
        else {
            Enemy e = GameManager.gmInstance.GetEnemyAtLoc(row, col);
            if (e != null) { target = (MovingEntity) e; }
            else
            {
                Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                if (player.GetRow() == row && player.GetCol() == col)
                {
                    target = (MovingEntity) player;
                }
            }
        }

        if (target == null && !canTargetLocation)
        {
            return false;
        }

        if (canTargetLocation)
        {
            GameManager.gmInstance.Log.AddLog($">{caster.entityName} used {skillName}.");
        }
        else
        {
            GameManager.gmInstance.Log.AddLog($">{caster.entityName} used {skillName} on {target.entityName}.");
        }
         

        caster.AddMp(-manaCost);

        

        switch (type)
        {
            case SkillType.Restore:
                float restoreAmount = target.GetMaxHP() * 0.5f;
                target.TakeDamage(restoreAmount, ColorManager.HEAL);
                break;

            case SkillType.Fireball:
                float damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.FIRE);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.burn, 5, EffectOrder.End));
                break;

            case SkillType.IceShard:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.ICE);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.freeze, 5, EffectOrder.End));
                break;

            case SkillType.LightningBolt:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.LIGHTNING);
                if (Random.Range(0, 100) <= 20)
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
                target.SetPosition((int)pos.x, (int)pos.y);
                target.DoneMoving();
                break;

            case SkillType.ArmorPolish:
                target.AddStatusEffect(new StatusEffect(EffectType.defense_up, 20, EffectOrder.Status));
                break;

            case SkillType.Berserk:
                target.AddStatusEffect(new StatusEffect(EffectType.strength_up, 20, EffectOrder.Status));
                break;

            case SkillType.FieldDress:
                restoreAmount = target.GetMaxHP() * 0.3f;
                target.TakeDamage(restoreAmount, ColorManager.HEAL);
                break;

            case SkillType.LifeDrain:
                float amount = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-amount, ColorManager.DAMAGE);
                caster.TakeDamage(amount, ColorManager.HEAL);
                break;

            case SkillType.ManaDrain:
                amount = target.GetMaxMP() * 0.2f;
                if (target.GetMP() < amount)
                {
                    amount = target.GetMP();
                }
                if (target.GetMP() == 0)
                {
                    GameManager.gmInstance.Log.AddLog($">{target.entityName} has no MP left.");
                    break;
                }
                target.AddMp((int)-amount);
                caster.AddMp((int)amount);
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

            case SkillType.PoisonSpike:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.POISON);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.poison, 5, EffectOrder.End));
                break;

            case SkillType.WhirlwindStrike:
                // Get all entities in circle of target
                List<MovingEntity> entities = new List<MovingEntity>();
                for (int r = caster.GetRow() - 1; r <= caster.GetRow() + 1; r++)
                {
                    for (int c = caster.GetCol() - 1; c <= caster.GetCol() + 1; c++)
                    {
                        MovingEntity e = GameManager.gmInstance.GetEnemyAtLoc(r, c);
                        if (e != null)
                            entities.Add(e);
                    }
                }
                // Attack all entities
                foreach (MovingEntity e in entities)
                {
                    damage = e.GetMaxHP() * 0.2f;
                    e.TakeDamage(-damage, ColorManager.DAMAGE);
                }
                break;

            case SkillType.Slash:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.bleed, 5, EffectOrder.End));
                break;

            case SkillType.Scratch:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 10)
                    target.AddStatusEffect(new StatusEffect(EffectType.bleed, 5, EffectOrder.End));
                break;

            case SkillType.Stomp:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 10)
                    target.AddStatusEffect(new StatusEffect(EffectType.paralysis, 5, EffectOrder.End));
                break;

            case SkillType.Bite:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 10)
                    target.AddStatusEffect(new StatusEffect(EffectType.bleed, 5, EffectOrder.End));
                else if (Random.Range(0, 100) <= 10)
                    target.AddStatusEffect(new StatusEffect(EffectType.poison, 5, EffectOrder.End));
                break;

            case SkillType.Pound:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 10)
                    target.AddStatusEffect(new StatusEffect(EffectType.paralysis, 5, EffectOrder.Start));
                break;

            case SkillType.Trap:
                // Spawn trap
                GameObject o = caster.SpawnObject(spawn, new Vector2(col, row));
                o.GetComponent<Trap>().Setup(row, col, GameManager.gmInstance.level);
                break;

            case SkillType.FlamePalm:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.burn, 5, EffectOrder.End));
                break;

            case SkillType.IcePalm:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.freeze, 5, EffectOrder.End));
                break;

            case SkillType.StaticPalm:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.electric, 5, EffectOrder.End));
                break;

            case SkillType.PoisonPalm:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                if (Random.Range(0, 100) <= 20)
                    target.AddStatusEffect(new StatusEffect(EffectType.poison, 5, EffectOrder.End));
                break;

            case SkillType.MagicMissile:
                damage = target.GetMaxHP() * 0.1f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                break;

            case SkillType.Taunt:
                target.agro = true;
                break;

            case SkillType.Bind:
                // Root status effect
                target.AddStatusEffect(new StatusEffect(EffectType.root, 5, EffectOrder.Status));
                break;

            case SkillType.Stealth:
                // Stealth status effect
                target.AddStatusEffect(new StatusEffect(EffectType.stealth, 5, EffectOrder.Status));
                break;

            //case SkillType.Invisibility:
            //    // Invisibility status effect
            //    target.AddStatusEffect(new StatusEffect(EffectType.invisible, 5, EffectOrder.Status));
            //    break;

            case SkillType.Bash:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                // Move target back 1 tile
                int rdif = target.GetRow() - caster.GetRow();
                int cdif = target.GetCol() - caster.GetCol();
                //if (!GameManager.gmInstance.Dungeon.tileMap[rdif, cdif].getBlocked())
                //{
                //    target.SetPosition(target.GetRow() + rdif, target.GetCol() + cdif);
                //}
                break;

            case SkillType.Headbutt:
                damage = target.GetMaxHP() * 0.4f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                caster.TakeDamage(-((int)(caster.GetMaxHP() * 0.1f)), ColorManager.DAMAGE);
                break;

            case SkillType.Thrust:
                damage = target.GetMaxHP() * 0.2f;
                target.TakeDamage(-damage, ColorManager.DAMAGE);
                break;

            case SkillType.BloodCurse:
                target.AddStatusEffect(new StatusEffect(EffectType.bleed, 5, EffectOrder.End));
                break;

            default: { Debug.Log("Default"); return false; }
        }
        Debug.Log("End of Skill");
        return true;
    }

    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(1);
    }
}

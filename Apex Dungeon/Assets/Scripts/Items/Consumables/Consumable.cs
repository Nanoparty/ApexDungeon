using System;
using System.Collections.Generic;
using UnityEngine;
using static Skill;
using static StatusEffect;

public class Consumable : Item
{
    public bool throwable;

    public Consumable(string id, string name, string flavor, string desc, Sprite img, int level = 1, SkillType st = SkillType.Fireball){
        itemName = name;
        flavorText = flavor;
        description = desc;
        image = img;
        this.level = level;
        this.id = id;
        skillType = st;
    }

    public Consumable(string id, string name, string flavor, string desc, Sprite img, int spriteIndex, int level = 1, SkillType st = SkillType.Fireball)
    {
        itemName = name;
        flavorText = flavor;
        description = desc;
        image = img;
        this.level = level;
        this.id = id;
        skillType = st;
        this.spriteIndex = spriteIndex;
    }

    public Consumable(string id, string name, string flavor, string desc, Sprite img, int level, bool throwable)
    {
        itemName = name;
        flavorText = flavor;
        description = desc;
        image = img;
        this.id = id;
        this.level = level;
        this.throwable = throwable;
    }

    public Consumable() { }

    public Consumable(SaveConsumable sc)
    {
        itemName = sc.itemName;
        flavorText = sc.rank;
        description = sc.description;
        level = sc.itemLevel;
        id = sc.id;
        skillType = Enum.Parse<SkillType>(sc.skillType);
        spriteIndex = sc.spriteIndex;
    }

    public void SetStats(string n, string f, string d, Sprite s)
    {
        itemName = n;
        flavorText = f;
        description = d;
        image = s;
    }

    public virtual Sprite GetImage() { return null; }

    public override void Create()
    {
    }

    public override void UseItem()
    {
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(id == "HealthPotion"){
            int baseHP = 50;
            float appliedHP = baseHP * Mathf.Pow(1.5f,level-1);
            p.AddHP((int)appliedHP);
            SoundManager.sm.PlayPotionSound();
        }
        if (id == "HealthRegenPotion")
        {
            p.AddStatusEffect(new StatusEffect(EffectType.health_regen, 5, EffectOrder.Start));
        }
        if(id == "ManaPotion"){
            int baseMP = 25;
            float appliedMP = baseMP * Mathf.Pow(1.5f, level - 1);
            p.AddMP((int)appliedMP);
            SoundManager.sm.PlayPotionSound();
        }
        if (id == "ManaRegenPotion")
        {
            p.AddStatusEffect(new StatusEffect(EffectType.mana_regen, 5, EffectOrder.Start));
        }
        if(id == "SkipOrb"){
            p.NextFloor();
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
        if(id == "TeleportOrb"){
            Vector2 pos = GameManager.gmInstance.Dungeon.getRandomUnoccupiedTile();
            p.SetPosition((int)pos.x, (int)pos.y);
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
        if(id == "DeathOrb"){
            List<Vector2> activeShadows = GameManager.gmInstance.Dungeon.getActiveShadowCoords();
            foreach(Vector2 v in activeShadows){
                Enemy e = GameManager.gmInstance.GetEnemyAtLoc((int)v.x,(int)v.y);
                if(e != null){
                    e.Die();
                }
            }
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
        if(id == "MapFragment"){
            GameManager.gmInstance.Dungeon.setFullExplored(true);
            SoundManager.sm.PlayPageTurn();
        }
        if(id == "LightOrb"){
            GameManager.gmInstance.Dungeon.setFullBright(true);
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
        if(id == "Bandage")
        {
            p.RemoveAllStatusEffect(EffectType.bleed);
        }
        if(id == "Antidote")
        {
            p.RemoveAllStatusEffect(EffectType.poison);
        }
        if (id == "Chicken" || id == "food")
        {
            p.AddStatusEffect(new StatusEffect(EffectType.health_regen, 2, EffectOrder.End));
        }
        if (id == "BuffScroll")
        {
            EffectType[] effects = { EffectType.strength_up, EffectType.defense_up, EffectType.evasion_up, EffectType.critical_up };
            EffectType effect = (EffectType) effects.GetValue(UnityEngine.Random.Range(0, effects.Length));
            p.AddStatusEffect(new StatusEffect(effect, 20, EffectOrder.Status));

        }
        if (id == "EscapeRope")
        {
            if (GameManager.gmInstance.level > 1)
            {
                GameManager.gmInstance.level -= 2;
                p.NextFloor();
            }
            else
            {
                GameManager.gmInstance.level -= 1;
                p.NextFloor();
            }
        }
        if (id == "MagicDice")
        {
            System.Array values = System.Enum.GetValues(typeof(EffectType));
            EffectType effect = (EffectType) values.GetValue(UnityEngine.Random.Range(0, values.Length));
            int duration = 20;
            EffectOrder order = EffectOrder.Status;
            EffectType[] shortEffects = { EffectType.bleed, EffectType.poison, EffectType.health_regen };
            if (Array.IndexOf(shortEffects, effect) != -1){
                duration = 5;
                order = EffectOrder.End;
            }
            p.AddStatusEffect(new StatusEffect(effect, duration, order));
        }
        if (id == "ExpPotion")
        {
            p.AddExp(p.GetMaxExp());
        }
        if (id == "HeartGem")
        {
            p.AddBaseHP((int)(p.GetMaxHP() * 0.1f));

        }
        if (id == "FullCleanse")
        {
            System.Array values = System.Enum.GetValues(typeof(EffectType));
            foreach (EffectType effect in values)
            {
                p.RemoveAllStatusEffect(effect);
            }
        }
        if (id == "skillbook")
        {
            Skill skill = GameManager.gmInstance.SkillGenerator.GetSkill(skillType);
            p.skills.Add(skill);
        }
        if (id == "Stone")
        {
            Skill stoneSkill = new Skill(SkillType.Rock, "Stone Toss", 3, true);
            p.activeSkill = stoneSkill;
            p.targetMode = true;
            p.CloseJournal();
        }
        if (id == "Dart")
        {
            Skill dartSkill = new Skill(SkillType.Dart, "Dart Throw", 3, true);
            p.activeSkill = dartSkill;
            p.targetMode = true;
            p.CloseJournal();
        }
    }
}

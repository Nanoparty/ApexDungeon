using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class StatusEffect
{

    public enum EffectType
    {
        health_regen,
        mana_regen,
        poison,
        bleed,
        silence,
        paralysis,
        burn,
        freeze,
        electric,
        sleep,
        stealth,
        invisible,
        root,
        strength_up,
        defense_up,
        critical_up,
        evasion_up,
        strength_down,
        defense_down,
        critical_down,
        evasion_down
    }

    public enum EffectOrder
    {
        Start,
        Update,
        End,
        Status
    }

    public EffectType effectId;
    public int duration;
    public Color textColor;
    public string popupText;
    public EffectOrder order;

    public StatusEffect(EffectType id, int duration, EffectOrder order)
    {
        this.effectId = id;
        this.duration = duration;
        this.order = order;

        switch (effectId)
        {
            case EffectType.health_regen:
                textColor = ColorManager.HEAL;
                popupText = "HP Regen";
                break;
            case EffectType.mana_regen:
                textColor = ColorManager.MANA;
                popupText = "MP Regen";
                break;
            case EffectType.poison:
                textColor = ColorManager.POISON;
                popupText = "Poison";
                break;
            case EffectType.bleed:
                textColor = ColorManager.BLEED;
                popupText = "Bleed";
                break;
            case EffectType.burn:
                textColor = ColorManager.FIRE;
                popupText = "Burn";
                break;
            case EffectType.freeze:
                textColor = ColorManager.ICE;
                popupText = "Freeze";
                break;
            case EffectType.electric:
                textColor = ColorManager.LIGHTNING;
                popupText = "Electrified";
                break;
            case EffectType.paralysis:
                textColor = Color.yellow;
                popupText = "Paralysis";
                break;
            case EffectType.sleep:
                textColor = ColorManager.SLEEP;
                popupText = "Sleep";
                break;
            case EffectType.silence:
                textColor = ColorManager.SILENT;
                popupText = "Silenced";
                break;
            case EffectType.invisible:
                textColor = ColorManager.INVISIBLE;
                popupText = "Invisible";
                break;
            case EffectType.stealth:
                textColor = ColorManager.STEALTH;
                popupText = "Stealth";
                break;
            case EffectType.root:
                textColor = ColorManager.ROOT;
                popupText = "Root";
                break;
            case EffectType.strength_up:
                textColor = Color.cyan;
                popupText = "Strength Up";
                break;
            case EffectType.defense_up:
                textColor = Color.cyan;
                popupText = "Defense Up";
                break;
            case EffectType.critical_up:
                textColor = Color.cyan;
                popupText = "Critical Up";
                break;
            case EffectType.evasion_up:
                textColor = Color.cyan;
                popupText = "Evasion Up";
                break;
            case EffectType.strength_down:
                textColor = Color.magenta;
                popupText = "Strength Down";
                break;
            case EffectType.defense_down:
                textColor = Color.magenta;
                popupText = "Defense Down";
                break;
            case EffectType.critical_down:
                textColor = Color.magenta;
                popupText = "Critical Down";
                break;
            case EffectType.evasion_down:
                textColor = Color.magenta;
                popupText = "Evasion Down";
                break;
        }
    }

    public void Activate(MovingEntity entity)
    {
        switch (effectId)
        {
            case EffectType.health_regen:
                // Heal 10% max health
                int healing = (int)(entity.getMaxHP() * 0.1);
                entity.takeDamage(healing, textColor, false, false);
                break;
            case EffectType.mana_regen:
                // Heal 10% max health
                int mana = (int)(entity.getMaxMP() * 0.1);
                entity.addMp(mana);
                break;
            case EffectType.poison:
                // Damage 10% max health
                int poisonDamage = (int)(entity.getMaxHP() * 0.1);
                entity.takeDamage(-poisonDamage, textColor, false, false);
                break;
            case EffectType.bleed:
                // Damage 5% max health
                int bleedDamage = (int)(entity.getMaxHP() * 0.05);
                entity.takeDamage(-bleedDamage, textColor, false, false);
                break;
            case EffectType.burn:
                int burnDamage = (int)(entity.getMaxHP() * 0.05);
                entity.takeDamage(-burnDamage, textColor, false, false);
                break;
            case EffectType.freeze:
                int freezeDamage = (int)(entity.getMaxHP() * 0.05);
                entity.takeDamage(-freezeDamage, textColor, false, false);
                break;
            case EffectType.electric:
                int electricDamage = (int)(entity.getMaxHP() * 0.05);
                entity.takeDamage(-electricDamage, textColor, false, false);
                break;
            case EffectType.paralysis:
                // 50% chance to skip turn
                float r = Random.Range(0f, 1f);
                //Debug.Log("COIN: " + r);
                if (r > 0.5f)
                {
                    //Debug.Log("ZAP");
                    entity.AddTextPopup("Paralyzed", textColor);
                    //entity.SkipTurn();
                }
                break;
            case EffectType.silence:
                entity.AddTextPopup("Silenced", textColor);
                entity.silenced = true;
                break;
            case EffectType.stealth:
                entity.AddTextPopup("Stealth", textColor);
                entity.stealth = true;
                break;
            case EffectType.invisible:
                entity.AddTextPopup("Invisible", textColor);
                entity.invisible = true;
                break;
            case EffectType.root:
                entity.AddTextPopup("Rooted", textColor);
                entity.root = true;
                break;
            case EffectType.sleep:
                entity.AddTextPopup("Sleep", textColor);
                break;
            case EffectType.strength_up:
                // Increase player strength by 10%
                entity.setStrengthScale(entity.getStrengthScale() + 0.1f);
                break;
            case EffectType.defense_up:
                // Increase player defense by 10%
                entity.setDefenseScale(entity.getDefenseScale() + 0.1f);
                Player player = (Player)entity;
                player.UpdateHealthAndDefense();
                break;
            case EffectType.critical_up:
                // Increase player critical by 10%
                entity.setCriticalScale(entity.getCriticalScale() + 0.1f);
                break;
            case EffectType.evasion_up:
                // Increase player evasion by 10%
                entity.setEvadeScale(entity.getEvadeScale() + 0.1f);
                break;
            case EffectType.strength_down:
                // Decrease player strength by 10%
                entity.setStrengthScale(entity.getStrengthScale() - 0.1f);
                break;
            case EffectType.defense_down:
                // Decrease player defense by 10%
                entity.setDefenseScale(entity.getDefenseScale() - 0.1f);
                player = (Player)entity;
                player.UpdateHealthAndDefense();
                break;
            case EffectType.critical_down:
                // Decrease player critical by 10%
                entity.setCriticalScale(entity.getCriticalScale() - 0.1f);
                break;
            case EffectType.evasion_down:
                // Decrease player evasion by 10%
                entity.setEvadeScale(entity.getEvadeScale() - 0.1f);
                break;
        }
    }

    public void Deactivate(MovingEntity entity)
    {
        GameManager.gmInstance.Log.AddLog($">{entity.entityName} is no longer affected by {popupText}.");
        switch (effectId)
        {
            case EffectType.strength_up:
                entity.setStrengthScale(entity.getStrengthScale() - 0.1f);
                break;
            case EffectType.strength_down:
                entity.setStrengthScale(entity.getStrengthScale() + 0.1f);
                break;
            case EffectType.defense_up:
                entity.setDefenseScale(entity.getDefenseScale() - 0.1f);
                Player player = (Player)entity;
                player.UpdateHealthAndDefense();
                break;
            case EffectType.defense_down:
                entity.setDefenseScale(entity.getDefenseScale() + 0.1f);
                player = (Player)entity;
                player.UpdateHealthAndDefense();
                break;
            case EffectType.critical_up:
                entity.setCriticalScale(entity.getCriticalScale() - 0.1f);
                break;
            case EffectType.critical_down:
                entity.setCriticalScale(entity.getCriticalScale() + 0.1f);
                break;
            case EffectType.evasion_up:
                entity.setEvadeScale(entity.getEvadeScale() - 0.1f);
                break;
            case EffectType.evasion_down:
                entity.setEvadeScale(entity.getEvadeScale() + 0.1f);
                break;
            case EffectType.stealth:
                entity.stealth = false;
                break;
            case EffectType.invisible:
                entity.invisible = false;
                break;
            case EffectType.silence:
                entity.silenced = false;
                break;
            case EffectType.root:
                entity.root = false;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class StatusEffect
{

    public enum EffectType
    {
        health_regen,
        poison,
        bleed,
        paralysis,
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
        End
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
                textColor = new Color(52f / 255f, 219f / 255f, 37f / 255f);
                popupText = "HP Regen";
                break;
            case EffectType.poison:
                textColor = new Color(206f / 255f, 40f / 255f, 209f / 255f);
                popupText = "Poison";
                break;
            case EffectType.bleed:
                textColor = Color.red;
                popupText = "Bleed";
                break;
            case EffectType.paralysis:
                textColor = Color.yellow;
                popupText = "Paralysis";
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
                entity.takeDamage(healing, textColor);
                break;
            case EffectType.poison:
                // Damage 10% max health
                int poisonDamage = (int)(entity.getMaxHP() * 0.1);
                entity.takeDamage(-poisonDamage, textColor);
                break;
            case EffectType.bleed:
                // Damage 5% max health
                int bleedDamage = (int)(entity.getMaxHP() * 0.05);
                entity.takeDamage(-bleedDamage, textColor);
                break;
            case EffectType.paralysis:
                // 50% chance to skip turn
                float r = Random.Range(0f, 1f);
                Debug.Log("COIN: " + r);
                if (r > 0.5f)
                {
                    Debug.Log("ZAP");
                    entity.AddTextPopup("Paralyzed", textColor);
                    //entity.SkipTurn();
                }
                break;
            case EffectType.strength_up:
                break;
            case EffectType.defense_up:
                break;
            case EffectType.critical_up:
                break;
            case EffectType.evasion_up:
                break;
            case EffectType.strength_down:
                break;
            case EffectType.defense_down:
                break;
            case EffectType.critical_down:
                break;
            case EffectType.evasion_down:
                break;
        }
    }
}

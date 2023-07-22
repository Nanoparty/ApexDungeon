using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static StatusEffect;

public class StatusEffectCard : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image IconImage;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DescriptionText;
    [SerializeField] private TMP_Text DurationText;

    [Header("Icons")]
    [SerializeField] private Sprite hp_regen_sprite;
    [SerializeField] private Sprite mp_regen_sprite;
    [SerializeField] private Sprite bleed_sprite;
    [SerializeField] private Sprite poison_sprite;
    [SerializeField] private Sprite paralysis_sprite;
    [SerializeField] private Sprite burn_sprite;
    [SerializeField] private Sprite freeze_sprite;
    [SerializeField] private Sprite electric_sprite;
    [SerializeField] private Sprite sleep_sprite;
    [SerializeField] private Sprite silenced_sprite;
    [SerializeField] private Sprite invisible_sprite;
    [SerializeField] private Sprite stealth_sprite;
    [SerializeField] private Sprite root_sprite;

    [SerializeField] private Sprite strength_up_sprite;
    [SerializeField] private Sprite strength_down_sprite;
    [SerializeField] private Sprite critical_up_sprite;
    [SerializeField] private Sprite critical_down_sprite;
    [SerializeField] private Sprite evasion_up_sprite;
    [SerializeField] private Sprite evasion_down_sprite;
    [SerializeField] private Sprite defense_up_sprite;
    [SerializeField] private Sprite defense_down_sprite;

    private string effectName;
    private string description;
    private Sprite iconSprite;

    public void Setup(EffectType effect, int turns)
    {
        switch (effect)
        {
            case EffectType.health_regen:
                effectName = "HP Regeneration";
                description = "Restores 10% of Max HP Each Turn";
                iconSprite = hp_regen_sprite;
                break;
            case EffectType.mana_regen:
                effectName = "MP Regeneration";
                description = "Restores 10% of Max MP Each Turn";
                iconSprite = mp_regen_sprite;
                break;
            case EffectType.bleed:
                effectName = "Bleeding";
                description = "Lose 5% of Max HP Each Turn";
                iconSprite = bleed_sprite;
                break;
            case EffectType.poison:
                effectName = "Poisoned";
                description = "Lose 10% of Max HP Each Turn";
                iconSprite = poison_sprite;
                break;
            case EffectType.burn:
                effectName = "Burned";
                description = "Lose 5% of Max HP Each Turn";
                iconSprite = burn_sprite;
                break;
            case EffectType.freeze:
                effectName = "FrostBite";
                description = "Lost 5% of Max HP Each Turn";
                iconSprite = freeze_sprite;
                break;
            case EffectType.electric:
                effectName = "Electrified";
                description = "Lost 5% of Max HP Each Turn";
                iconSprite = electric_sprite;
                break;
            case EffectType.sleep:
                effectName = "Sleep";
                description = "Skip Turn Until Awake";
                iconSprite = sleep_sprite;
                break;
            case EffectType.invisible:
                effectName = "Invisible";
                description = "Undetectable by enemies. Broken on attack.";
                iconSprite = invisible_sprite;
                break;
            case EffectType.stealth:
                effectName = "Stealth";
                description = "Reduce enemy detection range.";
                iconSprite = stealth_sprite;
                break;
            case EffectType.root:
                effectName = "Root";
                description = "Unable to move.";
                iconSprite = root_sprite;
                break;
            case EffectType.paralysis:
                effectName = "Paralyzed";
                description = "50% Chance to Skip Turn";
                iconSprite = paralysis_sprite;
                break;
            case EffectType.silence:
                effectName = "Silenced";
                description = "Cannot Use Skills";
                iconSprite = silenced_sprite;
                break;
            case EffectType.strength_up:
                effectName = "Strength Up";
                description = "Strength Increased by 10%";
                iconSprite = strength_up_sprite;
                break;
            case EffectType.critical_up:
                effectName = "Critical Up";
                description = "Critical Increased by 10%";
                iconSprite = critical_up_sprite;
                break;
            case EffectType.evasion_up:
                effectName = "Evasion Up";
                description = "Evasion Increased by 10%";
                iconSprite = evasion_up_sprite;
                break;
            case EffectType.defense_up:
                effectName = "Defense Up";
                description = "Defense Increased by 10%";
                iconSprite = defense_up_sprite;
                break;
            case EffectType.strength_down:
                effectName = "Strength Down";
                description = "Strength Decreased by 10%";
                iconSprite = strength_down_sprite;
                break;
            case EffectType.critical_down:
                effectName = "Critical Down";
                description = "Critical Decreased by 10%";
                iconSprite = critical_down_sprite;
                break;
            case EffectType.evasion_down:
                effectName = "Evasion Down";
                description = "Evasion Decreased by 10%";
                iconSprite = evasion_down_sprite;
                break;
            case EffectType.defense_down:
                effectName = "Defense Down";
                description = "Defense Decreased by 10%";
                iconSprite = defense_down_sprite;
                break;
        }

        IconImage.sprite = iconSprite;
        NameText.SetText(effectName);
        DescriptionText.SetText(description);
        DurationText.SetText("Remaining Turns: " + turns);
    }
}

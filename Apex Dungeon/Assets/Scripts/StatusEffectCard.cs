using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectCard : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image IconImage;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DescriptionText;
    [SerializeField] private TMP_Text DurationText;

    [Header("Icons")]
    [SerializeField] private Sprite hp_regen_sprite;
    [SerializeField] private Sprite bleed_sprite;
    [SerializeField] private Sprite poison_sprite;
    [SerializeField] private Sprite paralysis_sprite;

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

    public void Setup(string effect, int turns)
    {
        switch (effect)
        {
            case "hp_regeneration":
                effectName = "HP Regeneration";
                description = "Restores 10% of Max HP Each Turn";
                iconSprite = hp_regen_sprite;
                break;
            case "bleed":
                effectName = "Bleeding";
                description = "Lose 5% of Max HP Each Turn";
                iconSprite = bleed_sprite;
                break;
            case "poison":
                effectName = "Poisoned";
                description = "Lose 10% of Max HP Each Turn";
                iconSprite = poison_sprite;
                break;
            case "paralysis":
                effectName = "Paralyzed";
                description = "50% Chance to Skip Turn";
                iconSprite = paralysis_sprite;
                break;
            case "strength_up":
                effectName = "Strength Up";
                description = "Strength Increased by 10%";
                iconSprite = strength_up_sprite;
                break;
            case "critical_up":
                effectName = "Critical Up";
                description = "Critical Increased by 10%";
                iconSprite = critical_up_sprite;
                break;
            case "evasion_up":
                effectName = "Evasion Up";
                description = "Evasion Increased by 10%";
                iconSprite = evasion_up_sprite;
                break;
            case "defense_up":
                effectName = "Defense Up";
                description = "Defense Increased by 10%";
                iconSprite = defense_up_sprite;
                break;
            case "strength_down":
                effectName = "Strength Down";
                description = "Strength Decreased by 10%";
                iconSprite = strength_down_sprite;
                break;
            case "critical_down":
                effectName = "Critical Down";
                description = "Critical Decreased by 10%";
                iconSprite = critical_down_sprite;
                break;
            case "evasion_down":
                effectName = "Evasion Down";
                description = "Evasion Decreased by 10%";
                iconSprite = evasion_down_sprite;
                break;
            case "defense_down":
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

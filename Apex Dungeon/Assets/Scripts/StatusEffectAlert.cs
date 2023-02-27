using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static StatusEffect;

public class StatusEffectAlert : MonoBehaviour
{
    [SerializeField] private Image EffectIcon;
    [SerializeField] private TMP_Text DurationText;

    [SerializeField] private Sprite hp_regen;
    [SerializeField] private Sprite poison;
    [SerializeField] private Sprite bleed;
    [SerializeField] private Sprite paralysis;
    [SerializeField] private Sprite burn;
    [SerializeField] private Sprite freeze;
    [SerializeField] private Sprite electric;
    [SerializeField] private Sprite sleep;
    [SerializeField] private Sprite strength_up;
    [SerializeField] private Sprite strength_down;
    [SerializeField] private Sprite defense_up;
    [SerializeField] private Sprite defense_down;
    [SerializeField] private Sprite critical_up;
    [SerializeField] private Sprite critical_down;
    [SerializeField] private Sprite evasion_up;
    [SerializeField] private Sprite evasion_down;

    public void Setup(EffectType effect, int duration)
    {
        DurationText.SetText(duration.ToString());

        switch (effect)
        {
            case EffectType.health_regen:
                EffectIcon.sprite = hp_regen;
                break;
            case EffectType.bleed:
                EffectIcon.sprite = bleed;
                break;
            case EffectType.poison:
                EffectIcon.sprite = poison;
                break;
            case EffectType.burn:
                EffectIcon.sprite = burn;
                break;
            case EffectType.freeze:
                EffectIcon.sprite = freeze;
                break;
            case EffectType.electric:
                EffectIcon.sprite = electric;
                break;
            case EffectType.sleep:
                EffectIcon.sprite = sleep;
                break;
            //case EffectType.paralysis:
            //    EffectIcon.sprite = paralysis;
            //    break;
            case EffectType.strength_up:
                EffectIcon.sprite = strength_up;
                break;
            case EffectType.defense_up:
                EffectIcon.sprite = defense_up;
                break;
            case EffectType.evasion_up:
                EffectIcon.sprite = evasion_up;
                break;
            case EffectType.critical_up:
                EffectIcon.sprite = critical_up;
                break;
            case EffectType.strength_down:
                EffectIcon.sprite = strength_down;
                break;
            case EffectType.defense_down:
                EffectIcon.sprite = defense_down;
                break;
            case EffectType.evasion_down:
                EffectIcon.sprite = evasion_down;
                break;
            case EffectType.critical_down:
                EffectIcon.sprite = critical_down;
                break;
        }
    }
}

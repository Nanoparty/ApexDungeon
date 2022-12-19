using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectAlert : MonoBehaviour
{
    [SerializeField] private Image EffectIcon;
    [SerializeField] private TMP_Text DurationText;

    [SerializeField] private Sprite hp_regen;
    [SerializeField] private Sprite poison;
    [SerializeField] private Sprite bleed;
    [SerializeField] private Sprite paralysis;
    [SerializeField] private Sprite strength_up;
    [SerializeField] private Sprite strength_down;
    [SerializeField] private Sprite defense_up;
    [SerializeField] private Sprite defense_down;
    [SerializeField] private Sprite critical_up;
    [SerializeField] private Sprite critical_down;
    [SerializeField] private Sprite evasion_up;
    [SerializeField] private Sprite evasion_down;

    public void Setup(string effect, int duration)
    {
        DurationText.SetText(duration.ToString());

        switch (effect)
        {
            case "hp_regeneration":
                EffectIcon.sprite = hp_regen;
                break;
            case "bleed":
                EffectIcon.sprite = bleed;
                break;
            case "poison":
                EffectIcon.sprite = poison;
                break;
            case "paralysis":
                EffectIcon.sprite = paralysis;
                break;
            case "strength_up":
                EffectIcon.sprite = strength_up;
                break;
            case "defense_up":
                EffectIcon.sprite = defense_up;
                break;
            case "evasion_up":
                EffectIcon.sprite = evasion_up;
                break;
            case "critical_up":
                EffectIcon.sprite = critical_up;
                break;
            case "strength_down":
                EffectIcon.sprite = strength_down;
                break;
            case "defense_down":
                EffectIcon.sprite = defense_down;
                break;
            case "evasion_down":
                EffectIcon.sprite = evasion_down;
                break;
            case "critical_down":
                EffectIcon.sprite = critical_down;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MagicTrap : Trap
{
    private Animator anim;

    [SerializeField] private TrapType type;
    [SerializeField] private Sprite whiteCircle;
    [SerializeField] private Sprite blueCircle;
    [SerializeField] private Sprite redCircle;

    private enum TrapType
    {
        poison,
        bleed,
        teleport,
        hp_regen,
        strength_up,
        strength_down,
        defense_up,
        defense_down,
        critical_up,
        critical_down,
        evasion_up,
        evasion_down
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Setup(int row, int col, int floor)
    {
        this.row = row;
        this.col = col;
        System.Array values = System.Enum.GetValues(typeof(TrapType));
        type = (TrapType)values.GetValue(Random.Range(0, values.Length));
        GameManager.gmInstance.AddTrapToList(this);
    }

    public override void TriggerTrap(MovingEntity me)
    {
        if (disarmed) return;

        if (!typeof(Player).IsInstanceOfType(me))
        {
            return;
        }

        disarmed = true;

        if (typeof(Player).IsInstanceOfType(me))
        {
            Player player = (Player)me;
            anim.enabled = false;
            SoundManager.sm.PlayMagicSound();

            if (type == TrapType.bleed)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.bleed, 5, StatusEffect.EffectOrder.End));
                sr.sprite = redCircle;
            }
            if (type == TrapType.poison)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.poison, 5, StatusEffect.EffectOrder.End));
                sr.sprite = redCircle;
            }
            if (type == TrapType.teleport)
            {
                Vector2 pos = GameManager.gmInstance.Dungeon.getRandomUnoccupiedTile();
                player.SetPosition((int)pos.x, (int)pos.y);
                player.DoneMoving();
                player.interrupt = true;
                
                sr.sprite = whiteCircle;
            }
            if (type == TrapType.hp_regen)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.health_regen, 5, StatusEffect.EffectOrder.End));
                sr.sprite = blueCircle;
            }
            if (type == TrapType.strength_up)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.strength_up, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = blueCircle;
            }
            if (type == TrapType.defense_up)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.defense_up, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = blueCircle;
            }
            if (type == TrapType.critical_up)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.critical_up, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = blueCircle;
            }
            if (type == TrapType.evasion_up)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.evasion_up, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = blueCircle;
            }
            if (type == TrapType.strength_down)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.strength_down, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = redCircle;
            }
            if (type == TrapType.defense_down)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.defense_down, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = redCircle;
            }
            if (type == TrapType.critical_down)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.critical_down, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = redCircle;
            }
            if (type == TrapType.evasion_down)
            {
                player.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.evasion_down, 20, StatusEffect.EffectOrder.Status));
                sr.sprite = redCircle;
            }
        }
        

        
    }

    public override bool DisarmTrap(MovingEntity e)
    {
        return false;
    }

    public override void DestroyTrap()
    {
        base.DestroyTrap();
    }

    public override bool canActivate(MovingEntity e)
    {
        if (typeof(Enemy).IsInstanceOfType(e))
        {
            return false;
        }
        return true;
    }
}

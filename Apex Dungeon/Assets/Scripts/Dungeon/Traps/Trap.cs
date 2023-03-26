using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap : MonoBehaviour
{
    public string trapName = "Trap";

    protected int row;
    protected int col;
    protected bool disarmed;
    protected int damage;



    protected SpriteRenderer sr;

    [SerializeField] protected int baseDamage = 15;
    [SerializeField] protected ParticleSystem destruction;
    [SerializeField] protected GameObject disarmText;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (GameManager.gmInstance.Dungeon == null) return;

        if (!GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
        {
            sr.enabled = false;
        }
        else
        {
            sr.enabled = true;
        }
    }

    public virtual void Setup(int row, int col, int floor)
    {
        this.row = row;
        this.col = col;
        damage = (int)(baseDamage + baseDamage * 0.1 * (floor - 1));
        GameManager.gmInstance.AddTrapToList(this);
    }

    public virtual void TriggerTrap(MovingEntity me)
    {
        if (disarmed) return;

        me.TakeDamage(-damage, Color.red);
        me.AddStatusEffect(new StatusEffect(StatusEffect.EffectType.bleed, 5, StatusEffect.EffectOrder.End));

        DestroyTrap();
    }

    public virtual bool DisarmTrap(MovingEntity e)
    {
        e.AddTextPopup("DISARM", Color.white);
        DestroyTrap();
        return true;
    }

    public virtual void DestroyTrap()
    {
        if (GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
        {
            SoundManager.sm.PlayTrapSound();
        }
        disarmed = true;
        Instantiate(destruction, new Vector2(col, row), Quaternion.identity);
        GameManager.gmInstance.RemoveTrap(this);
        Destroy(gameObject);
    }

    public int GetRow() { return row; }
    public int GetCol() { return col; }

    public virtual bool canActivate(MovingEntity e)
    {
        
        return true;
    }
}

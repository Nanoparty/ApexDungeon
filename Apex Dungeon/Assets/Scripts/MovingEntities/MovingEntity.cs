using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static StatusEffect;

public abstract class MovingEntity : MonoBehaviour
{
    public string entityName;

    protected int maxHp;
    protected int maxMp;
    protected int hp;
    protected int baseHp;
    protected int mp;
    protected int expLevel;
    protected int exp;
    protected int maxExp;

    // Entity Stats
    protected int attack;
    protected int defense;
    protected int strength;
    protected int intelligence;
    protected int critical;
    protected int evade;
    protected int blockStat;

    // Combat
    protected int attackRange;

    // Resistances
    protected float fireResistance;
    protected float iceResistance;
    protected float lightningResistance;
    protected float poisonResistance;

    // Active Effects
    public bool silenced;
    public bool stealth;
    public bool invisible;
    public bool root;

    protected float attackScale = 1f;
    protected float defenseScale = 1f;
    protected float strengthScale = 1f;
    protected float intelligenceScale = 1f;
    protected float criticalScale = 1f;
    protected float evadeScale = 1f;
    protected float blockScale = 1f;

    protected bool dead;

    public bool agro;
    public int agroRange;

    public List<StatusEffect> statusEffects;
    public List<Skill> skills;

    public Item heldItem;
    public int heldMoney;

    public Queue<(string, Color)> popupTexts;
    public bool canDisplayPopupText = true;
    [SerializeField] public float popupTextDelay = .4f;

    public float moveTime = 0.1f;
    public float speed = 3f;
    public LayerMask blockingLayer;

    public ParticleSystem blood;
    public GameObject damageText;

    protected BoxCollider2D boxCollider;

    private Vector2 target;
    protected bool atTarget = true;

    protected int row;
    protected int col;

    protected int type = 2;

    private Pathfinder pathing = new Pathfinder();
    private Path path = new Path();

    protected bool moving = false;
    protected bool moved = true;
    
    protected virtual void Start()
    {
        dead = false;
        boxCollider = GetComponent<BoxCollider2D>();
        row = (int)transform.position.y;
        col = (int)transform.position.x;

        statusEffects = new List<StatusEffect>();
        popupTexts = new Queue<(string, Color)> ();
        canDisplayPopupText = true;
        skills = new List<Skill>();
    }

    bool checkValidPath()
    {
        if (path == null || (path.nodes.Count == 0))
        {
            doneMoving();
            return false;
        }
        return true;
    }

    public void CancelPath()
    {
        moving = false;
        atTarget = true;
    }

    bool checkValidTile(Vector2 next)
    {
        if (GameManager.gmInstance.Dungeon.tileMap[(int)next.y, (int)next.x].getBlocked())
        {
            doneMoving();
            return false;

        }
        return true;
    }

    void setMapOccupancy()
    {
        GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
        GameManager.gmInstance.Dungeon.tileMap[(int)target.y, (int)target.x].occupied = type;
    }

    public GameObject SpawnObject(GameObject o, Vector2 pos)
    {
        return Instantiate(o, pos, Quaternion.identity);
    }

    void updateLocalPosition()
    {
        row = (int)target.y;
        col = (int)target.x;
    }

    protected bool Move(int r, int c)
    {
        Tile startTile = GameManager.gmInstance.Dungeon.tileMap[row, col];
        Tile endTile = GameManager.gmInstance.Dungeon.tileMap[r, c];
        
        //If destination is current location
        if (row == r && col == c){
            return false;
        }

        if (!moving && atTarget)
        {
            if (GameManager.gmInstance.Dungeon.tileMap[r, c].getWall())
            {
                return false;
            }
            path = pathing.findPath(startTile, endTile);

            if (!checkValidPath()){
                return false;
            }
            
            Vector2 next = path.nodes.Peek();

            if (!checkValidTile(next))
            {
                return false;
            }

            moving = true;
            atTarget = false;
            target = path.nodes.Dequeue();

            setMapOccupancy();
            updateLocalPosition();

            return true;
        }
        return false;
    }

    protected virtual void setNextTarget()
    {
        if (atTarget)
        {
            if (!checkValidPath())
            {
                return;
            }
            else
            {
                target = path.nodes.Dequeue();

                if (!GameManager.gmInstance.Dungeon.tileMap[(int)target.y, (int)target.x].getBlocked())
                {
                    moving = true;
                    atTarget = false;

                    setMapOccupancy();
                    updateLocalPosition();
                }
                else
                {
                    //Interupt Path
                    doneMoving();
                    return;
                }
            }
        }   
    }

    protected virtual void Update()
    {
        if (GameManager.gmInstance.Dungeon == null) return;

        if (!atTarget)
        {
            moveToTarget();
        }

        if (canDisplayPopupText && popupTexts.Count > 0)
        {
            canDisplayPopupText = false;
            var values = popupTexts.Dequeue();
            StartCoroutine(SpawnText(values.Item1, values.Item2));
        }
    }

    protected void moveToTarget()
    {
        float distancex = target.x - transform.position.x;
        float distancey = target.y - transform.position.y;

        if (distancex > 0)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        if (distancex < 0)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        if (distancey > 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        if (distancey < 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }

        if (Mathf.Abs(distancex) < 0.1 && Mathf.Abs(distancey) < 0.1)
        {
            atTarget = true;
            transform.position = new Vector2(target.x, target.y);
            if ((path.nodes.Count == 0))
            {
                moving = false;
            }
        }
    }

    protected virtual bool AttemptMove <T>(int r, int c) where T : Component
    {
        bool canMove = Move(r, c);

        if(!canMove)
        {
            return false;
        }
        return true;
    }

    protected abstract void OnCantMove<T>(T Component) where T : Component;

    public float calculateDamage(float m = 1f)
    {
        int scaledStrength = (int)(strength * strengthScale);
        float increase = (float)(attack * (scaledStrength * 0.05));
        float attackDamage = (float)(attack + increase);
        attackDamage *= m;
        return -attackDamage;
    }

    public virtual void takeDamage(float change, Color color, bool critical = false, bool canDodge = true)
    {
        hp += (int)change;
        change = Mathf.Floor(change);

        if (change < 0)
        {
            if (critical)
            {
                AddTextPopup($"CRIT! {(int)change}", color);
            }
            else
            {
                AddTextPopup($"{(int)change}", color);
            }
            moving = false;
           
            SpawnBlood();
        }
        else
        {
            SoundManager.sm.PlayHealSound();
            AddTextPopup($"+{(int)change}", color);
        }
        
        if(hp <= 0)
        {
            dead = true;
            SpawnBlood();
        }
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public virtual void AttackTarget()
    {

    }

    public virtual void AddTextPopup(string text, Color color)
    {
        popupTexts.Enqueue((text, color));
    }

    private IEnumerator SpawnText(string text, Color color)
    {
        canDisplayPopupText = false;
        GameObject popup = GameObject.Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        popup.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{text}";
        popup.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = color;
        yield return new WaitForSeconds(popupTextDelay);
        canDisplayPopupText = true;
    }

    protected void SpawnBlood()
    {
        ParticleSystem bloodSpray = Instantiate(blood, transform.position, Quaternion.identity);
        bloodSpray.transform.parent = transform;
    }

    public virtual void AddStatusEffect(StatusEffect se)
    {
        statusEffects.Add(se);
        AddTextPopup(se.popupText, se.textColor);

        GameManager.gmInstance.Log.AddLog($">{entityName} is afflicted with {se.popupText}.");

        if (se.order == StatusEffect.EffectOrder.Status)
        {
            se.Activate(this);
        }
    }

    protected void UpdateStatusEffectDuration()
    {
        foreach (StatusEffect e in statusEffects)
        {
            e.duration -= 1;
            if (e.duration == 0) e.Deactivate(this);
        }
        statusEffects.RemoveAll(e => e.duration == 0);
    }

    public virtual void RemoveAllStatusEffect(EffectType type)
    {
        foreach (StatusEffect e in statusEffects)
        {
            if (e.effectId == type)
            {
                e.Deactivate(this);
            }
        }
        statusEffects.RemoveAll(e => e.effectId == type);
    }

    protected virtual void ApplyStatusEffects(string time)
    {
        if (time == "start")
        {
            foreach (StatusEffect e in statusEffects)
            {
                if (e.order == EffectOrder.Start) e.Activate(this);
            }
        }
        if (time == "end")
        {
            foreach (StatusEffect e in statusEffects)
            {
                if (e.order == EffectOrder.End) e.Activate(this);
            }
        }
    }

    public virtual void SkipTurn()
    {
        Debug.Log("ENTITY SKIP TURN");
    }

    //public Enumerable PauseForSeconds(float seconds)
    //{

    //}

    public virtual void CalculateStats()
    {

    }

    public void addMP(float change)
    {
        mp += (int)change;
    }

    public int getRow()
    {
        return row;
    }

    public int getCol()
    {
        return col;
    }

    public int getHP()
    {
        return hp;
    }

    public int getMP()
    {
        return mp;
    }

    public int getMaxHP()
    {
        return maxHp;
    }

    public int getMaxMP()
    {
        return maxMp;
    }

    private void doneMoving()
    {
        moving = false;
        atTarget = true;
    }

    public float getStrengthScale()
    {
        return strengthScale;
    }
    public float getCriticalScale()
    {
        return criticalScale;
    }
    public float getDefenseScale()
    {
        return defenseScale;
    }
    public float getEvadeScale()
    {
        return evadeScale;
    }

    public void setStrengthScale(float f)
    {
        strengthScale = f;
    }
    public void setDefenseScale(float f)
    {
        defenseScale = f;
    }
    public void setCriticalScale(float f)
    {
        criticalScale = f;
    }
    public void setEvadeScale(float f)
    {
        evadeScale = f;
    }

    private void UpdateShadows(int r, int c)
    {
        GameManager.gmInstance.Dungeon.UpdateShadows(r, c);
    }

    public void setHP(int i){
        this.hp = i;
        if (hp > maxHp) hp = maxHp;
        if (hp < 0) hp = 0;
    }

    public void addHp(int i)
    {
        this.hp += i;
        if (hp > maxHp) hp = maxHp;
        if (hp < 0) hp = 0;
    }

    public void addMp(int i)
    {
        this.mp += i;
        if (i > 0)
        {
            AddTextPopup($"MP +{i}", ColorManager.MANA);
        }
        if (i < 0)
        {
            AddTextPopup($"MP {i}", ColorManager.MANA);
        }

        if (mp > maxMp) mp = maxMp;
        if (mp < 0) mp = 0;
    }

    public void setPosition(int r, int c){
        GameManager.gmInstance.Dungeon.tileMap[row,col].occupied = 0;
        gameObject.transform.position = new Vector3(c, r, 0f);
        this.row = r;
        this.col = c;
        GameManager.gmInstance.Dungeon.tileMap[r,c].occupied = 1;
    }


}

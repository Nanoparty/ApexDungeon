using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using static StatusEffect;

public abstract class MovingEntity : MonoBehaviour
{
    [Header("Name")]
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
    protected int dexterity;
    protected int constitution;
    protected int critical;
    protected int evade;
    protected int blockStat;

    // Combat
    public int attackRange;

    // Resistances
    protected float physicalResistance;
    protected float magicResistance;
    protected float fireResistance;
    protected float iceResistance;
    protected float lightningResistance;
    protected float poisonResistance;

    [Header("Active Effects")]
    public bool silenced;
    public bool stealth;
    public bool invisible;
    public bool root;
    public bool skipTurn;
    public bool interrupt;
    public bool sleeping;

    protected float attackScale = 1f;
    protected float defenseScale = 1f;
    protected float strengthScale = 1f;
    protected float intelligenceScale = 1f;
    protected float criticalScale = 1f;
    protected float evadeScale = 1f;
    protected float blockScale = 1f;

    protected bool dead;

    [Header("Agro")]
    public bool agro;
    public int agroRange;

    public List<StatusEffect> statusEffects;
    public List<Skill> skills;

    [Header("Held Items")]
    public Item heldItem;
    public int heldMoney;

    [Header("Popup Text")]
    public Queue<(string, Color)> popupTexts;
    public bool canDisplayPopupText = true;
    [SerializeField] public float popupTextDelay = .4f;

    [Header("Sleep Effect")]
    public GameObject sleepEffectPrefab;
    private GameObject sleepInstance;

    [Header("Movement")]
    public float moveTime = 0.1f;
    public float speed = 3f;
    public LayerMask blockingLayer;

    [Header("Particles")]
    public ParticleSystem blood;
    public GameObject damageText;

    protected BoxCollider2D boxCollider;

    public Vector2 target;
    protected bool atTarget = true;

    protected int row;
    protected int col;

    protected int type = 2;

    protected Pathfinder pathing = new Pathfinder();
    protected Path path = new Path();

    protected Animator animator;

    protected bool moving = false;
    protected bool moved = true;
    
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        statusEffects = new List<StatusEffect>();
        popupTexts = new Queue<(string, Color)>();
        skills = new List<Skill>();
        animator = this.transform.GetChild(1).gameObject.GetComponent<Animator>();

        dead = false;
        row = (int)transform.position.y;
        col = (int)transform.position.x;
        canDisplayPopupText = true;
    }

    bool CheckValidPath(Path p)
    {
        return !(p == null || p.nodes.Count == 0);
    }

    bool CheckValidTile(Vector2 next)
    {
        if (GameManager.gmInstance.Dungeon.tileMap[(int)next.y, (int)next.x].getBlocked())
        {
            return false;
        }
        return true;
    }

    void SetMapOccupancy()
    {
        GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
        GameManager.gmInstance.Dungeon.tileMap[(int)target.y, (int)target.x].occupied = type;
    }

    protected void SetAttackAnimation(int enemyRow, int enemyCol)
    {
        if (enemyRow > row) animator.Play("AttackUp");
        if (enemyRow < row) animator.Play("AttackDown");
        if (enemyCol > col) animator.Play("AttackRight");
        if (enemyCol < col) animator.Play("AttackLeft");
    }

    public GameObject SpawnObject(GameObject o, Vector2 pos)
    {
        return Instantiate(o, pos, Quaternion.identity);
    }

    void UpdateLocalPosition()
    {
        row = (int)target.y;
        col = (int)target.x;
    }

    protected Path GeneratePath(Tile start, Tile end)
    {
        Path tempPath = pathing.findPath(start, end);
        return tempPath;
    }

    protected bool Move(int r, int c)
    {
        Tile startTile = GameManager.gmInstance.Dungeon.tileMap[row, col];
        Tile endTile = GameManager.gmInstance.Dungeon.tileMap[r, c];
        
        //If destination is current location
        if (row == r && col == c){
            return false;
        }

        if (root) { return false; }

        if (atTarget)
        {
            if (GameManager.gmInstance.Dungeon.tileMap[r, c].getWall())
            {
                return false;
            }

            if (path == null || path.nodes.Count == 0)
            {
                path = pathing.findPath(startTile, endTile);
            }
            

            if (!CheckValidPath(path)){
                return false;
            }
            
            Vector2 next = path.nodes.Peek();

            if (!CheckValidTile(next))
            {
                DoneMoving();
                return false;
            }

            moving = true;
            atTarget = false;
            target = path.nodes.Dequeue();

            SetMapOccupancy();
            UpdateLocalPosition();

            return true;
        }
        return false;
    }

    protected virtual void SetNextTarget()
    {
        if (atTarget)
        {
            if (path == null || path.nodes.Count == 0)
            {
                interrupt = false;
                moving = false;
                path = null;
                return;
            }

            target = path.nodes.Dequeue();

            if (GameManager.gmInstance.Dungeon.tileMap[(int)target.y, (int)target.x].getBlocked())
            {
                //Interupt Path
                interrupt = false;
                moving = false;
                path = null;
                return;
            }
            else
            {
                moving = true;
                atTarget = false;

                SetMapOccupancy();
                UpdateLocalPosition();
            }
        }   
    }

    protected bool SetNewPath(int r, int c)
    {
        Tile currentTile = null;
        if (atTarget)
        {
            currentTile = GameManager.gmInstance.Dungeon.tileMap[row, col];
        }
        else
        {
            currentTile = GameManager.gmInstance.Dungeon.tileMap[(int)target.y, (int)target.x];
        }
        Tile endTile = GameManager.gmInstance.Dungeon.tileMap[r, c];

        Path newPath = pathing.findPath(currentTile, endTile);

        if (CheckValidPath(newPath))
        {
            path = newPath;
            return true;
        }
        return false;
    }

    protected virtual void Update()
    {
        if (GameManager.gmInstance.Dungeon == null) return;

        if (!atTarget)
        {
            MoveToTarget();
        }

        if (sleeping && sleepInstance == null) StartSleep();
        if (!sleeping && sleepInstance != null) EndSleep();

        if (canDisplayPopupText && popupTexts.Count > 0)
        {
            canDisplayPopupText = false;
            var values = popupTexts.Dequeue();
            StartCoroutine(SpawnText(values.Item1, values.Item2));
        }
    }

    protected void MoveToTarget()
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
            interrupt = false;
            transform.position = new Vector2(target.x, target.y);
            if (path == null || path.nodes.Count == 0)
            {
                moving = false;
                path = null;
            }
        }
    }

    protected virtual bool AttemptMove <T>(int r, int c) where T : Component
    {
        bool canMove = Move(r, c);

        return canMove;
    }

    public float CalculateDamage(float m = 1f)
    {
        int scaledStrength = (int)(strength * strengthScale);
        float increase = (float)(attack * (scaledStrength * 0.05));
        float attackDamage = (float)(attack + increase);
        attackDamage *= m;
        return -attackDamage;
    }

    public virtual void TakeDamage(float change, Color color, bool critical = false, bool canDodge = true)
    {
        hp += (int)change;
        change = Mathf.Floor(change);

        if (change < 0)
        {
            sleeping = false;
            if (critical)
            {
                AddTextPopup($"CRIT! {(int)change}", color);
            }
            else
            {
                AddTextPopup($"{(int)change}", color);
            }
            moving = false;
            path = null;
           
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

    public virtual void CalculateStats()
    {

    }

    public int GetRow()
    {
        return row;
    }

    public int GetCol()
    {
        return col;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMP()
    {
        return mp;
    }

    public int GetMaxHP()
    {
        return maxHp;
    }

    public int GetMaxMP()
    {
        return maxMp;
    }

    public void DoneMoving()
    {
        moving = false;
        path = null;
        atTarget = true;
        interrupt = false;
    }

    public float GetStrengthScale()
    {
        return strengthScale;
    }
    public float GetCriticalScale()
    {
        return criticalScale;
    }
    public float GetDefenseScale()
    {
        return defenseScale;
    }
    public float GetEvadeScale()
    {
        return evadeScale;
    }

    public void SetStrengthScale(float f)
    {
        strengthScale = f;
    }
    public void SetDefenseScale(float f)
    {
        defenseScale = f;
    }
    public void SetCriticalScale(float f)
    {
        criticalScale = f;
    }
    public void SetEvadeScale(float f)
    {
        evadeScale = f;
    }

    public void AddMp(int i)
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

    public void SetPosition(int r, int c){
        GameManager.gmInstance.Dungeon.tileMap[row,col].occupied = 0;
        gameObject.transform.position = new Vector3(c, r, 0f);
        this.row = r;
        this.col = c;
        GameManager.gmInstance.Dungeon.tileMap[r,c].occupied = 1;
    }

    protected bool IsAdjacent(int r, int c)
    {
        int rDis = Mathf.Abs((r) - row);
        int cDis = Mathf.Abs(c - col);
        if ((rDis == 1 && cDis == 0) || (rDis == 0 && cDis == 1))
        {
            return true;
        }
        return false;
    }

    protected bool IsInAttackRange(int r, int c)
    {
        int rDis = Mathf.Abs(r - row);
        int cDis = Mathf.Abs(c - col);
        if (rDis + cDis <= attackRange)
        {
            Debug.Log("Is in attack range");
            return true;
        }
        return false;
    }

    public bool IsBlocked(int r, int c)
    {
        return GameManager.gmInstance.Dungeon.tileMap[r, c].getWall() || GameManager.gmInstance.Dungeon.tileMap[r, c].getVoid();
    }

    public void AddStrength(int i)
    {
        strength += i;
    }
    public void AddAttack(int i)
    {
        attack += i;
    }
    public void AddCrit(int i)
    {
        critical += i;
    }
    public void AddIntelligence(int i)
    {
        intelligence += i;
    }
    public void AddBlock(int i)
    {
        blockStat += i;
    }
    public void AddEvade(int i)
    {
        evade += i;
    }
    public void AddDexterity(int i)
    {
        dexterity += i;
    }
    public void AddConstitution(int i)
    {
        constitution += i;
    }

    public void SetStrength(int i)
    {
        strength = i;
    }
    public void SetDefense(int i)
    {
        defense = i;
        int newHp = baseHp + (int)((float)baseHp * 0.05f * (int)(defense * defenseScale));
        int diff = newHp - maxHp;
        maxHp += diff;
        hp += diff;
    }
    public void SetCritical(int i)
    {
        critical = i;
    }
    public void SetEvasion(int i)
    {
        evade = i;
    }
    public void SetDexterity(int i)
    {
        dexterity = i;
    }
    public void SetConstitution(int i)
    {
        constitution = i;
    }
    public int GetStrength()
    {
        return strength;
    }
    public int GetDefense()
    {
        return defense;
    }
    public int GetIntelligence()
    {
        return intelligence;
    }
    public int GetCritical()
    {
        return critical;
    }
    public int GetEvade()
    {
        return evade;
    }
    public int GetDexterity()
    {
        return dexterity;
    }
    public int GetConstitution()
    {
        return constitution;
    }
    public void AddMP(int i)
    {
        mp += i;
        if (mp > maxMp) mp = maxMp;
    }
    public void AddMaxMP(int i)
    {
        maxMp += i;
    }
    public void AddHP(int i)
    {
        hp += i;
        if (hp > maxHp) hp = maxHp;
        AddTextPopup($"+{i}", new Color(50f / 255f, 205f / 255f, 50f / 255f));
    }
    public void AddMaxHP(int i)
    {
        maxHp += i;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }
    public void AddBaseHP(int i)
    {
        baseHp += i;
        int newHp = baseHp + (int)((float)baseHp * 0.05f * (int)(defense * defenseScale));
        int diff = newHp - maxHp;
        maxHp += diff;
        hp += diff;

    }

    public void StartSleep()
    {
        sleepInstance = Instantiate(sleepEffectPrefab, transform.position, Quaternion.identity);
        sleepInstance.GetComponent<ParticleSystem>().Play();
    }

    public void EndSleep()
    {
        Destroy(sleepInstance);
    }

}

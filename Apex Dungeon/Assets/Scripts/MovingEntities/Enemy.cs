using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using static StatusEffect;

public class Enemy : MovingEntity
{
    Animator animator;
    SpriteRenderer sr;
    SpriteRenderer healthBar;

    private int pRow;
    private int pCol;

    Player player;

    bool pathFound = false;

    private Path path;
    private Pathfinder finder;
    private int floor;

    protected MovingEntity enemyTarget;

    protected List<Skill> availableSkills;

    [SerializeField] private float poisonChance = 0.01f;
    [SerializeField] private float bleedChance = 0.05f;

    protected override void Start()
    {
        floor = GameManager.gmInstance.level;

        //Values
        entityName = "Enemy";
        hp = 100;
        maxHp = 100;
        defense = 5;
        agroRange = 7;
        pRow = pCol = -1;

        setStatsByFloor(floor);

        //Objects
        GameManager.gmInstance.AddEnemyToList(this);
        animator = this.transform.GetChild(1).gameObject.GetComponent<Animator>();
        sr = this.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        healthBar = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        path = new Path();
        finder = new Pathfinder();
        availableSkills = new List<Skill>();

        base.Start();

    }

    protected override void Update()
    {
        if (GameManager.gmInstance.Dungeon == null) return;

        base.Update();
        if (!GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
        {
            animator.enabled = false;
            sr.enabled = false;
            healthBar.enabled = false;
        }
        else
        {
            animator.enabled = true;
            sr.enabled = true;
            healthBar.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            if (!typeof(MagicTrap).IsInstanceOfType(other.GetComponent<Trap>()))
            {
                return;
            }
            if (other.GetComponent<Trap>().canActivate(this))
            {
                GameManager.gmInstance.Log.AddLog($">{entityName} activates {other.GetComponent<Trap>().trapName}.");
            }
            
            other.GetComponent<Trap>().TriggerTrap(this);
        }
        if (other.gameObject.tag == "Consumable")
        {
            if (heldItem != null) return;
            heldItem = other.GetComponent<Pickup>().GetItem();
            Destroy(other.gameObject);
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            GameManager.gmInstance.Log.AddLog($">{entityName} picks up {heldItem.itemName}.");
        }
        if (other.gameObject.tag == "Gold" || other.gameObject.tag == "Silver" || other.gameObject.tag == "Copper")
        {
            int amount = other.GetComponent<Money>().amount;
            heldMoney += amount;
            AddTextPopup($"+{amount}", new Color(255f / 255f, 238f / 255f, 0f / 255f));
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            Destroy(other.gameObject);
            GameManager.gmInstance.Log.AddLog($">{entityName} picks up {amount} gold.");

        }
        if (other.gameObject.tag == "Equipment")
        {
            if (heldItem != null) return;
            heldItem = other.GetComponent<Pickup>().GetItem();
            Destroy(other.gameObject);
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            GameManager.gmInstance.Log.AddLog($">{entityName} picks up {heldItem.itemName}.");
        }
    }

    protected override bool AttemptMove<T>(int r, int c)
    {
        return base.AttemptMove<T>(r, c);
    }

    private void setStatsByFloor(int floor){
        int basehp = 50;
        hp = (int)(basehp + basehp * 0.2 * (floor-1));
        maxHp = hp;

        mp = 100 * floor;
        maxMp = mp;

        int basedamage = 15;
        attack = (int)(basedamage + basedamage * 0.2 * (floor-1));
        defense = 5 + 1 * floor;
    }

    int calculateExp(){
        return 50 + 50 * floor;
    }

    public override void takeDamage(float d, Color c, bool critical = false, bool canDodge = true){        
        int netDamage = (int)calculateDamageIn(d);
        if (critical)
        {
            if (GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
            {
                SoundManager.sm.PlayCriticalSound();
            }
            c = new Color(1.0f, 0.64f, 0.0f);
        }
        else if (d < 0)
        {
            if (GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
            {
                SoundManager.sm.PlayHitSound();
            }
        }
        base.takeDamage(netDamage, c, critical);
    }

    public void die(){
        GameManager.gmInstance.Log.AddLog($">{entityName} died.");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
        {
            SoundManager.sm.PlayMonsterSounds();
        }
        SpawnBlood();
        GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
        GameManager.gmInstance.removeEnemy(this);
        int givenExp = calculateExp();
        player.addExp(givenExp);

        // Drop gold
        if (heldMoney > 0)
        {
            GameObject item = new GameObject("Gold");

            item.AddComponent<SpriteRenderer>();
            item.GetComponent<SpriteRenderer>().sprite = GameManager.gmInstance.consumableGenerator.gold;
            item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

            item.AddComponent<BoxCollider2D>();
            item.GetComponent<BoxCollider2D>().isTrigger = true;

            Money money = item.AddComponent<Money>();
            money.amount = heldMoney;

            item.tag = "Gold";

            GameObject goldPile = Instantiate(item, new Vector2(col, row), Quaternion.identity);
            goldPile.GetComponent<Money>().SetLocation(row, col);
            GameManager.gmInstance.Dungeon.itemList.Add(new Vector2(row, col));
            
            GameManager.gmInstance.Log.AddLog($">{entityName} drops {heldMoney} gold.");
        }

        // Drop Item
        {

        }

        Destroy(this.gameObject);
    }

    private void StartTurn()
    {
        ApplyStatusEffects("start");
    }

    private void EndTurn()
    {
        ApplyStatusEffects("end");
        UpdateStatusEffectDuration();
    }

    public void MoveEnemy()
    {
        StartTurn();

        if (dead || hp <= 0)
        {
            die();
            return;
        }
        if (agro)
        {
            //ATTACKING
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            checkPlayerMoved(player);
            attackController(player);

            base.AttemptMove<Player>(player.getRow(), player.getCol());

            if (moving)
            {
                if (atTarget)
                {
                    setNextTarget();
                }
            }
        }
        else
        {
            //IDLE
            checkAgro();
            moveRandom();
        }

        EndTurn();
    }

    void checkPlayerMoved(Player player)
    {
        if (player.getRow() != pRow || player.getCol() != pCol)
        {
            pRow = player.getRow();
            pCol = player.getCol();
            moving = false;
        }
    }

    public virtual void attackController(Player player)
    {
        if (canUseSkill())
        {
            Debug.Log("Can Use Skill");
            // Chance to use skill
            int chance = Random.Range(0, 100);
            if (chance < 20)
            {
                
                Skill s = availableSkills[Random.Range(0, availableSkills.Count)];
                
                bool success = false;
                if (s.range == 0)
                {
                    success = s.Activate(this, row, col);
                }
                else
                {
                    success = s.Activate(this, pRow, pCol);
                }

                Debug.Log("Using Skill " + s.skillName + " : " + success);

                availableSkills.Clear();
                return;
            }
            Debug.Log("Failed to use skill");
            availableSkills.Clear();
        }
        if (canAttackTarget())
        {
            attackTarget(player); 
        }
    }

    public bool canAttackTarget()
    {
        if (isAdjacent(enemyTarget)) return true;

        return false;
    }

    public bool canUseSkill()
    {
        int rdif = Mathf.Abs(enemyTarget.getRow() - row);
        int cdif = Mathf.Abs(enemyTarget.getCol() - col);
        bool value = false;
        foreach (Skill s in skills)
        {
            if (rdif + cdif <= s.range || s.range == 0)
            {
                availableSkills.Add(s);
                value = true;
            }
        }

        return value;
    }

    protected void attackTarget(MovingEntity target)
    {
        target.takeDamage(calculateDamageOut(), Color.red);

        // Status Effect Roll
        float roll = Random.Range(0f, 1f);
        if (roll <= poisonChance)
        {
            player.AddStatusEffect(new StatusEffect(EffectType.poison, 5, EffectOrder.End));
        }
        else if (roll <= bleedChance)
        {
            player.AddStatusEffect(new StatusEffect(EffectType.bleed, 5, EffectOrder.End));
        }
        setAttackAnimation(player.getRow(), player.getCol());
    }

    public void setAttackAnimation(int enemyRow, int enemyCol)
    {
        if (enemyRow > row) animator.Play("AttackUp");
        if (enemyRow < row) animator.Play("AttackDown");
        if (enemyCol > col) animator.Play("AttackRight");
        if (enemyCol < col) animator.Play("AttackLeft");
    }

    void checkAgro()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Vector3 fromPosition = transform.position;
        Vector3 toPosition = new Vector2(player.getCol(), player.getRow());
        Vector3 direction = toPosition - fromPosition;

        boxCollider.enabled = false;

        if (player.invisible) return;

        int effectiveAgroRange = agroRange;

        if (player.stealth) effectiveAgroRange -= 1;

        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction, effectiveAgroRange);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag.Equals("Player"))
            {
                agro = true;
                enemyTarget = player;
            }
        }
        boxCollider.enabled = true;
    }

    void moveRandom()
    {
        Vector2 dir;
        float ran = Random.Range(0.0f, 1.0f);
        if (ran < 0.25f)
        {
            dir = Vector2.up;
        }
        else if (ran < 0.5f)
        {
            dir = Vector2.down;
        }
        else if (ran < 0.75f)
        {
            dir = Vector2.left;
        }
        else
        {
            dir = Vector2.right;
        }

        base.AttemptMove<Enemy>(row + (int)dir.y, col + (int)dir.x);
    }

    protected override void OnCantMove<T>(T Component)
    {
        //INTERACT
        //ATTACK PLAYER
    }

    bool isAdjacent(MovingEntity p)
    {
        int rDis = Mathf.Abs(p.getRow() - row);
        int cDis = Mathf.Abs(p.getCol() - col);
        if ((rDis == 1 && cDis == 0) || (rDis == 0 && cDis == 1))
        {
            return true;
        }
        return false;
    }

    private float calculateDamageOut()
    {
        return -attack;
    }

    private float calculateDamageIn(float d){
        return d;
    }

    void addHP(float change)
    {
        hp += (int)change;
    }
}

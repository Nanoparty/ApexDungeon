using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using static StatusEffect;

public class Enemy : MovingEntity
{
    SpriteRenderer sr;
    SpriteRenderer healthBar;

    private int pRow;
    private int pCol;

    Player player;

    private int floor;

    protected MovingEntity enemyTarget;

    protected List<Skill> availableSkills;

    [SerializeField] private float poisonChance = 0.01f;
    [SerializeField] private float bleedChance = 0.05f;

    protected override void Start()
    {
        base.Start();
        InitializeObjects();
        SetInitialValues();
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

    protected void InitializeObjects()
    {
        GameManager.gmInstance.AddEnemyToList(this);

        sr = this.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        healthBar = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        path = new Path();
        availableSkills = new List<Skill>();
        sleepEffectPrefab = Resources.Load<GameObject>("ParticleEffects/SleepEffect");
    }

    protected void SetInitialValues()
    {
        floor = GameManager.gmInstance.level;
        entityName = "Enemy";
        hp = 100;
        agroRange = 5;
        SetStatsByFloor(floor);
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

    private void SetStatsByFloor(int floor){
        int basehp = 50;
        hp = (int)(basehp + basehp * 0.2 * (floor-1));
        maxHp = hp;

        mp = 100 * floor;
        maxMp = mp;

        int basedamage = 15;
        attack = (int)(basedamage + basedamage * 0.2 * (floor-1));
        defense = 5 + 1 * floor;
    }

    int CalculateExp(){
        return 50 + 50 * floor;
    }

    public override void TakeDamage(float d, Color c, bool critical = false, bool canDodge = true){
        int netDamage = (int)d;
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
        base.TakeDamage(netDamage, c, critical);
    }

    public void Die(){
        GameManager.gmInstance.Log.AddLog($">{entityName} died.");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
        {
            SoundManager.sm.PlayMonsterSounds();
        }
        SpawnBlood();
        GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
        GameManager.gmInstance.RemoveEnemy(this);
        int givenExp = CalculateExp();
        player.AddExp(givenExp);

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
        if (heldItem != null) {
            if (typeof(Consumable).IsInstanceOfType(heldItem))
            {
                GameManager.gmInstance.consumableGenerator.CreatePickup((Consumable)heldItem, row, col);

            }
            else if (typeof(Equipment).IsInstanceOfType(heldItem))
            {
                GameManager.gmInstance.equipmentGenerator.CreatePickup((Equipment)heldItem, row, col);
            }
            GameManager.gmInstance.Log.AddLog($">{entityName} drops {heldItem.itemName}.");
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

    public bool MoveEnemy()
    {
        if (!atTarget) return false;

        StartTurn();

        if (dead || hp <= 0)
        {
            Die();
            return false;
        }

        if (skipTurn || sleeping)
        {
            skipTurn = false;
            EndTurn();
            return true;
        }

        if (agro)
        {
            //ATTACKING
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            CheckPlayerMoved(player);
            if (AttackController(player))
            {
                EndTurn();
                return true;
            }

            if (root)
            {
                EndTurn();
                return true;
            }

            base.AttemptMove<Enemy>(player.GetRow(), player.GetCol());

            if (moving)
            {
                if (atTarget)
                {
                    SetNextTarget();
                }
            }
        }
        else
        {
            //IDLE
            CheckAgro();
            MoveRandom();
        }

        EndTurn();
        return true;
    }

    void CheckPlayerMoved(Player player)
    {
        if (player.GetRow() != pRow || player.GetCol() != pCol)
        {
            pRow = player.GetRow();
            pCol = player.GetCol();
            moving = false;
        }
    }

    public virtual bool AttackController(Player player)
    {
        if (CanUseSkill())
        {
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

                availableSkills.Clear();

                if (success && s.range != 0)
                {
                    SetAttackAnimation(player.GetRow(), player.GetCol());
                    return true;
                }
                else
                {
                    availableSkills.Clear();
                    if (CanAttackTarget())
                    {
                        AttackTarget(player);
                        return true;
                    }
                    return false;
                }
            }
            availableSkills.Clear();
        }
        if (CanAttackTarget())
        {
            AttackTarget(player);
            return true;
        }
        return false;
    }

    public bool CanAttackTarget()
    {
        if (IsAdjacent(enemyTarget.GetRow(), enemyTarget.GetCol())) return true;

        return false;
    }

    public bool CanUseSkill()
    {
        int rdif = Mathf.Abs(enemyTarget.GetRow() - row);
        int cdif = Mathf.Abs(enemyTarget.GetCol() - col);
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

    protected void AttackTarget(MovingEntity target)
    {
        target.TakeDamage(-attack, Color.red);

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
        SetAttackAnimation(player.GetRow(), player.GetCol());
    }

    void CheckAgro()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Vector3 fromPosition = transform.position;
        Vector3 toPosition = new Vector2(player.GetCol(), player.GetRow());
        Vector3 direction = toPosition - fromPosition;

        boxCollider.enabled = false;

        if (player.invisible) return;

        Debug.Log("Playe Visible");

        int effectiveAgroRange = agroRange;

        if (player.stealth) effectiveAgroRange -= 1;

        Debug.Log("Effective agro range=" + effectiveAgroRange);

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

    void MoveRandom()
    {
        if (root) return;

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
}

using TMPro;
using UnityEngine;

public class Enemy : MovingEntity
{
    Animator animator;
    SpriteRenderer sr;
    SpriteRenderer healthBar;

    int agroRange;

    private int pRow;
    private int pCol;

    Player player;

    bool agro = false;
    bool pathFound = false;

    private Path path;
    private Pathfinder finder;
    private int floor;

    protected override void Start()
    {
        floor = GameManager.gmInstance.level;

        //Values
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
        
        base.Start();
    }

    protected override void Update()
    {
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

    protected override bool AttemptMove<T>(int r, int c)
    {
        return base.AttemptMove<T>(r, c);
    }

    private void setStatsByFloor(int floor){
        int basehp = 30;
        hp = (int)(basehp * Mathf.Pow(1.5f, floor-1));
        maxHp = hp;

        mp = 100 * floor;
        maxMp = mp;

        int basedamage = 10;
        attack = (int)(basedamage * Mathf.Pow(1.5f, floor-1));
        defense = 5 + 1 * floor;
    }

    int calculateExp(){
        return 50 + 50 * floor;
    }

    public new void takeDamage(float d){
        //Debug.Log("ENEMY SPAWN DMG TEXT");
        SoundManager.sm.PlayHitSound();
        
        int netDamage = (int)calculateDamageIn(d);
        GameObject damageNum = GameObject.Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        damageNum.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{netDamage}";
        base.takeDamage(netDamage);
    }

    public void die(){
        SoundManager.sm.PlayMonsterSounds();
        SpawnBlood();
        GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
        GameManager.gmInstance.removeEnemy(this);
        int givenExp = calculateExp();
        player.addExp(givenExp);
        Destroy(this.gameObject);
    }

    public void MoveEnemy()
    {
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

    void attackController(Player player)
    {
        if (isAdjacent(player))
        {
            player.takeAttack(calculateDamageOut());
            setAttackAnimation(player.getRow(), player.getCol());
            //animator.Play("AttackLeft");
        }
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

        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction, agroRange);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag.Equals("Player"))
            {
                agro = true;
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

        base.AttemptMove<Player>(row + (int)dir.y, col + (int)dir.x);
    }

    protected override void OnCantMove<T>(T Component)
    {
        //INTERACT
        //ATTACK PLAYER
    }

    bool isAdjacent(Player p)
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
        // float damangeBlocked = 1 - 1/(1+d);
        // float netDamage = d - (d * damangeBlocked);
        // return netDamage;
        //Debug.Log("Enemy takes damage:"+d);
        return d;
    }

    void addHP(float change)
    {
        hp += (int)change;
    }
}

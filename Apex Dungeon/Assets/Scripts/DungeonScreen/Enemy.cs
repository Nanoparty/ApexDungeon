using System.Collections;
using System.Collections.Generic;
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

    protected override void Start()
    {
        //Values
        hp = 100;
        maxHp = 100;
        defense = 5;
        agroRange = 7;
        pRow = pCol = -1;

        //Objects
        GameManager.gmInstance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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

    

    

    public void MoveEnemy()
    {
        if (dead)
        {
            GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
            GameManager.gmInstance.removeEnemy(this);
            Destroy(this.gameObject);
            
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
            //base.AttemptMove<Player>(row + 0, col - 1);
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
            player.takeDamage(-1);
        }
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

    float calculateDamage(float damage)
    {
        float netDamage = damage - (defense * 0.5f);
        return netDamage;
    }

    void addHP(float change)
    {
        hp += (int)change;
    }

    

   
}

using TMPro;
using UnityEngine;

public abstract class MovingEntity : MonoBehaviour
{
    protected int maxHp;
    protected int maxMp;
    protected int hp;
    protected int mp;
    protected int expLevel;
    protected int exp;
    protected int maxExp;

    
    protected int attack;
    protected int defense;
    protected int strength;
    protected int intelligence;
    protected int critical;
    protected int evade;
    protected int blockStat;

    protected bool dead;

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
        if (!atTarget)
        {
            moveToTarget();
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
        float increase = (float)(attack * (strength * 0.02));
        float attackDamage = (float)(attack + increase);
        attackDamage *= m;
        return -attackDamage;
    }

    public virtual void takeDamage(float change)
    {
        hp += (int)change;
        moving = false;
        if(hp <= 0)
        {
            dead = true;
            SpawnBlood();
        }
        if (change < 0)
        {
            GameObject damageNum = GameObject.Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity, this.transform);
            damageNum.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{change}";
            SpawnBlood();
        }
    }

    protected void SpawnBlood()
    {
        Vector3 position = new Vector3(col, row, 0f);
        Instantiate(blood, position, Quaternion.identity);
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

    private void UpdateShadows(int r, int c)
    {
        GameManager.gmInstance.Dungeon.UpdateShadows(r, c);
    }

    public void setHP(int i){
        this.hp = i;
    }

    public void setPosition(int r, int c){
        GameManager.gmInstance.Dungeon.tileMap[row,col].occupied = 0;
        gameObject.transform.position = new Vector3(c, r, 0f);
        this.row = r;
        this.col = c;
        GameManager.gmInstance.Dungeon.tileMap[r,c].occupied = 1;
    }


}

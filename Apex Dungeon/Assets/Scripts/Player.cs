using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingEntity
{
    //UI Elements
    public Image hpbar;
    public Image mpbar;
    public Button bag;
    public GameObject panel;
    public GameObject slot;

    private Inventory inventory;

    private int gold;
    //Components
    Animator animator;

    private bool openInventory = false;

    protected override void Start()
    {
        //Values
        defense = 10;
        hp = 100;
        mp = 50;
        maxMp = 50;
        maxHp = 100;
        type = 1;
        damage = 10;
        gold = 0;

        //GameObjects
        animator = GetComponent<Animator>();
        inventory = new Inventory(panel, slot);

        hpbar = GameObject.FindGameObjectWithTag("hpbar").GetComponent<Image>();
        mpbar = GameObject.FindGameObjectWithTag("mpbar").GetComponent<Image>();
        
        bag = GameObject.FindGameObjectWithTag("bag").GetComponent<Button>();
        bag.onClick.AddListener(bagListener);

        base.Start();
    }

    void moveController(int clickRow, int clickCol)
    {
        bool val;
        val = AttemptMove<Player>(clickRow, clickCol);
        GameManager.gmInstance.playersTurn = false;
    }

    protected override void Update()
    {

        if (hp < 0)
            hp = 0;
        if (hp > maxHp)
            hp = maxHp;

        if (mp < 0)
            mp = 0;
        if (mp > maxMp)
            mp = maxMp;

        updateUI();

        if (Input.GetKeyDown("space"))
        {
            if (!openInventory)
            {
                openInventory = true;
                inventory.openInventory();
            }
            else
            {
                openInventory = false;
                inventory.closeInventory();
            }
            //GameObject.FindGameObjectWithTag("DunGen").GetComponent<MapGenerator>().Reset();


        }

        if (openInventory)
        {
            if (inventory.getClosed())
            {
                openInventory = false;
                inventory.setClosed(false);
            }
            inventory.Update();
            return;
        }

        //check dead
        if (dead)
        {
            Destroy(this.gameObject);
        }
        //checkInventory();
        
        base.Update();

        

        checkMoving();
        
        if (!GameManager.gmInstance.playersTurn) return;

        if (Input.GetButtonDown("Fire1"))
        {
            int clickRow = (int)GameManager.gmInstance.mRow;
            int clickCol = (int)GameManager.gmInstance.mCol;
            //check attack

            //
            if (isAdjacent(clickRow, clickCol))
            {
                if(isFurniture(clickRow, clickCol))
                {
                    return;
                }
                else
                {
                    attackController(clickRow, clickCol);
                }
                
            }
                

            moveController(clickRow, clickCol);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Stairs")
        {
            Debug.Log("Stairs");
        }
        if(other.gameObject.tag == "Potion")
        {
            inventory.addItem(other.GetComponent<Item>());
            //takeDamage(10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Gold")
        {
            Destroy(other.gameObject);
            gold += 100;
            inventory.setGold(gold);
        }
        if (other.gameObject.tag == "Silver")
        {
            Destroy(other.gameObject);
            gold += 50;
            inventory.setGold(gold);
        }
        if (other.gameObject.tag == "Copper")
        {
            Destroy(other.gameObject);
            gold += 25;
            inventory.setGold(gold);
        }
    }

    bool isFurniture(int row, int col)
    {
        Furniture f = GameManager.gmInstance.getFurnitureAtLoc(row, col);
        if (f != null)
        {
            f.setDamage(-1);
            GameManager.gmInstance.playersTurn = false;
            return true;
        }
        else return false;
    }

    void bagListener()
    {
        
    }

    

    void checkInventory()
    {
        if (openInventory)
        {
            return;
        }
    }

    void checkMoving()
    {
        if (moving)
        {
            if (atTarget)
            {
                setNextTarget();
                GameManager.gmInstance.playersTurn = false;
            }

            return;
        }
    }

    void attackController(int clickRow, int clickCol)
    {
        
            Enemy enemy = GameManager.gmInstance.getEnemyAtLoc(clickRow, clickCol);
            if (enemy != null)
            {
                enemy.takeDamage(-10);
                GameManager.gmInstance.playersTurn = false;
                return;
            }
        
    }

    bool isAdjacent(int r, int c)
    {
        int rDis = Mathf.Abs((r) - row);
        int cDis = Mathf.Abs(c - col);
        if ((rDis == 1 && cDis == 0) || (rDis == 0 && cDis == 1))
        {
            return true;
        }
        return false;
    }

    protected override bool AttemptMove<T>(int r, int c)
    {
        bool canMove = base.AttemptMove<T>(r,c);
        
        if(canMove)
        {
            return true;
        }
        return false;
    }

    protected override void OnCantMove<T>(T Component)
    {
        //INTERACTION
    }

    public void addMP(int i)
    {
        mp += i;
    }
    

    void updateUI()
    {
        hpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)hp / (float)maxHp * 400f);
        mpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)mp / (float)maxMp * 400f);
    }

    public bool getInventory()
    {
        return openInventory;
    }

    public void setInventory(bool b)
    {
        openInventory = b;
    }
}

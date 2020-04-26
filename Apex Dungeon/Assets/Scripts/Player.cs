using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingEntity
{
    //UI Elements
    public Image hpbar;
    public Image mpbar;
    public GameObject inventory;
    public Button bag;
    

    //Components
    Animator animator;

    public bool openInventory = false;

    protected override void Start()
    {
        //Values
        defense = 10;
        hp = 100;
        mp = 50;
        maxHp = 100;
        type = 1;
        damage = 10;
        

        //GameObjects
        animator = GetComponent<Animator>();
        hpbar = GameObject.FindGameObjectWithTag("hpbar").GetComponent<Image>();
        mpbar = GameObject.FindGameObjectWithTag("mpbar").GetComponent<Image>();
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.SetActive(false);
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
        //check dead
        if (dead)
        {
            Destroy(this.gameObject);
        }
        //checkInventory();
        
        base.Update();

        updateUI();

        checkMoving();
        
        if (!GameManager.gmInstance.playersTurn) return;

        if (Input.GetButtonDown("Fire1"))
        {
            int clickRow = (int)GameManager.gmInstance.mRow;
            int clickCol = (int)GameManager.gmInstance.mCol;
            //check attack
            attackController(clickRow, clickCol);

            moveController(clickRow, clickCol);
        }
    }

    void bagListener()
    {
        openInventory = !openInventory;
        inventory.SetActive(openInventory);
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
        if (isAdjacent(clickRow, clickCol))
        {
            Enemy enemy = GameManager.gmInstance.getEnemyAtLoc(clickRow, clickCol);
            if (enemy != null)
            {
                enemy.takeDamage(-10);
                GameManager.gmInstance.playersTurn = false;
                return;
            }
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

    void updateUI()
    {
        hpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)hp / (float)maxHp * 400f);
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

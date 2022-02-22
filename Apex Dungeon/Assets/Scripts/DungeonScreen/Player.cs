using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingEntity
{
    //UI Elements
    public Image hpbar;
    public Image mpbar;
    public Button bag;
    public Button map;
    public Button character;
    public GameObject panel;
    public GameObject characterPanel;
    public GameObject slot;
    public GameObject block;
    public GameObject pblock;
    public GameObject mapHolder;
    public GameObject OpeningScreen;

    private Inventory inventory;
    private MiniMap mini;
    private Character characterStats;


    private int gold;
    //Components
    Animator animator;

    private bool openInventory = false;
    private bool openMap = false;
    private bool openCharacter = false;

    private bool opening = true;
    private bool fadeIn = false;

    private Time time;
    private float start;

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
        start = 0;

        
     

        //GameObjects
        animator = GetComponent<Animator>();
        
        inventory = new Inventory(panel, slot);
        characterStats = new Character(characterPanel);

        if(Data.i != null)
        {
            inventory = Data.i;
        }

        if (GameManager.gmInstance.level > 1)
        {
            hp = Data.hp;
            mp = Data.mp;
            gold = Data.gold;
        }

        mini = new MiniMap(mapHolder, block, pblock);

        hpbar = GameObject.FindGameObjectWithTag("hpbar").GetComponent<Image>();
        mpbar = GameObject.FindGameObjectWithTag("mpbar").GetComponent<Image>();
        OpeningScreen = GameObject.FindGameObjectWithTag("Opening");
        
        
        bag = GameObject.FindGameObjectWithTag("bag").GetComponent<Button>();
        map = GameObject.FindGameObjectWithTag("mapButton").GetComponent<Button>();
        character = GameObject.FindGameObjectWithTag("characterButton").GetComponent<Button>();

        bag.onClick.AddListener(bagListener);
        map.onClick.AddListener(mapListener);
        character.onClick.AddListener(characterListener);

        base.Start();
    }

    void moveController(int clickRow, int clickCol)
    {
        bool val;
        val = AttemptMove<Player>(clickRow, clickCol);
        GameManager.gmInstance.playersTurn = false;
    }

    void checkDead()
    {
        if (dead)
        {
            Destroy(GameObject.FindGameObjectWithTag("GameManager"));
            Debug.Log("Dead");
            Data.i = inventory;
            Data.hp = hp;
            Data.mp = mp;
            Data.gold = gold;
            SceneManager.LoadScene("Town");
        }
    }

    void setFadeIn()
    {

    }

    protected override void Update()
    {
        if (!GameManager.gmInstance.playersTurn) return;


        updateUI();

        MapGenerator.UpdateShadows(row, col);

        if (openInventory)
        {
            inventory.Update();
            if (inventory.getClosed())
            {
                inventory.setClosed(false);
                openInventory = false;
            }
            return;
        }

        if (openMap)
        {
            //mini.Update();
            if (mini.getClosed())
            {
                mini.setClosed(false);
                openMap = false;
            }
            return;
        }

        if (openCharacter)
        {
            if (characterStats.getClosed())
            {
                characterStats.setClosed(false);
                openCharacter = false;
            }
            return;
        }

        //reset code
        //GameObject.FindGameObjectWithTag("DunGen").GetComponent<MapGenerator>().Reset();


        if (Input.GetKeyDown("t"))
        {
            mini.buildMiniMap();
        }

        checkDead();
        
        base.Update();

        checkMoving();
        
        

        if (Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
                return;
            }

            int clickRow = (int)GameManager.gmInstance.mRow;
            int clickCol = (int)GameManager.gmInstance.mCol;

            //attack or interact
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

            //move
            moveController(clickRow, clickCol);
        }
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Stairs")
        {
            Debug.Log("Stairs");
            GameManager.gmInstance.level++;
            Data.i = inventory;
            Data.hp = hp;
            Data.mp = mp;
            Data.gold = gold;
            GameObject.FindGameObjectWithTag("DunGen").GetComponent<MapGenerator>().Reset();
        }
        if(other.gameObject.tag == "Potion")
        {
            inventory.addItem(other.GetComponent<Item>());
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
        if (other.gameObject.tag == "Equipment")
        {
            inventory.addItem(other.GetComponent<Item>());
            Debug.Log("ADD TO INVENTORY:" + other.gameObject.GetComponent<Equipment>().itemName);
            Destroy(other.gameObject);
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
        if (!openInventory && !openMap && !openCharacter)
        {
            inventory.openInventory();
            openInventory = true;
        }
    }

    void mapListener()
    {
        if (!openMap && !openInventory && !openCharacter)
        {
            mini.buildMiniMap();
            openMap = true;
        }
    }

    void characterListener()
    {
        if (!openMap && !openInventory && !openCharacter)
        {
            characterStats.openStats();
            openCharacter = true;
        }
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
        if (hp < 0)
            hp = 0;
        if (hp > maxHp)
            hp = maxHp;

        if (mp < 0)
            mp = 0;
        if (mp > maxMp)
            mp = maxMp;

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

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance = null;
    public DungeonObject Dungeon;
    public DungeonGenerator DunGen;
    public bool playersTurn = false;
    
    private List<Enemy> enemies;
    private List<Furniture> furniture;
    private bool enemiesTurn;
    public bool doingSetup = true;

    public GameObject tileCursor;
    GameObject cursor;
    public float mRow;
    public float mCol;

    public string DungeonName = "Misty Dungeon";
    public int floor = 72;
    public int level;

    private void Awake()
    {
        level = 1;
        if(gmInstance == null)
        {
            gmInstance = this;
        }else if(gmInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        furniture = new List<Furniture>();
        Setup();

        Vector3 position = new Vector3(0f, 0f, 0f);
        cursor = Instantiate(tileCursor, position, Quaternion.identity) as GameObject;
    }

    void Setup()
    {
        if(SceneManager.GetActiveScene().name == "test") {
            Dungeon = DunGen.Initalize();
        }
    }

    public void Reset(){
        Dungeon = DunGen.Reset();
    }

    void Update()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        mRow = Mathf.Round(pz.y);
        mCol = Mathf.Round(pz.x);

        //updating grid cursor image
        cursor.transform.position = new Vector3(mCol, mRow, 0f);

        if (playersTurn || enemiesTurn || doingSetup)
        {
            return;
        }

        if (!enemiesTurn)
        {
            enemiesTurn = true;
            MoveEnemies();
        }
    }

    public void removeEnemy(Enemy e)
    {
        enemies.Remove(e);
    }

    public void clearEnemies()
    {
        enemies.Clear();
    }

    public void clearFurniture()
    {
        furniture.Clear();
    }

    public void removeFurniture(Furniture e)
    {
        furniture.Remove(e);
    }

    public Enemy getEnemyAtLoc(int r, int c)
    {
        if (enemies.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            int er = enemies[i].getRow();
            int ec = enemies[i].getCol();
            if(r == er && c == ec)
            {
                return enemies[i];
            }
            
        }
        return null;
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    public void AddFurnitureToList(Furniture f)
    {
        furniture.Add(f);
    }

    public Furniture getFurnitureAtLoc(int r, int c)
    {
        if (furniture.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < furniture.Count; i++)
        {
            int er = furniture[i].getRow();
            int ec = furniture[i].getCol();
            if (r == er && c == ec)
            {
                return furniture[i];
            }

        }
        return null;
    }

    void MoveEnemies()
    {
        if(enemies.Count == 0)
        {
            return;
        }
        for(int i = 0;i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            //WAIT OPTIONAL
        }
        playersTurn = true;
        enemiesTurn = false;        
    }
}

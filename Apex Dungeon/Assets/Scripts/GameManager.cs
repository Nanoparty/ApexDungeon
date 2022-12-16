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
    private List<Chest> chests;
    private List<Trap> traps;

    private bool enemiesTurn;
    public bool doingSetup = true;

    public GameObject tileCursor;
    GameObject cursor;
    public float mRow;
    public float mCol;

    public string DungeonName = "Misty Dungeon";
    public int level;
    public int score;
    public List<(string, int)> scores;
    public string playerName;
    public string state = "play";

    private int cursorRow, cursorCol;

    private void Awake()
    {
        if(gmInstance == null)
        {
            gmInstance = this;
        }else if(gmInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        level = Data.floor;
        score = 0;
        scores = new List<(string, int)>();
        playerName = Data.playerName;
        
        enemies = new List<Enemy>();
        furniture = new List<Furniture>();
        chests = new List<Chest>();
        traps = new List<Trap>();
        Setup();

        Vector3 position = new Vector3(0f, 0f, 0f);
        cursor = Instantiate(tileCursor, position, Quaternion.identity) as GameObject;
    }

    void Setup()
    {
        if(SceneManager.GetActiveScene().name == "Dungeon") {
            Dungeon = null;
            Dungeon = DunGen.Initalize();
        }
    }

    public void Reset(){
        Dungeon = DunGen.Reset();
    }

    public void FullReset(){
        Data.reset();
        level = 1;
        score = 0;
        enemies.Clear();
        furniture.Clear();
        chests.Clear();
        traps.Clear();
    }

    void Update()
    {
        //Debug.Log("ORIENTATION:" + Input.deviceOrientation);
        //if (Input.deviceOrientation == DeviceOrientation.Portrait
        //    || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        //{
        //    Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>().assetsPPU = 48;
        //    //Debug.Log("Portrait");
        //}
        //else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft
        //    || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        //{
        //    Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>().assetsPPU = 32;
        //    //Debug.Log("Landscape");
        //}
            

        if (state == "score"){

            return;
        }
        if(state == "menu"){
            
            return;
        }
        if(state == "play" && cursor == null){
            level = 1;
            score = 0;
            enemies.Clear();
            furniture.Clear();
            chests.Clear();
            traps.Clear();
            Vector3 position = new Vector3(0f, 0f, 0f);
            cursor = Instantiate(tileCursor, position, Quaternion.identity) as GameObject;
            Dungeon = DunGen.Initalize();
        }
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        mRow = Mathf.Round(pz.y);
        mCol = Mathf.Round(pz.x);

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //updating grid cursor image
        if (Input.GetButtonDown("Fire1"))
        {
            cursor.SetActive(true);
            cursor.transform.position = new Vector3(mCol, mRow, 0f);
            cursorRow = (int)mRow;
            cursorCol = (int)mCol;
            if (player.isBlocked((int)mRow, (int)mCol))
            {
                cursor.GetComponent<Cursor>().SetRed();
            }
            else
            {
                cursor.GetComponent<Cursor>().SetGreen();
            }
        }
        
        if (player.getRow() == cursorRow && player.getCol() == cursorCol)
        {
            cursor.SetActive(false);
        }


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

    public void removeChest(Chest e)
    {
        chests.Remove(e);
    }

    public void removeTrap(Trap t)
    {
        traps.Remove(t);
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

    public Trap GetTrapAtLoc(int r, int c)
    {
        if (traps.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < traps.Count; i++)
        {
            int tr = traps[i].GetRow();
            int tc = traps[i].GetCol();
            if (r == tr && c == tc)
            {
                return traps[i];
            }

        }
        return null;
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    public void AddTrapToList(Trap trap)
    {
        traps.Add(trap);
    }

    public void AddFurnitureToList(Furniture f)
    {
        furniture.Add(f);
    }

    public void AddChestToList(Chest c)
    {
        chests.Add(c);
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

    public Chest getChestAtLoc(int r, int c)
    {
        if (chests.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < chests.Count; i++)
        {
            int er = chests[i].getRow();
            int ec = chests[i].getCol();
            if (r == er && c == ec)
            {
                return chests[i];
            }

        }
        return null;
    }

    void MoveEnemies()
    {
        if(enemies.Count == 0)
        {
            playersTurn = true;
            enemiesTurn = false;
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

    public void UpdateCursor(string state, int row, int col)
    {
        //if (state == "Furniture")
        //{
        //    //look for nearest open square
        //    cursor.SetActive(true);
        //    cursor.transform.position = new Vector3(col, row, 0f);
        //    cursor.GetComponent<Cursor>().SetRed();
        //}
        //if (state == "Attack")
        //{
        //    cursor.SetActive(true);
        //    cursor.transform.position = new Vector3(col, row, 0f);
        //    cursor.GetComponent<Cursor>().SetRed();
        //    //cursorRow = (int)mRow;
        //    //cursorCol = (int)mCol;
        //}
        //if (state == "Player")
        //{
        //    cursor.SetActive(true);
        //    cursor.transform.position = new Vector3(col, row, 0f);
        //    cursor.GetComponent<Cursor>().SetGreen();
        //}
        //if (state == "Blocked")
        //{
        //    cursor.SetActive(true);
        //    cursor.transform.position = new Vector3(col, row, 0f);
        //    cursor.GetComponent<Cursor>().SetRed();
        //}
    }

    public void UpdateCursor(string state)
    {
        //if (state == "Interrupt")
        //{
        //    cursor.GetComponent<Cursor>().SetRed();
        //}
    }
}

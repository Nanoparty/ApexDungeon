using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    ItemGenerator itemGen;

    public static Tile[,] tileMap;
    public static GameObject[,] shadowMap;
    public static List<GameObject> activeShadows;
    public static List<Vector2> visibleTiles;

    public static int width = 50;
    public static int height = 50;

    public int maxWidth = 10;
    public int minWidth = 5;
    public int maxHeight = 10;
    public int minHeight = 5;
    public int numRooms = 30;

    public static List<Room> rooms;

    private Transform dungeon;

    public GameObject Opening;

    public GameObject Shadow;

    public GameObject Player;
    public GameObject Stairs;
    public GameObject[] Enemy;
    public GameObject[] Items;

    public GameObject[] floor;
    public GameObject[] wallH;
    public GameObject[] wallV;
    public GameObject[] Furniture;


    // Start is called before the first frame update
    void Start()
    {
        itemGen = this.GetComponent<ItemGenerator>();

        maxWidth = 15;
        minWidth = 8;
        maxHeight = 15;
        minHeight = 8;
        numRooms = 10;

        rooms = new List<Room>();

        activeShadows = new List<GameObject>();
        visibleTiles = new List<Vector2>();

        dungeon = new GameObject("Dungeon").transform;

        GameObject op = GameObject.Instantiate(Opening, new Vector3(0, 0, 0), Quaternion.identity);
        op.transform.GetChild(0).gameObject.GetComponent<Text>().text = GameManager.gmInstance.DungeonName;
        op.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Floor " + GameManager.gmInstance.level;
        op.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        InitializeTileMap();
        InitializeShadowMap();

        GenerateRooms();

        ConvertRoomsToTileMap();

        if (rooms != null || rooms.Count > 0)
        {
            for (int i = 1; i < rooms.Count; i++)
            {
                connectRooms(i - 1, i);
            }
        }
       
        //GenerateMap();
        SpawnMap();
        SpawnPlayer();
        SpawnStairs();
        SpawnEnemies();
        SpawnFurniture();
        SpawnItems();

        InstantiateShadowMap();
    }

    public void Reset()
    {
        Debug.Log("CLEARING");
        Destroy(dungeon.gameObject);
        GameManager.gmInstance.clearEnemies();
        GameManager.gmInstance.clearFurniture();
        Debug.Log("RESET");
        Start();
    }

    void InitializeTileMap()
    {
        tileMap = new Tile[height, width];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                tileMap[j, i] = new Tile(j, i, 0);
            }
        }
    }

    void InitializeShadowMap()
    {
        shadowMap = new GameObject[height, width];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                shadowMap[j, i] = new GameObject();
            }
        }
    }

    int CreateVerticleHallway(int startR, int endR, int col)
    {
        
        if (startR < endR)
        {
            //end cap
            if (tileMap[startR - 1, col - 1].type == 0)
            {
                tileMap[startR - 1, col - 1] = new Tile(startR - 1, col - 1, 2);
            }
            if (tileMap[startR - 1, col + 1].type == 0)
            {
                tileMap[startR - 1, col + 1] = new Tile(startR - 1, col + 1, 2);
            }
            if (tileMap[startR - 1, col].type == 0)
            {
                tileMap[startR - 1, col] = new Tile(startR - 1, col, 2);
            }

            while (startR < endR)
            {
                //place middle floor;
                if (tileMap[startR, col].type == 0 || tileMap[startR, col].type == 2)
                {
                    tileMap[startR, col] = new Tile(startR, col, 3);
                }
                //place side walls left;
                if (tileMap[startR, col - 1].type == 0)
                {
                    tileMap[startR, col - 1] = new Tile(startR, col - 1, 2);
                }
                //place side walls right;
                if (tileMap[startR, col + 1].type == 0)
                {
                    tileMap[startR, col + 1] = new Tile(startR, col + 1, 2);
                }
                startR++;
            }
        }
        if(startR > endR)
        {
            //end cap
            if (tileMap[startR + 1, col - 1].type == 0)
            {
                tileMap[startR + 1, col - 1] = new Tile(startR + 1, col - 1, 2);
            }
            if (tileMap[startR + 1, col + 1].type == 0)
            {
                tileMap[startR + 1, col + 1] = new Tile(startR + 1, col + 1, 2);
            }
            if (tileMap[startR + 1, col].type == 0)
            {
                tileMap[startR + 1, col] = new Tile(startR + 1, col, 2);
            }

            while (startR > endR)
            {
                //place middle floor;
                if (tileMap[startR, col].type == 0 || tileMap[startR, col].type == 2)
                {
                    tileMap[startR, col] = new Tile(startR, col, 3);
                }
                //place side walls left;
                if (tileMap[startR, col - 1].type == 0)
                {
                    tileMap[startR, col - 1] = new Tile(startR, col - 1, 2);
                }
                //place side walls right;
                if (tileMap[startR, col + 1].type == 0)
                {
                    tileMap[startR, col + 1] = new Tile(startR, col + 1, 2);
                }
                startR--;
            }
        }
        return startR;
    }

    int CreateHorizontalHallway(int startC, int endC, int row)
    {
        if(startC < endC)
        {
            //end cap
            if (tileMap[row + 1, startC - 1].type == 0)
            {
                tileMap[row + 1, startC - 1] = new Tile(row + 1, startC - 1, 2);
            }
            if (tileMap[row - 1, startC - 1].type == 0)
            {
                tileMap[row - 1, startC - 1] = new Tile(row - 1, startC - 1, 2);
            }
            if (tileMap[row, startC - 1].type == 0)
            {
                tileMap[row, startC - 1] = new Tile(row, startC - 1, 2);
            }

            while (startC < endC)
            {
                //place middle floor;
                if (tileMap[row, startC].type == 0 || tileMap[row, startC].type == 2)
                {
                    tileMap[row, startC] = new Tile(row, startC, 3);
                }
                //place side walls left;
                if (tileMap[row - 1, startC].type == 0)
                {
                    tileMap[row - 1, startC] = new Tile(row - 1, startC, 2);
                }
                //place side walls right;
                if (tileMap[row + 1, startC].type == 0)
                {
                    tileMap[row + 1, startC] = new Tile(row + 1, startC, 2);
                }
                startC++;
            }
        }
        if(startC > endC)
        {
            //end cap
            if (tileMap[row + 1, startC + 1].type == 0)
            {
                tileMap[row + 1, startC + 1] = new Tile(row + 1, startC + 1, 2);
            }
            if (tileMap[row - 1, startC + 1].type == 0)
            {
                tileMap[row - 1, startC + 1] = new Tile(row - 1, startC + 1, 2);
            }
            if (tileMap[row, startC + 1].type == 0)
            {
                tileMap[row, startC + 1] = new Tile(row, startC + 1, 2);
            }

            while (startC > endC)
            {
                //place middle floor;
                if (tileMap[row, startC].type == 0 || tileMap[row, startC].type == 2)
                {
                    tileMap[row, startC] = new Tile(row, startC, 3);
                }
                //place side walls left;
                if (tileMap[row - 1, startC].type == 0)
                {
                    tileMap[row - 1, startC] = new Tile(row - 1, startC, 2);
                }
                //place side walls right;
                if (tileMap[row + 1, startC].type == 0)
                {
                    tileMap[row + 1, startC] = new Tile(row + 1, startC, 2);
                }
                startC--;
            }
        }
        return startC;
    }

    void connectRooms(int i, int j)
    {
        Room r1 = rooms[i];
        Room r2 = rooms[j];


        int startR = r1.getCenterRow();
        int startC = r1.getCenterCol();


        int endR = r2.getCenterRow();
        int endC = r2.getCenterCol();

        startR = CreateVerticleHallway(startR, endR, startC);

        CreateHorizontalHallway(startC, endC, startR);


    }

    void GenerateRooms()
    {
        for (int i = 0; i < numRooms; i++)
        {
            bool valid = false;
            int rWidth = 0;
            int rHeight = 0;
            int row = 0;
            int col = 0;
            int tries = 0;
            while (!valid && tries < 50)
            {
                tries++;
                rWidth = Random.Range(minWidth, maxWidth);
                rHeight = Random.Range(minHeight, maxHeight);
                row = Random.Range(0, height - rHeight);
                col = Random.Range(0, width - rWidth);
                if(!checkRoomCollision(row, col, rWidth, rHeight))
                {
                    valid = true;
                }
            }
            if (valid)
            {
                rooms.Add(new Room(row, col, rWidth, rHeight));
            }
            
            
        }
    }

    void ConvertRoomsToTileMap()
    {
        for(int i = 0;i < rooms.Count; i++)
        {
            Room room = rooms[i];
            int r = room.row;
            int c = room.col;
            int w = room.width;
            int h = room.height;
            //Debug.Log(r + " " + " " + c + " " + w + " " + h);
            for(int j = 0; j < h; j++)
            {
                for(int k = 0; k < w; k++)
                {
                    tileMap[r + j, c + k] = new Tile(r + j, c + k, 2);
                    
                }
            }
            for (int j = 1; j < h-1; j++)
            {
                for (int k = 1; k < w-1; k++)
                {
                    tileMap[r + j, c + k] = new Tile(r + j, c + k, 1);

                }
            }
        }
    }

    private void InstantiateShadowMap()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float row = dungeon.position.y + i;
                float col = dungeon.position.x + j;

                shadowMap[i,j] = InstantiateSingle(Shadow, row, col);
            }
        }
    }

    public static void UpdateShadows(int r, int c)
    {
        SetShadowsDark();
        //hallway
        bool hallway = tileMap[r, c].type == 3;
        if (hallway)
        {
            //only update top, bottom, left, right
            tileMap[r, c].visible = true;
            tileMap[r + 1, c].visible = true;
            tileMap[r - 1, c].visible = true;
            tileMap[r, c + 1].visible = true;
            tileMap[r, c - 1].visible = true;

            SetShadowVisible(r, c);
            SetShadowVisible(r + 1, c);
            SetShadowVisible(r - 1, c);
            SetShadowVisible(r, c + 1);
            SetShadowVisible(r, c - 1);
        }
        else
        {
            Room curRoom = getRoom(r, c);
            if (curRoom == null) return;

            int startRow = curRoom.row;
            int startCol = curRoom.col;
            int rWidth = curRoom.width;
            int rHeight = curRoom.height;

            //Debug.Log(startRow + " " + startCol + " " + rWidth + " " + rHeight);

            SetShadowVisible(r, c);

            for(int i = startCol; i < startCol + rWidth; i++)
            {
                for(int j = startRow; j < startRow + rHeight; j++)
                {
                    SetShadowVisible(j, i);
                    //Debug.Log("Set Visible " + j + " " + i);
                }
            }
        }
    }

    static void SetShadowVisible(int r, int c)
    {
        GameObject o = shadowMap[r, c];
        o.SetActive(false);
        activeShadows.Add(o);
        tileMap[r, c].visible = true;
        visibleTiles.Add(new Vector2(r, c));
        tileMap[r, c].explored = true;
    }

    static void SetShadowsDark()
    {
        foreach(GameObject o in activeShadows)
        {
            o.SetActive(true);
            
        }
        activeShadows.Clear();
        foreach(Vector2 v in visibleTiles)
        {
            tileMap[(int)v.x, (int)v.y].visible = false;
        }
        visibleTiles.Clear();
    }

    Vector2 findValidLocation(int w, int h)
    {
        int tries = 0;
        int row = -1;
        int col = -1;
        while(tries < 10)
        {
            tries++;
            row = Random.Range(0, height - w - 1);
            col = Random.Range(0, width - h - 1);
            
            
                
            
        }
        return new Vector2(row, col);
    }

    public static bool isPlayer(int r, int c)
    {
        
        if(tileMap[r,c].occupied == 1)
        {
            return true;
        }
        return false;
    }

    public static bool isEnemy(int r, int c)
    {

        if (tileMap[r, c].occupied == 2)
        {
            return true;
        }
        return false;
    }

    bool checkRoomCollision(int r, int c, int w, int h)
    {
        bool collide = false;
        
        for(int i = 0; i < rooms.Count; i++)
        {
            Room room = rooms[i];
            Rect r1 = new Rect(c, r, w, h);
            Rect r2 = new Rect(room.col, room.row, room.width, room.height);

            if(r1.Overlaps(r2) || r2.Overlaps(r1))
            {
                collide = true;
                break;
            }
            
        }
        return collide;
    }

    void GenerateMap()
    {
        

        for(int i =0; i< height; i++)
        {
            for(int j = 0;j< width; j++)
            {
                
                tileMap[i, j] = new Tile(i , j,1);
            }
        }

        for(int i = 0; i < width; i++)
        {
            tileMap[0,i] = new Tile(0,i,2);
            tileMap[i, 0] = new Tile(i, 0, 2);

            tileMap[height-1, i] = new Tile(height-1, i, 2);
            tileMap[i, height-1] = new Tile(i, height-1, 2);

            //tileMap[4, i+2] = new Tile(4, i+2, 2);
        }

        for(int i = 0;i < 5; i++)
        {
            tileMap[4, i] = new Tile(4, i, 2);
        }
    }

    void SpawnMap()
    {
        //Debug.Log(tileMap[0, 0].type);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float row = dungeon.position.y + i;
                float col = dungeon.position.x + j;
                int type = tileMap[(int)row, (int)col].type;
                
                switch (type)
                {
                    case 1:
                        InstantiateRandom(floor, row, col);
                        break;
                    case 2:
                        InstantiateSingle(getWall((int)row,(int)col), row, col); 
                        break;
                    case 3:
                        InstantiateRandom(floor, row, col);
                        break;
                }
                    


            }
        }
    }

    void SpawnPlayer()
    {
        int r = Random.Range(0, rooms.Count - 1);
        Room room = rooms[r];

        int row = Random.Range(room.row + 1, room.row + room.height - 2);
        int col = Random.Range(room.col + 1, room.col + room.width - 2);

        InstantiateSingle(Player, row, col);
        tileMap[row, col].occupied = 1; 
    }

    void SpawnEnemies()
    {
        for(int i = 0;i < rooms.Count; i++)
        {
            Room room = rooms[i];
            bool valid = false;
            int tries = 0;
            int row = 0;
            int col = 0;
            while(!valid && tries < 10)
            {
                tries++;
                row = Random.Range(room.row + 1, room.row + room.height - 2);
                col = Random.Range(room.col + 1, room.col + room.width - 2);
                if (!tileMap[row, col].getOccupied())
                {
                    valid = true;
                }
            }
            if (valid)
            {
                InstantiateRandom(Enemy, row, col);
                tileMap[row, col].occupied = 2;
            }
            
        }
        
    }

    void SpawnFurniture()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Room room = rooms[i];
                bool valid = false;
                int tries = 0;
                int row = 0;
                int col = 0;
                while (!valid && tries < 10)
                {
                    tries++;
                    row = Random.Range(room.row + 1, room.row + room.height - 2);
                    col = Random.Range(room.col + 1, room.col + room.width - 2);
                    if (!tileMap[row, col].getOccupied() && !tileMap[row,col].stairs)
                    {
                        valid = true;
                    }
                }
                if (valid)
                {
                    InstantiateRandom(Furniture, row, col);
                    tileMap[row, col].occupied = 3;

                }
            }
        }
    }

    void SpawnStairs()
    {
        int r = Random.Range(0, rooms.Count - 1);
        Room room = rooms[r];
        int row = 0;
        int col = 0;
        bool valid = false;
        int tries = 0;
        while (!valid && tries < 10)
        {
            
            tries++;
            row = Random.Range(room.row + 1, room.row + room.height - 2);
            col = Random.Range(room.col + 1, room.col + room.width - 2);
            if (!tileMap[row, col].getOccupied())
            {
                valid = true;
            }
        }
        if (valid)
        {
            InstantiateSingle(Stairs, row, col);
            tileMap[row, col].stairs = true;
        }
        else
        {
            Debug.Log("STAIRS FAILED");
        }
        
    }

    void SpawnItems()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Room room = rooms[i];
                bool valid = false;
                int tries = 0;
                int row = 0;
                int col = 0;
                while (!valid && tries < 10)
                {
                    tries++;
                    row = Random.Range(room.row + 1, room.row + room.height - 2);
                    col = Random.Range(room.col + 1, room.col + room.width - 2);
                    if (!tileMap[row, col].getOccupied() && !tileMap[row, col].stairs)
                    {
                        valid = true;
                    }
                }
                if (valid)
                {
                    float coin = Random.Range(0f, 1f);
                    //Debug.Log(coin);
                    if(coin > 0.5)
                    {
                        GameObject item = itemGen.GenerateItem(1,"chestplate", 3);
                        Debug.Log("CREATING:" + item.GetComponent<Equipment>().getName());
                        InstantiateSingle(item, row, col);
                    }
                    else
                    {
                        InstantiateRandom(Items, row, col);
                    }
                    //tileMap[row, col].occupied = 3;
                }
            }
        }
    }

    GameObject InstantiateSingle(GameObject prefab, float row, float col)
    {
        Vector3 position = new Vector3(col, row, 0f);
        GameObject tileInstance = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = dungeon.transform;
        return tileInstance;
    }

    void InstantiateRandom(GameObject[] tileArray, float row, float col)
    {
        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
        InstantiateSingle(tileChoice, row, col);
    }

    GameObject getRandom(GameObject[] tileArray, float row, float col)
    {
        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
        return tileChoice;
    }

    static Room getRoom(int r, int c)
    {
        foreach(Room room in rooms)
        {
            if (room.containsPos(r, c))
                return room;
        }
        return null;
    }

    GameObject getWall(int r, int c)
    {
       
        bool bottom = false;

        if (r > 0)
        {
            if(tileMap[r-1, c].type == 2)
            {
                bottom = true;
            }
        }
            
        /*
        if (c < width-1)
            top = tileMap[r, c + 1].type == 2;
        if(c > 0)
            bottom = tileMap[r, c - 1].type == 2;
        if(r > 0)
            left = tileMap[r - 1, c].type == 2;
        if(r < height-1)
            right = tileMap[r + 1, c].type == 2;
            */

        
        if (bottom)
        {
            return getRandom(wallV,r,c);
        }
        else
        {
            return getRandom(wallH, r, c);
        }
        
    }
}

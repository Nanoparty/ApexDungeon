using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DungeonGenerator", menuName = "ScriptableObjects/Dungeon Generator")]
public class DungeonGenerator : ScriptableObject
{
    public static Tile[,] tileMap;
    private static List<Vector2> walkableTiles;
    public static GameObject[,] shadowMap;
    public static List<GameObject> activeShadows;
    public static List<Vector2> activeShadowCoords;
    public static List<Vector2> visibleTiles;
    public static List<Vector2> itemList;

    public static GameObject waterBackgroundObject;
    public static GameObject cloudBackgroundObject;

    public static int width = 50;
    public static int height = 50;

    public int maxWidth = 10;
    public int minWidth = 5;
    public int maxHeight = 10;
    public int minHeight = 5;
    public int numRooms = 30;

    public int border = 10;

    public static List<Room> rooms;

    private Transform dungeon;
    private Transform shadowContainer;
    private Transform mapContainer;
    public Transform itemContainer;
    private Transform enemyContainer;
    private Transform furnitureContainer;

    public Biome currentBiome;

    public GameObject Opening;
    public GameObject Shadow;
    public GameObject Player;
    public GameObject[] Furniture;
    public GameObject Chest;
    public Biome[] Biomes;
    public GameObject WaterBackground;
    public GameObject CloudBackground;

    public DungeonObject Initalize()
    {
        maxWidth = 15;
        minWidth = 8;
        maxHeight = 15;
        minHeight = 8;
        numRooms = 10;

        rooms = new List<Room>();

        activeShadows = new List<GameObject>();
        activeShadowCoords = new List<Vector2>();
        visibleTiles = new List<Vector2>();
        walkableTiles = new List<Vector2>();
        itemList = new List<Vector2>();

        dungeon = new GameObject("Dungeon").transform;
        shadowContainer = new GameObject("ShadowContainer").transform;
        mapContainer = new GameObject("MapContainer").transform;
        itemContainer = new GameObject("ItemContainer").transform;
        enemyContainer = new GameObject("EnemyContainer").transform;
        furnitureContainer = new GameObject("FurnitureContainer").transform;

        shadowContainer.parent = dungeon.transform;
        mapContainer.parent = dungeon.transform;
        itemContainer.parent = dungeon.transform;
        enemyContainer.parent = dungeon.transform;
        furnitureContainer.parent = dungeon.transform;

        currentBiome = Biomes[Random.Range(0, Biomes.Length)];

        GameObject op = GameObject.Instantiate(Opening, new Vector3(0, 0, 0), Quaternion.identity);
        op.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = currentBiome.biomeName;
        op.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = "Floor " + GameManager.gmInstance.level;
        op.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        width = 50;
        height = 50;

        width += 2 * border;
        height += 2 * border;

        Destroy(waterBackgroundObject);
        Destroy(cloudBackgroundObject);

        if (currentBiome.name == "Water")
        {
            waterBackgroundObject = Instantiate(WaterBackground, new Vector3(35,35,0), Quaternion.identity);
        }
        if (currentBiome.name == "Cloud")
        {
            cloudBackgroundObject = Instantiate(CloudBackground, new Vector3(35,35,0), Quaternion.identity) ;
        }

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
        SpawnDecoration();
        SpawnTraps();
        SpawnFurniture();
        SpawnChests();
        SpawnItems();
        SpawnMoney();
        InstantiateShadowMap();

        DungeonObject dungeonObject = new DungeonObject();
        dungeonObject.activeShadows = activeShadows;
        dungeonObject.activeShadowCoords = activeShadowCoords;
        dungeonObject.shadowMap = shadowMap;
        dungeonObject.tileMap = tileMap;
        dungeonObject.visibleTiles = visibleTiles;
        dungeonObject.walkableTiles = walkableTiles;
        dungeonObject.itemList = itemList;
        dungeonObject.rooms = rooms;
        dungeonObject.width = width;
        dungeonObject.height = height;
        return dungeonObject;
    }

    public DungeonObject Reset()
    {
        Destroy(dungeon.gameObject);
        GameManager.gmInstance.ClearEnemies();
        GameManager.gmInstance.ClearFurniture();
        GameManager.gmInstance.ClearChests();
        GameManager.gmInstance.ClearTraps();
        return Initalize();
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
                    walkableTiles.Add(new Vector2(startR, col));
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
                    walkableTiles.Add(new Vector2(startR, col));
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
                    walkableTiles.Add(new Vector2(row, startC));
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
                    walkableTiles.Add(new Vector2(row, startC));
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
                row = Random.Range(border, height - rHeight - border);
                col = Random.Range(border, width - rWidth - border);
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
                    walkableTiles.Add(new Vector2(r + j, c + k));
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

                shadowMap[i,j] = InstantiateSingle(Shadow, row, col, shadowContainer);
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


            SetShadowVisible(r, c);

            for(int i = startCol; i < startCol + rWidth; i++)
            {
                for(int j = startRow; j < startRow + rHeight; j++)
                {
                    SetShadowVisible(j, i);
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

    void SpawnMap()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float row = dungeon.position.y + i;
                float col = dungeon.position.x + j;
                int type = tileMap[(int)row, (int)col].type;

                if (currentBiome.top_bottom && type == 2)
                {
                    InstantiateSingle(getWallInverted((int)row, (int)col), row, col, mapContainer);
                    continue;
                }
                
                switch (type)
                {
                    case 0:
                        InstantiateRandom(currentBiome.voidTiles, row, col, mapContainer);
                        break;
                    case 1:
                        InstantiateRandom(currentBiome.floorTiles, row, col, mapContainer);
                        break;
                    case 2:
                        InstantiateSingle(getWall((int)row,(int)col), row, col, mapContainer); 
                        break;
                    case 3:
                        InstantiateRandom(currentBiome.floorTiles, row, col, mapContainer);
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

        InstantiateSingle(Player, row, col, dungeon);
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
            int numEnemies;
            int eCoin = Random.Range(0, 100);
            if (eCoin <= 20)
            {
                numEnemies = 3;
            }else if (eCoin <= 50)
            {
                numEnemies = 2;
            }
            else
            {
                numEnemies = 1;
            }
            for (int e = 0; e < numEnemies; e++)
            {
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
                    InstantiateRandom(currentBiome.enemies, row, col, enemyContainer);
                    tileMap[row, col].occupied = 2;
                }
                valid = false;
                tries = 0;
            }
            
            
        }
        
    }

    void SpawnDecoration()
    {
        if (currentBiome.decorations.Length == 0) return;

        for (int i = 0; i < rooms.Count; i++)
        {
            Room room = rooms[i];
            int roomSize = room.width * room.height;
            int numDecorations = (int)(roomSize * currentBiome.decorationPercentage);
            int tries = 0;
            int row = 0;
            int col = 0;
            while (tries < numDecorations)
            {
                tries++;
                row = Random.Range(room.row + 1, room.row + room.height - 2);
                col = Random.Range(room.col + 1, room.col + room.width - 2);
                InstantiateRandom(currentBiome.decorations, row, col, furnitureContainer);
            }
        }
    }

    void SpawnTraps()
    {
        if (currentBiome.traps.Length == 0) return;

        for (int i = 0; i < rooms.Count; i++)
        {
            Room room = rooms[i];
            if (Random.Range(0, 1) > currentBiome.trapChance) continue;

            int row = Random.Range(room.row + 1, room.row + room.height - 2);
            int col = Random.Range(room.col + 1, room.col + room.width - 2);
            GameObject trap = InstantiateRandom(currentBiome.traps, row, col, furnitureContainer);
            trap.GetComponent<Trap>().Setup(row, col, GameManager.gmInstance.level);
        }
    }

    void SpawnFurniture()
    {
        if (currentBiome.obstacles.Length == 0) return;

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
                    InstantiateRandom(currentBiome.obstacles, row, col, furnitureContainer);
                    tileMap[row, col].occupied = 3;

                }
            }
        }
    }

    void SpawnChests()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (Random.Range(0f, 1f) <= 0.3f)
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
                    GameObject chest = InstantiateSingle(Chest, row, col, furnitureContainer);
                    chest.GetComponent<Chest>().SetPosition(row, col);
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
            InstantiateSingle(currentBiome.stairs, row, col, dungeon);
            tileMap[row, col].stairs = true;
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            // Spawn Consumable
            float coin = Random.Range(0f, 1f);
            if (coin <= .3f)
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
                    Vector3 position = new Vector3(col, row, 0f);
                    //GameObject item = GameManager.gmInstance.consumableGenerator.CreateRandomConsumable(GameManager.gmInstance.level);
                    GameObject item = GameManager.gmInstance.consumableGenerator.foodGenerator.CreateRandomFood();
                    item.transform.parent = itemContainer.transform;
                    item.transform.position = position;
                    item.GetComponent<Pickup>().SetLocation(row, col);
                    itemList.Add(new Vector2(row, col));
                    
                }
            }

            // Spawn Equipment
            coin = Random.Range(0f, 1f);
            if (coin <= .3f)
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
                    Vector3 position = new Vector3(col, row, 0f);
                    GameObject item = GameManager.gmInstance.equipmentGenerator.GenerateEquipment(GameManager.gmInstance.level);
                    item.transform.parent = itemContainer.transform;
                    item.transform.position = position;
                    item.GetComponent<Pickup>().SetLocation(row, col);
                    itemList.Add(new Vector2(row, col));
                }
            }
        }
    }

    void SpawnMoney()
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
                    Vector3 position = new Vector3(col, row, 0f);
                    
                    GameObject item = GameManager.gmInstance.consumableGenerator.CreateRandomMoney(GameManager.gmInstance.level);
                    item.transform.parent = itemContainer.transform;
                    item.transform.position = position;
                    item.GetComponent<Money>().SetLocation(row, col);
                    itemList.Add(new Vector2(row, col));
                }
            }
        }
    }

    GameObject InstantiateSingle(GameObject prefab, float row, float col, Transform parent)
    {
        Vector3 position = new Vector3(col, row, 0f);
        GameObject tileInstance = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = parent.transform;
        return tileInstance;
    }

    GameObject InstantiateRandom(GameObject[] tileArray, float row, float col, Transform parent)
    {
        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
        return InstantiateSingle(tileChoice, row, col, parent);
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
       
        bool bottom = true;

        if (r > 0)
        {
            if(tileMap[r-1, c].type == 1 || tileMap[r-1,c].type == 3){
                bottom = false;
            }
        }
            
        if (bottom)
        {
            return getRandom(currentBiome.verticalWallTiles,r,c);
        }
        else
        {
            return getRandom(currentBiome.horizontalWallTiles, r, c);
        }
        
    }

    GameObject getWallInverted(int r, int c)
    {

        bool top = true;

        if (r > 0)
        {
            if (tileMap[r + 1, c].type == 1 || tileMap[r + 1, c].type == 3)
            {
                top = false;
            }
        }

        if (top)
        {
            return getRandom(currentBiome.verticalWallTiles, r, c);
        }
        else
        {
            return getRandom(currentBiome.horizontalWallTiles, r, c);
        }

    }
}

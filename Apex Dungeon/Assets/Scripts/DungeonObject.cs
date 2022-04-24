using System.Collections.Generic;
using UnityEngine;

public class DungeonObject
{
    public Tile[,] tileMap;
    public GameObject[,] shadowMap;
    public List<GameObject> activeShadows;
    public List<Vector2> activeShadowCoords;
    public List<Vector2> visibleTiles;
    public List<Vector2> walkableTiles;
    public List<Room> rooms;

    public int width;
    public int height;
    private bool fullBright = false;
    private bool fullExplored = false;

    public DungeonObject(){
        tileMap = null;
        shadowMap = null;
        activeShadowCoords = null;
        activeShadows = null;
        visibleTiles = null;
        walkableTiles = null;
        rooms = null;
        fullBright = false;
        fullExplored = false;
    }

    
    public void UpdateShadows(int r, int c)
    {
        if(fullExplored){
            Debug.Log("FullExplored");
            for(int i = 0; i < width;i++){
                for(int j = 0; j < height;j++){
                    visibleTiles.Add(new Vector2(i, j));
                    tileMap[i, j].explored = true;
                }
            }
        }

        if(fullBright){
            Debug.Log("Full Bright");
            for(int i = 0; i < width;i++){
                for(int j = 0; j < height;j++){
                    GameObject o = shadowMap[i, j];
                    o.SetActive(false);
                    activeShadows.Add(o);
                    tileMap[i,j].visible = true;
                }
            }
        }


        
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

    public void SetShadowVisible(int r, int c)
    {
        if(!fullBright){
            GameObject o = shadowMap[r, c];
            o.SetActive(false);
            activeShadows.Add(o);
            activeShadowCoords.Add(new Vector2(r,c));
        }
        if(!fullExplored){
            tileMap[r, c].visible = true;
            visibleTiles.Add(new Vector2(r, c));
            tileMap[r, c].explored = true;
        }
        
    }

    public void SetShadowsDark()
    {
        if(!fullBright){
            foreach(GameObject o in activeShadows)
            {
                o.SetActive(true);
                
            }
            activeShadows.Clear();
            activeShadowCoords.Clear();
        }
        if(!fullExplored){
            foreach(Vector2 v in visibleTiles)
            {
                tileMap[(int)v.x, (int)v.y].visible = false;
            }
            visibleTiles.Clear();
        }
        
    }

    public Room getRoom(int r, int c)
    {
        foreach(Room room in rooms)
        {
            if (room.containsPos(r, c))
                return room;
        }
        return null;
    }

    public bool isPlayer(int r, int c)
    {
        if(tileMap[r,c].occupied == 1)
        {
            return true;
        }
        return false;
    }

    public bool isEnemy(int r, int c)
    {
        if (tileMap[r, c].occupied == 2)
        {
            return true;
        }
        return false;
    }

    public List<Vector2> getActiveShadowCoords(){
        return activeShadowCoords;
    }

    public void setFullBright(bool b){
        fullBright = b;
    }
    public void setFullExplored(bool b){
        fullExplored = b;
    }

    public Vector2 getRandomUnoccupiedTile(){
        Vector2 result = new Vector2();
        bool found = false;
        while(!found){
            int index = Random.Range(0,walkableTiles.Count);
            Vector2 pos = walkableTiles[index];
            if(tileMap[(int)pos.x,(int)pos.y].getOccupied() == false) {
                found = true;
                result = pos;
            }
        }
        return result;
    }
}

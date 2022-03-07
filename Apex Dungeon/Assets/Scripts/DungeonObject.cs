using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonObject
{
    public Tile[,] tileMap;
    public GameObject[,] shadowMap;
    public List<GameObject> activeShadows;
    public List<Vector2> visibleTiles;
    public List<Room> rooms;

    public int width;
    public int height;

    
    public void UpdateShadows(int r, int c)
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

    public void SetShadowVisible(int r, int c)
    {
        GameObject o = shadowMap[r, c];
        o.SetActive(false);
        activeShadows.Add(o);
        tileMap[r, c].visible = true;
        visibleTiles.Add(new Vector2(r, c));
        tileMap[r, c].explored = true;
    }

    public void SetShadowsDark()
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{

    public int row;
    public int col;
    public float x;
    public float y;

    /*
    0 = void
    1 = floor
    2 = wall
    3 = hallway
    */
    public int type;

    public List<Item> contents = new List<Item>();

    public int occupied = 0;

    public Sprite img;

    public bool blocked;

    public Tile(int row, int col, int type)
    {
        this.row = row;
        this.col = col;
        this.type = type;
    }

    public bool getOccupied()
    {
        if(occupied == 1 || occupied == 2 || occupied == 3)
        {
            return true;
        }
        return false;
    }

    public bool getWall()
    {
        if(type == 2 || occupied == 2 || occupied == 3)
        {
            return true;
        }
        return false;
    }

    public bool getBlocked()
    {
        if(getWall() || occupied != 0)
        {
            return true;
        }
        return false;
    }

    

    
    
}

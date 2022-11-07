﻿using System.Collections.Generic;
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
    public bool visible;
    public bool explored;
    public List<Item> contents = new List<Item>();
    public int occupied = 0;
    public Sprite img;
    public bool blocked;
    public bool stairs = false;

    public Tile(int row, int col, int type)
    {
        this.row = row;
        this.col = col;
        this.type = type;
        this.visible = false;
        this.explored = false;
    }
    //0 = empty
    //1 = player
    //2 = enemy
    //3 = furniture
    //4 = stairs
    public bool getOccupied()
    {
        if(occupied == 1 || occupied == 2 || occupied == 3)
        //if(occupied != 0)
        {
            return true;
        }
        return false;
    }

    public bool isEmpty()
    {
        return (type == 1 || type == 3) && occupied != 3;
    }

    public bool getStairs()
    {
        return occupied == 4;
    }

    public bool getWall()
    {
        if(type == 2 || occupied == 3)
        {
            return true;
        }
        if(occupied == 2){
            //check if visible
            if (visible) return true;
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

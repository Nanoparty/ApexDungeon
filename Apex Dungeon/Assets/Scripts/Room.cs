﻿public class Room
{
    public int row;
    public int col;
    public int width;
    public int height;
    public int type;

    public Room(int r, int c, int w, int h)
    {
        this.row = r;
        this.col = c;
        this.width = w;
        this.height = h;
    }

    public (int, int) getCenter()
    {
        int cr = (row + (row + height)) / 2;
        int cc = (col + (col + width)) / 2;
        return (cr, cc);
    }

    public int getCenterCol()
    {
        return (col + (col + width)) / 2;
    }

    public int getCenterRow()
    {
        return (row + (row + height)) / 2;
    }

    public bool containsPos(int r, int c)
    {
        if (r >= row && r <= row + height)
        {
            if(c >= col && c <= col + width)
            {
                return true;
            }
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    Queue<Tile> queue = new Queue<Tile>();
    List<Tile> visited = new List<Tile>();
    Dictionary<Tile, Tile> mapping = new Dictionary<Tile, Tile>();

    public Path findPath(Tile start, Tile end)
    {
        Path p = new Path();
        queue.Clear();
        visited.Clear();
        mapping.Clear();

        queue.Enqueue(start);
        visited.Add(start);

        

        if(start.row == end.row && start.col == end.col)
        {
            Debug.Log("Same Position");
            return null;
        }
        bool found = false;

        Tile node;
        while (!found)
        {
            if(queue.Count == 0)
            {
                return null;
            }
            node = queue.Dequeue();
            if(node == end)
            {
                found = true;
            }
            else
            {
                addAdjacent(node);
            }
        }

        node = end;
        Stack<Tile> stack = new Stack<Tile>();
        while(node != start)
        {
            stack.Push(node);
            node = mapping[node];
        }
        //string pathDebug = "Start:"+start.row+" " +start.col;
        while (!(stack.Count == 0))
        {
            Tile temp = stack.Pop();
            p.nodes.Enqueue(new Vector2(temp.col, temp.row));
            
        }


        return p;
    }

    void addAdjacent(Tile t)
    {
        Tile left = null;
        Tile right = null;
        Tile up = null;
        Tile down = null;

        if (t.col > 0)
            left = MapGenerator.tileMap[t.row, t.col - 1];

        if (t.col < MapGenerator.width)
            right = MapGenerator.tileMap[t.row, t.col + 1];

        if(t.row < MapGenerator.height)
            up = MapGenerator.tileMap[t.row+1, t.col];

        if(t.row > 0)
            down = MapGenerator.tileMap[t.row-1, t.col];

        if(!left.getWall() && !visited.Contains(left))
        {
            queue.Enqueue(left);
            visited.Add(left);
            mapping.Add(left, t);
        }
        if(!right.getWall() && !visited.Contains(right))
        {
            queue.Enqueue(right);
            visited.Add(right);
            mapping.Add(right, t);
        }
        if(!up.getWall() && !visited.Contains(up))
        {
            queue.Enqueue(up);
            visited.Add(up);
            mapping.Add(up, t);
        }
        if(!down.getWall() && !visited.Contains(down))
        {
            queue.Enqueue(down);
            visited.Add(down);
            mapping.Add(down, t);
        }
        
        
        
    }
}

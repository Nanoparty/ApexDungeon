using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public Sprite damaged;

    int health = 2;
    int max = 2;
    private SpriteRenderer renderer;
    private int row;
    private int col;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        row = (int)transform.position.y;
        col = (int)transform.position.x;
        GameManager.gmInstance.AddFurnitureToList(this);
    }

    public void setDamage(int d)
    {
        health += d;
        if(health < max)
        {
            renderer.sprite = damaged;
        }
        if(health <= 0)
        {
            MapGenerator.tileMap[row, col].occupied = 0;
            GameManager.gmInstance.removeFurniture(this);
            Destroy(this.gameObject);
        }
    }

    public int getRow()
    {
        return row;
    }
    public int getCol()
    {
        return col;
    }
}

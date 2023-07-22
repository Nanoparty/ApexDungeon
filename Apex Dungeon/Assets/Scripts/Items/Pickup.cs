using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;

    public SpriteRenderer sr;

    public int row = 0;
    public int col = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public Item GetItem(){
        return item;
    }

    public void SetItem(Item i){
        item = i;
    }

    public void SetLocation(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    private void Update()
    {
        if (GameManager.gmInstance.Dungeon == null) return;

        if (!GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
        {
            sr.enabled = false;
        }
        else
        {
            sr.enabled = true;
        }
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite closed;
    public Sprite open;

    public int row, col;

    public ParticleSystem shine;

    private SpriteRenderer sr;
    private GameObject itemContainer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        row = (int)transform.position.y;
        col = (int)transform.position.x;
        //GameManager.gmInstance.AddFurnitureToList(this);
        itemContainer = GameObject.Find("ItemContainer");
    }

    public void OpenChest()
    {

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

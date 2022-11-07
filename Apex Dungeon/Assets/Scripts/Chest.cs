using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite closed;
    public Sprite open;

    private bool isOpen;

    public int row, col;

    public ParticleSystem shine;

    private SpriteRenderer sr;
    private GameObject itemContainer;

    private List<GameObject> loot;
    public EquipmentGenerator equipmentGenerator;

    private void Start()
    {
        loot = new List<GameObject>();
        sr = GetComponent<SpriteRenderer>();
        row = (int)transform.position.y;
        col = (int)transform.position.x;
        GameManager.gmInstance.AddChestToList(this);
        itemContainer = GameObject.Find("ItemContainer");
    }

    public void SetPosition(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void OpenChest()
    {
        if (isOpen) return;

        sr.sprite = open;
        Instantiate(shine, new Vector3(col, row, 0f), Quaternion.identity);
        GenerateLoot();
        (int nRow, int nCol) = DropLocation();
        Vector3 position = new Vector3(nCol, nRow, 0f);
        GameObject itemInstance = Instantiate(loot[0], position, Quaternion.identity) as GameObject;
        itemInstance.transform.parent = itemContainer.transform;
    }

    private void GenerateLoot()
    {
        GameObject itemObject = equipmentGenerator.GenerateEquipment(GameManager.gmInstance.level);
        //Item item = itemObject.GetComponent<Item>();
        loot.Add(itemObject);
    }

    private (int, int) DropLocation()
    {
        int sr = row - 1;
        int sc = col;
        while (!GameManager.gmInstance.Dungeon.tileMap[sr, sc].isEmpty())
        {
            sr -= 1;
        }
        return (sr, sc);
        
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

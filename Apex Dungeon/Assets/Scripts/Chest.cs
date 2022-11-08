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
        SpawnLoot();
    }

    private void SpawnLoot()
    {
        int numItems = Random.Range(1, 5);
        List<Vector2> positions = new List<Vector2>();
        positions = RadiusLocations(numItems);

        foreach (Vector2 pos in positions)
        {
            Vector3 position = new Vector3(pos.y, pos.x, 0f);
            GameObject item = equipmentGenerator.GenerateEquipment(GameManager.gmInstance.level);
            item.transform.parent = itemContainer.transform;
            item.transform.position = position;
        }
    }

    bool inside_circle(Vector2 center, Vector2 tile, int radius)
    {
        float dx = center.x - tile.x,
              dy = center.y - tile.y;
        float distance_squared = dx * dx + dy * dy;
        return distance_squared <= radius * radius;
    }

    private List<Vector2> RadiusLocations(int num)
    {
        List<Vector2> positions = new List<Vector2>();
        int radius = 1;

        while (positions.Count < num)
        {
            for (int r = row - radius; r <= row + radius; r++)
            {
                for (int c = col - radius; c <= col + radius; c++)
                {
                    if (inside_circle(new Vector2(row, col), new Vector2(r, c), radius))
                    {
                        if (GameManager.gmInstance.Dungeon.tileMap[r, c].isEmpty()
                            && !positions.Contains(new Vector2(r, c)))
                        {
                            positions.Add(new Vector2(r, c));
                        }
                    }
                }
            }
            radius++;
        }
        return positions;
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

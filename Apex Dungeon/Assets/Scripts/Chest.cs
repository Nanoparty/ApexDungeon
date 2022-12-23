using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chest : MonoBehaviour
{
    public Sprite closed;
    public Sprite open;

    private bool isOpen;

    public int row, col;

    public ParticleSystem shine;
    public ParticleSystem wood;

    private SpriteRenderer sr;
    private GameObject itemContainer;

    private List<GameObject> loot;
    public EquipmentGenerator equipmentGenerator;
    public ConsumableGenerator consumableGenerator;

    private void Start()
    {
        loot = new List<GameObject>();
        sr = GetComponent<SpriteRenderer>();
        row = (int)transform.position.y;
        col = (int)transform.position.x;
        GameManager.gmInstance.AddChestToList(this);
        itemContainer = GameObject.Find("ItemContainer");
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

    public void SetPosition(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void OpenChest()
    {
        if (isOpen)
        {
            Instantiate(wood, new Vector2(col, row), Quaternion.identity);
            GameManager.gmInstance.removeChest(this);
            GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
            Destroy(gameObject);
            return;
        }

        sr.sprite = open;
        Instantiate(shine, new Vector3(col, row, 0f), Quaternion.identity);
        SpawnLoot();
        isOpen = true;
    }

    private void SpawnLoot()
    {
        int numItems = Random.Range(1, 5);
        List<Vector2> positions = new List<Vector2>();
        positions = RadiusLocations(numItems);

        foreach (Vector2 pos in positions)
        {
            Vector3 position = new Vector3(pos.y, pos.x, 0f);
            GameObject item = new GameObject();
            if (Random.Range(0f,1f) > 0.5)
            {
                item = equipmentGenerator.GenerateEquipment(GameManager.gmInstance.level);
            }
            else
            {
                item = consumableGenerator.CreateRandomConsumable(GameManager.gmInstance.level);
            }
            
            item.transform.parent = itemContainer.transform;
            item.transform.position = position;
            item.GetComponent<Pickup>().SetLocation((int)pos.x, (int)pos.y);
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
        Player player = GameObject.FindObjectOfType<Player>();

        while (positions.Count < num)
        {
            for (int r = row - radius; r <= row + radius; r++)
            {
                for (int c = col - radius; c <= col + radius; c++)
                {
                    if (inside_circle(new Vector2(row, col), new Vector2(r, c), radius))
                    {
                        if (GameManager.gmInstance.Dungeon.tileMap[r, c].isEmpty()
                            && !positions.Contains(new Vector2(r, c))
                            && (player.getRow() != r && player.getCol() != c))
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

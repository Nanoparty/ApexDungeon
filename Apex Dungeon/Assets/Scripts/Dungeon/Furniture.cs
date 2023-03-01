using UnityEngine;
using UnityEngine.UIElements;

public class Furniture : MonoBehaviour
{
    public Sprite damaged;

    int health = 2;
    int max = 2;
    new private SpriteRenderer renderer;
    private int row;
    private int col;

    public ParticleSystem wood;
    public ConsumableGenerator consumableGenerator;
    public EquipmentGenerator equipmentGenerator;

    private GameObject itemContainer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        row = (int)transform.position.y;
        col = (int)transform.position.x;
        GameManager.gmInstance.AddFurnitureToList(this);
        itemContainer = GameObject.Find("ItemContainer");
    }

    public void setDamage(int d)
    {
        health += d;

        Vector3 position = new Vector3(col, row, 0f);
        Instantiate(wood, position, Quaternion.identity);

        if (health < max)
        {
            renderer.sprite = damaged;
        }
        if(health <= 0)
        {
            DropLoot();
            GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
            GameManager.gmInstance.removeFurniture(this);
            Destroy(this.gameObject);
        }
    }

    void DropLoot()
    {
        Vector3 position = new Vector3(col, row, 0f);
        float roll = Random.Range(0f, 1f);
        GameObject item = null;

        //Drop Equipment
        if (roll <= 0.2f)
        {
            item = equipmentGenerator.GenerateEquipment(GameManager.gmInstance.level);
        }
        else if (roll <= 0.6f)
        {
            item = consumableGenerator.CreateRandomConsumable(GameManager.gmInstance.level);
        }
        else
        {
            item = consumableGenerator.CreateRandomMoney(GameManager.gmInstance.level);
        }

        item.transform.position = position;
        itemContainer = GameObject.Find("ItemContainer");
        item.transform.parent = itemContainer.transform;
        if (item.GetComponent<Pickup>() != null)
        {
            item.GetComponent<Pickup>().SetLocation((int)row, (int)col);
        }
        if (item.GetComponent<Money>() != null)
        {
            item.GetComponent<Money>().SetLocation((int)row, (int)col);
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

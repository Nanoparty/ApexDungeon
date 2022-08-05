using UnityEngine;

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
            float coin = Random.Range(0f, 1f);
            if (coin > 0.5f)
            {
                Vector3 localPosition = new Vector3(col, row, 0f);
                GameObject item = consumableGenerator.CreateRandomMoney(GameManager.gmInstance.level);
                item.transform.parent = itemContainer.transform;
                item.transform.position = position;
            }

            GameManager.gmInstance.Dungeon.tileMap[row, col].occupied = 0;
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

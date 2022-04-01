using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableGenerator", menuName = "ScriptableObjects/Consumable Generator")]
public class ConsumableGenerator : ScriptableObject
{
    public Sprite redPotion;
    public Sprite bluePotion;
    public Sprite greenPotion;

    public GameObject CreateHealthPotion()
    {
        GameObject item = new GameObject("HealthPotion");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = redPotion;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        item.AddComponent<Consumable>();
        item.GetComponent<Consumable>().SetStats("Health Potion", "Cherry Flavor", "Heals 10 HP", redPotion);

        item.tag = "Potion";

        return item;
    }

    public GameObject CreateManaPotion()
    {
        GameObject item = new GameObject("ManaPotion");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = bluePotion;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        item.AddComponent<Consumable>();
        item.GetComponent<Consumable>().SetStats("ManaPotion", "Blueberry Flavor", "Restores 10 MP", bluePotion);

        item.tag = "Potion";

        return item;
    }

    public GameObject CreateRandomConsumable()
    {
        GameObject consumable = null;

        int rand = Random.Range(1, 3);

        if (rand == 1) consumable = CreateHealthPotion();
        else if (rand == 2) consumable = CreateManaPotion();

        if (consumable == null) consumable = new GameObject();

        return consumable;
    }
}

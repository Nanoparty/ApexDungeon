using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableGenerator", menuName = "ScriptableObjects/Consumable Generator")]
public class ConsumableGenerator : ScriptableObject
{
    public Sprite redPotion;
    public Sprite bluePotion;
    public Sprite greenPotion;

    public Sprite redBook;
    public Sprite blackBook;
    public Sprite greenBook;

    public Sprite blueOrb;
    public Sprite blackOrb;

    public Sprite scroll;
    public Sprite map;

    public Sprite redFlask;
    public Sprite blueFlask;
    public Sprite greenFlask;

    public Sprite redLeaf;
    public Sprite greenLeaf;
    public Sprite yellowLeaf;

    public GameObject CreateHealthPotion()
    {
        GameObject item = new GameObject("HealthPotion");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = redPotion;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable potion = new Consumable("Health Potion", "Cherry Flavor", "Heals 10 HP", redPotion);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(potion);

        item.tag = "Consumable";

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

        Consumable potion = new Consumable("ManaPotion", "Blueberry Flavor", "Restores 10 MP", bluePotion);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(potion);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateSkipOrb(){
        GameObject item = new GameObject("Skip Orb");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blueOrb;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable("Skip Orb", "This floor sucks", "Teleports you to the next floor", blueOrb);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateTeleportOrb(){
        GameObject item = new GameObject("Teleport Orb");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blueOrb;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable("Teleport Orb", "Anywhere but here please", "Teleports you randomly on this floor", blueOrb);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateDeathOrb(){
        GameObject item = new GameObject("Death Orb");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blackOrb;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable("Death Orb", "Very Edgy", "Kills all enemies in your vision.", blackOrb);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateMap(){
        GameObject item = new GameObject("Map Fragment");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = map;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable("Map Fragment", "Great Explorer", "Reveals the entire map of any floor", map);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateRandomConsumable()
    {
        GameObject consumable = null;

        int rand = Random.Range(1, 3);

        if (rand == 1) consumable = CreateHealthPotion();
        else if (rand == 2) consumable = CreateManaPotion();

        if (consumable == null) consumable = new GameObject();

        return CreateSkipOrb();
    }
}

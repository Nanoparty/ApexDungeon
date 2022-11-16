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

    public Sprite gold;
    public Sprite silver;
    public Sprite copper;

    public GameObject CreateHealthPotion(int level)
    {
        GameObject item = new GameObject("HealthPotion lvl " + level);

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = redPotion;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        int healAmount = (int)(50 + 50 * 0.2 * (level - 1));

        Consumable potion = new Consumable("HealthPotion", "Health Potion Lvl " + level, "Cherry Flavor", "Heals " + healAmount + " HP", redPotion, level);

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

        Consumable potion = new Consumable("ManaPotion", "ManaPotion", "Blueberry Flavor", "Restores 10 MP", bluePotion);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(potion);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateSkipOrb(){
        GameObject item = new GameObject("Skip Orb");
        string id = "SkipOrb";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blueOrb;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Skip Orb", "This floor sucks", "Teleports you to the next floor", blueOrb);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateLightOrb(){
        GameObject item = new GameObject("Light Orb");
        string id = "LightOrb";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blueOrb;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Light Orb", "Don't look under the bed.", "Lights up the entire floor", blueOrb);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateTeleportOrb(){
        GameObject item = new GameObject("Teleport Orb");
        string id = "TeleportOrb";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blueOrb;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Teleport Orb", "Anywhere but here please", "Teleports you randomly on this floor", blueOrb);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateDeathOrb(){
        GameObject item = new GameObject("Death Orb");
        string id = "DeathOrb";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blackOrb;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Death Orb", "Very Edgy", "Kills all enemies in your vision.", blackOrb);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateMap(){
        GameObject item = new GameObject("Map Fragment");
        string id = "MapFragment";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = map;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Map Fragment", "Great Explorer", "Reveals the entire map of any floor", map);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateGold(int level)
    {
        GameObject item = new GameObject("Gold");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = gold;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Money money = item.AddComponent<Money>();
        money.amount = (int)(100 * Mathf.Pow(1.5f, level - 1)); ;

        item.tag = "Gold";

        return item;
    }

    public GameObject CreateSilver(int level)
    {
        GameObject item = new GameObject("Silver");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = silver;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Money money = item.AddComponent<Money>();
        money.amount = (int)(50 * Mathf.Pow(1.5f, level - 1)); ;

        item.tag = "Silver";

        return item;
    }

    public GameObject CreateCopper(int level)
    {
        GameObject item = new GameObject("Copper");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = copper;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Money money = item.AddComponent<Money>();
        money.amount = (int)(25 * Mathf.Pow(1.5f, level - 1));

        item.tag = "Copper";

        return item;
    }

    public GameObject CreateRandomConsumable(int level)
    {
        GameObject consumable = null;

        int rand = Random.Range(1, 8);

        if (rand == 1) consumable = CreateHealthPotion(level);
        if (rand == 2) consumable = CreateHealthPotion(level);
        if (rand == 3) consumable = CreateDeathOrb();
        if (rand == 4) consumable = CreateLightOrb();
        if (rand == 5) consumable = CreateTeleportOrb();
        if (rand == 6) consumable = CreateMap();
        if (rand == 7) consumable = CreateSkipOrb();


        if (consumable == null) consumable = new GameObject();

        return consumable;
    }

    public GameObject CreateRandomMoney(int level)
    {
        GameObject money = null;

        int rand = Random.Range(1, 4);

        if (rand == 1) money = CreateGold(level);
        if (rand == 2) money = CreateSilver(level);
        if (rand == 3) money = CreateCopper(level);

        return money;
    }
}

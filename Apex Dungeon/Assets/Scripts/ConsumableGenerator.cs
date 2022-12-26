using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableGenerator", menuName = "ScriptableObjects/Consumable Generator")]
public class ConsumableGenerator : ScriptableObject
{
    public Sprite redPotion;
    public Sprite bluePotion;
    public Sprite greenPotion;
    public Sprite yellowPotion;
    public Sprite blackPotion;

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
    public Sprite yellowFlask;

    public Sprite redLeaf;
    public Sprite greenLeaf;
    public Sprite yellowLeaf;

    public Sprite gold;
    public Sprite silver;
    public Sprite copper;

    public Sprite bandage;
    public Sprite rope;
    public Sprite heart;
    public Sprite dice;
    public Sprite chicken;

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

    public GameObject CreateHealthRegenPotion()
    {
        GameObject item = new GameObject("HealthRegenPotion");

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = redFlask;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable potion = new Consumable("HealthRegenPotion", "Health Regen Potion", "Strawberry Flavor", "Restores 10% of Max Health per turn for 5 turns.", redFlask);

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

    public GameObject CreateBandage()
    {
        GameObject item = new GameObject("Bandage");
        string id = "Bandage";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = bandage;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Bandage", "Just a flesh wound", "Cleanse user of bleeding", bandage);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateAntidote()
    {
        GameObject item = new GameObject("Antidote");
        string id = "Antidote";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = yellowPotion;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Antidote", "I don't feel so good...", "Cleanse user of poison", yellowPotion);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateFullCleanse()
    {
        GameObject item = new GameObject("FullCleanse");
        string id = "FullCleanse";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = blackPotion;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Full Cleanse", "This might be overkill", "Cleanse user of all Status Effects", blackPotion);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateHeartGem()
    {
        GameObject item = new GameObject("HeartGem");
        string id = "HeartGem";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = heart;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Heart Gem", "Not anatomically accurate", "Permemantly increase HP by 10%", heart);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateExpPotion()
    {
        GameObject item = new GameObject("ExpPotion");
        string id = "ExpPotion";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = greenPotion;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Experience Potion", "Wise men learn from the experience of others", "Permenantly Gain 1 level", greenPotion);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateMagicDice()
    {
        GameObject item = new GameObject("MagicDice");
        string id = "MagicDice";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = dice;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Magic Dice", "Luck always beats skill", "Randomly gain 1 status effect", dice);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateEscapeRope()
    {
        GameObject item = new GameObject("EscapeRope");
        string id = "EscapeRope";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = rope;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Escape Rope", "Tactical Retreat", "Return to previous floor", rope);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateChicken()
    {
        GameObject item = new GameObject("Chicken");
        string id = "Chicken";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = chicken;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Chicken", "Lemon Pepper", "Restores 10% HP per turn for 2 turns", chicken);

        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(orb);

        item.tag = "Consumable";

        return item;
    }

    public GameObject CreateBuffScroll()
    {
        GameObject item = new GameObject("BuffScroll");
        string id = "BuffScroll";

        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = scroll;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;

        Consumable orb = new Consumable(id, "Buff Scroll", "Plus Ultra!", "Temporarily boosts one stat", scroll);

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

        int rand = Random.Range(1, 23);

        if (rand == 1) consumable = CreateHealthPotion(level);
        if (rand == 3) consumable = CreateHealthPotion(level);
        if (rand == 4) consumable = CreateHealthPotion(level);
        if (rand == 5) consumable = CreateHealthPotion(level);
        if (rand == 6) consumable = CreateHealthPotion(level);
        if (rand == 7) consumable = CreateHealthPotion(level);
        if (rand == 8) consumable = CreateDeathOrb();
        if (rand == 9) consumable = CreateLightOrb();
        if (rand == 10) consumable = CreateTeleportOrb();
        if (rand == 11) consumable = CreateMap();
        if (rand == 12) consumable = CreateSkipOrb();
        if (rand == 13) consumable = CreateAntidote();
        if (rand == 14) consumable = CreateBandage();
        if (rand == 15) consumable = CreateHealthRegenPotion();
        if (rand == 16) consumable = CreateBuffScroll();
        if (rand == 17) consumable = CreateChicken();
        if (rand == 18) consumable = CreateEscapeRope();
        if (rand == 19) consumable = CreateExpPotion();
        if (rand == 20) consumable = CreateFullCleanse();
        if (rand == 21) consumable = CreateHeartGem();
        if (rand == 22) consumable = CreateMagicDice();

        if (consumable == null) consumable = new GameObject();

        return CreateEscapeRope();
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

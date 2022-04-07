using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentGenerator", menuName = "ScriptableObjects/Equipment Generator")]
public class EquipmentGenerator : ScriptableObject
{
    //Images
    public Sprite LeatherHelm;
    public Sprite LeatherChest;
    public Sprite LeatherGloves;
    public Sprite LeatherLegs;
    public Sprite LeatherBoots;

    public Sprite IronHelm;
    public Sprite IronChest;
    public Sprite IronGloves;
    public Sprite IronLegs;
    public Sprite IronBoots;

    public Sprite DiamondHelm;
    public Sprite DiamondChest;
    public Sprite DiamondGloves;
    public Sprite DiamondLegs;
    public Sprite DiamondBoots;

    public Sprite IronRing;
    public Sprite GoldRing;
    public Sprite DiamondRing;

    public Sprite IronNecklace;
    public Sprite GoldNecklace;
    public Sprite DiamondNecklace;

    public Sprite Dagger;
    public Sprite Sword;
    public Sprite Axe;

    public Sprite LeatherShield;
    public Sprite IronShield;
    public Sprite DiamondShield;

    //Parameters
    int level;
    string type;
    int tier;

    //stats
    Sprite image;
    int defense;
    int attack;
    int intelligence;
    int crit;

    string modifier;

    public GameObject GenerateEquipment(int inputLevel, string inputType, int inputTier)
    {
        level = inputLevel;
        type = inputType;
        tier = inputTier;

        // switch (level)
        // {
        //     case 1:
        //         defense = 10;
        //         attack = 10;
        //         intelligence = 10;
        //         crit = 10;
        //         break;
        //     case 2:
        //         defense = 20;
        //         attack = 20;
        //         intelligence = 20;
        //         crit = 20;
        //         break;
        //     case 3:
        //         defense = 30;
        //         attack = 30;
        //         intelligence = 30;
        //         crit = 30;
        //         break;
        // }

        switch (type)
        {
            case "helmet":
                switch (tier)
                {
                    case 1:
                        image = LeatherHelm;
                        break;
                    case 2:
                        image = IronHelm;
                        break;
                    case 3:
                        image = DiamondHelm;
                        break;
                }
                break;
            case "chestplate":
                switch (tier)
                {
                    case 1:
                        image = LeatherChest;
                        break;
                    case 2:
                        image = IronChest;
                        break;
                    case 3:
                        image = DiamondChest;
                        break;
                }
                break;
            case "legs":
                switch (tier)
                {
                    case 1:
                        image = LeatherLegs;
                        break;
                    case 2:
                        image = IronLegs;
                        break;
                    case 3:
                        image = DiamondLegs;
                        break;
                }
                break;
            case "boots":
                switch (tier)
                {
                    case 1:
                        image = LeatherBoots;
                        break;
                    case 2:
                        image = IronBoots;
                        break;
                    case 3:
                        image = DiamondBoots;
                        break;
                }
                break;
            case "weapon":
                switch (tier)
                {
                    case 1:
                        image = Dagger;
                        break;
                    case 2:
                        image = Sword;
                        break;
                    case 3:
                        image = Axe;
                        break;
                }
                break;
            case "shield":
                switch (tier)
                {
                    case 1:
                        image = LeatherShield;
                        break;
                    case 2:
                        image = IronShield;
                        break;
                    case 3:
                        image = DiamondShield;
                        break;
                }
                break;
            case "necklace":
                switch (tier)
                {
                    case 1:
                        image = IronNecklace;
                        break;
                    case 2:
                        image = GoldNecklace;
                        break;
                    case 3:
                        image = DiamondNecklace;
                        break;
                }
                break;
            case "ring":
                switch (tier)
                {
                    case 1:
                        image = IronRing;
                        break;
                    case 2:
                        image = GoldRing;
                        break;
                    case 3:
                        image = DiamondRing;
                        break;
                }
                break;
        }

        //pick stats
        int defNum = Random.Range(1, 11);
        int atkNum = Random.Range(1, 11);
        int intNum = Random.Range(1, 11);
        int crtNum = Random.Range(1, 11);

        GameObject equipment = new GameObject("equipment");

        equipment.AddComponent<SpriteRenderer>();
        equipment.GetComponent<SpriteRenderer>().sprite = image;
        equipment.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        equipment.AddComponent<BoxCollider2D>();
        equipment.GetComponent<BoxCollider2D>().isTrigger = true;

        Equipment item = new Equipment(level, type, tier, image, defNum, atkNum, intNum, crtNum, modifier);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);

        equipment.tag = "Equipment";

        return equipment;
    }

    public GameObject GenerateEquipment(int level)
    {
        //pick type
        int typeNum = Random.Range(1,9);
        if (typeNum == 1) type = "helmet";
        else if (typeNum == 2) type = "chestplate";
        else if (typeNum == 3) type = "boots";
        else if (typeNum == 4) type = "legs";
        else if (typeNum == 5) type = "weapon";
        else if (typeNum == 6) type = "shield";
        else if (typeNum == 7) type = "necklace";
        else if (typeNum == 8) type = "ring";

        //pick tier
        int tierNum = Random.Range(1, 4);

        //pick defense stat

        //pick modifier
        
        //create gameobject
        return GenerateEquipment(level, type, tierNum);
    }
}

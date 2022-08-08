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

    public Sprite GoldHelm;
    public Sprite GoldChest;
    public Sprite GoldGloves;
    public Sprite GoldLegs;
    public Sprite GoldBoots;

    public Sprite DiamondHelm;
    public Sprite DiamondChest;
    public Sprite DiamondGloves;
    public Sprite DiamondLegs;
    public Sprite DiamondBoots;

    public Sprite CopperRing;
    public Sprite IronRing;
    public Sprite GoldRing;
    public Sprite DiamondRing;

    public Sprite CopperNecklace;
    public Sprite IronNecklace;
    public Sprite GoldNecklace;
    public Sprite DiamondNecklace;

    public Sprite Dagger;
    public Sprite Sword;
    public Sprite Axe;
    public Sprite Hammer;

    public Sprite LeatherShield;
    public Sprite IronShield;
    public Sprite GoldShield;
    public Sprite DiamondShield;

    //Parameters
    int level;
    string type;
    public int tier;

    //stats
    Sprite image;
    int defense;
    int attack;
    int intelligence;
    int crit;

    string modifier;

    public Sprite getEquipmentImage(int tier, string type)
    {
        Sprite image = LeatherBoots;
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
                        image = GoldHelm;
                        break;
                    case 4:
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
                        image = GoldChest;
                        break;
                    case 4:
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
                        image = GoldLegs;
                        break;
                    case 4:
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
                        image = GoldBoots;
                        break;
                    case 4:
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
                        image = Hammer;
                        break;
                    case 4:
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
                        image = GoldShield;
                        break;
                    case 4:
                        image = DiamondShield;
                        break;
                }
                break;
            case "necklace":
                switch (tier)
                {
                    case 1:
                        image = CopperNecklace;
                        break;
                    case 2:
                        image = IronNecklace;
                        break;
                    case 3:
                        image = GoldNecklace;
                        break;
                    case 4:
                        image = DiamondNecklace;
                        break;
                }
                break;
            case "ring":
                switch (tier)
                {
                    case 1:
                        image = CopperRing;
                        break;
                    case 2:
                        image = IronRing;
                        break;
                    case 3:
                        image = GoldRing;
                        break;
                    case 4:
                        image = DiamondRing;
                        break;
                }
                break;
        }
        return image;
    }

    public GameObject GenerateEquipment(int inputLevel, string inputType, int inputTier)
    {
        level = inputLevel;
        type = inputType;
        tier = inputTier;

        int attackDamage = 10;
        int hpBoost = 10;

        int critBoost = 0;
        int evadeBoost = 0;

        attackDamage = (int)(attackDamage * Mathf.Pow(1.5f, level-1));
        hpBoost = (int)(hpBoost * Mathf.Pow(1.5f,level-1));

        int up = Random.Range(0, 20);
        int down = Random.Range(0, 20);

        attackDamage = (int)(attackDamage * (1 + (up - down) * 0.01));
        hpBoost = (int)(hpBoost * (1 + (up - down) * 0.01));

        if (tier == 2)
        {
            attackDamage = (int)(attackDamage * 1.1);
            hpBoost = (int)(hpBoost * 1.1);
        }
        if(tier == 3)
        {
            attackDamage = (int)(attackDamage * 1.2);
            hpBoost = (int)(hpBoost * 1.2);
        }
        if(tier == 4)
        {
            attackDamage = (int)(attackDamage * 1.3);
            hpBoost = (int)(hpBoost * 1.3);
        }

        if(type == "weapon")
        {
            hpBoost = 0;
        }
        else
        {
            attackDamage = 0;
        }

        image = getEquipmentImage(tier, type);

        GameObject equipment = new GameObject("equipment");

        equipment.AddComponent<SpriteRenderer>();
        equipment.GetComponent<SpriteRenderer>().sprite = image;
        equipment.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        equipment.AddComponent<BoxCollider2D>();
        equipment.GetComponent<BoxCollider2D>().isTrigger = true;

        Equipment item = new Equipment(level, type, tier, image, hpBoost, attackDamage, critBoost, evadeBoost);

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
        //1% legendary/diamond
        //10% unique/gold
        //25% rare/iron
        //75% common/leather
        int tierNum = 0;

        int dice = Random.Range(1,101);
        if(dice >= 95){
            tierNum = 4;
        }
        else if(dice >= 85){
            tierNum = 3;
        }
        else if(dice >= 65){
            tierNum = 2;
        }else{
            tierNum = 1;
        }
        
        //create gameobject
        return GenerateEquipment(level, type, tierNum);
    }
}

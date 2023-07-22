using UnityEngine;
using UnityEngine.UIElements;
using static Equipment;

[CreateAssetMenu(fileName = "EquipmentGenerator", menuName = "ScriptableObjects/Equipment Generator")]
public class EquipmentGenerator : ScriptableObject
{
    // Container Objects
    //private Transform itemContainer;
    public ShieldGenerator shieldGenerator;
    public GlovesGenerator glovesGenerator;
    public ChestGenerator chestGenerator;
    public HelmetGenerator helmetGenerator;
    public ShoesGenerator bootsGenerator;

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

    public Sprite Slingshot;
    public Sprite Shortbow;
    public Sprite Longbow;
    public Sprite Crossbow;

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

    public Sprite getEquipmentImage(int tier, string type, int range = 1)
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
                if (range == 1)
                {
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
                }
                else
                {
                    switch (tier)
                    {
                        case 1:
                            image = Slingshot;
                            break;
                        case 2:
                            image = Shortbow;
                            break;
                        case 3:
                            image = Longbow;
                            break;
                        case 4:
                            image = Crossbow;
                            break;
                    }
                    break;
                }
                
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

    public Sprite GetImage(EquipType et)
    {
        //if (et == EquipType.HELMET) return helmetGenerator.GetImage(et);
        //if (et == EquipType.CHESTPLATE) return chestGenerator.GetImage(et);
        //if (et == EquipType.BOOTS) return bootsGenerator.GetImage(et);
        //if (et == EquipType.GLOVES) return glovesGenerator.GetImage(et);
        //if (et == EquipType.SHIELD) return shieldGenerator.GetImage(et);
        return null;
    }

    public GameObject GenerateEquipOfType(int inputLevel, string inputType, int inputTier, bool ranged)
    {
        level = inputLevel;
        type = inputType;
        tier = inputTier;

        int attackDamage = 10;
        int hpBoost = 10;

        int critBoost = 0;
        int evadeBoost = 0;

        int range = 1;

        attackDamage = (int)(attackDamage + attackDamage * 0.1 * (level - 1));
        hpBoost = (int)(hpBoost + hpBoost * 0.1 * (level - 1));

        int attackUpperRange = (int)(attackDamage * 0.5);
        int attackLowerRange = (int)(attackDamage * 0.2);

        int hpUpperRange = (int)(hpBoost * 0.5);
        int hpLowerRange = (int)(hpBoost * 0.2);

        int attackUp = Random.Range(0, attackUpperRange);
        int attackDown = Random.Range(0, attackLowerRange);

        int hpUp = Random.Range(0, hpUpperRange);
        int hpDown = Random.Range(0, hpLowerRange);

        attackDamage = (int)(attackDamage * (1 + (attackUp - attackDown) * 0.01));
        hpBoost = (int)(hpBoost * (1 + (hpUp - hpDown) * 0.01));

        //add crit
        if (Random.Range(0, 100) > 75)
        {
            critBoost = Random.Range(1, 5);
        }
        //add evade
        if (Random.Range(0, 100) > 75)
        {
            evadeBoost = Random.Range(1, 5);
        }

        if (tier == 2)
        {
            attackDamage = (int)(attackDamage * 1.1);
            hpBoost = (int)(hpBoost * 1.1);
            if (critBoost > 0)
                critBoost += Random.Range(1, 2);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(1, 2);
        }
        if (tier == 3)
        {
            attackDamage = (int)(attackDamage * 1.2);
            hpBoost = (int)(hpBoost * 1.2);
            if (critBoost > 0)
                critBoost += Random.Range(2, 3);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(2, 3);
        }
        if (tier == 4)
        {
            attackDamage = (int)(attackDamage * 1.3);
            hpBoost = (int)(hpBoost * 1.3);
            if (critBoost > 0)
                critBoost += Random.Range(3, 5);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(3, 5);
        }

        if (type == "weapon")
        {
            if (ranged)
            {
                range = 3;

                attackDamage = (int)((double)attackDamage * 0.7);
            }
            

            if (Random.Range(0, 100) < 75)
            {
                hpBoost = 0;
            }
            else
            {
                hpBoost = (int)(hpBoost * 0.5);
            }
        }
        else
        {
            if (Random.Range(0, 100) < 75)
            {
                attackDamage = 0;
            }
            else
            {
                attackDamage = (int)(attackDamage * 0.5);
            }
        }

        image = getEquipmentImage(tier, type, range);

        GameObject equipment = new GameObject("equipment");

        equipment.AddComponent<SpriteRenderer>();
        equipment.GetComponent<SpriteRenderer>().sprite = image;
        equipment.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        equipment.AddComponent<BoxCollider2D>();
        equipment.GetComponent<BoxCollider2D>().isTrigger = true;

        Equipment item = new Equipment(level, type, tier, image, hpBoost, attackDamage, critBoost, evadeBoost, range);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);

        equipment.tag = "Equipment";

        return equipment;
    }

    public Equipment GenerateEquipOfTypeNoPickup(int inputLevel, string inputType, int inputTier, bool ranged = false)
    {
        level = inputLevel;
        type = inputType;
        tier = inputTier;

        int attackDamage = 10;
        int hpBoost = 10;

        int critBoost = 0;
        int evadeBoost = 0;

        int range = 1;

        attackDamage = (int)(attackDamage + attackDamage * 0.1 * (level - 1));
        hpBoost = (int)(hpBoost + hpBoost * 0.1 * (level - 1));

        int attackUpperRange = (int)(attackDamage * 0.5);
        int attackLowerRange = (int)(attackDamage * 0.2);

        int hpUpperRange = (int)(hpBoost * 0.5);
        int hpLowerRange = (int)(hpBoost * 0.2);

        int attackUp = Random.Range(0, attackUpperRange);
        int attackDown = Random.Range(0, attackLowerRange);

        int hpUp = Random.Range(0, hpUpperRange);
        int hpDown = Random.Range(0, hpLowerRange);

        attackDamage = (int)(attackDamage * (1 + (attackUp - attackDown) * 0.01));
        hpBoost = (int)(hpBoost * (1 + (hpUp - hpDown) * 0.01));

        //add crit
        if (Random.Range(0, 100) > 75)
        {
            critBoost = Random.Range(1, 5);
        }
        //add evade
        if (Random.Range(0, 100) > 75)
        {
            evadeBoost = Random.Range(1, 5);
        }

        if (tier == 2)
        {
            attackDamage = (int)(attackDamage * 1.1);
            hpBoost = (int)(hpBoost * 1.1);
            if (critBoost > 0)
                critBoost += Random.Range(1, 2);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(1, 2);
        }
        if (tier == 3)
        {
            attackDamage = (int)(attackDamage * 1.2);
            hpBoost = (int)(hpBoost * 1.2);
            if (critBoost > 0)
                critBoost += Random.Range(2, 3);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(2, 3);
        }
        if (tier == 4)
        {
            attackDamage = (int)(attackDamage * 1.3);
            hpBoost = (int)(hpBoost * 1.3);
            if (critBoost > 0)
                critBoost += Random.Range(3, 5);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(3, 5);
        }

        if (type == "weapon")
        {
            if (ranged)
            {
                range = 3;

                attackDamage = (int)((double)attackDamage * 0.7);
            }


            if (Random.Range(0, 100) < 75)
            {
                hpBoost = 0;
            }
            else
            {
                hpBoost = (int)(hpBoost * 0.5);
            }
        }
        else
        {
            if (Random.Range(0, 100) < 75)
            {
                attackDamage = 0;
            }
            else
            {
                attackDamage = (int)(attackDamage * 0.5);
            }
        }

        image = getEquipmentImage(tier, type, range);

        Equipment item = new Equipment(level, type, tier, image, hpBoost, attackDamage, critBoost, evadeBoost, range);

        return item;
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

        int range = 1;

        attackDamage = (int)(attackDamage + attackDamage * 0.1 * (level -1 ));
        hpBoost = (int)(hpBoost + hpBoost * 0.1 * (level -1));

        int attackUpperRange = (int)(attackDamage * 0.5);
        int attackLowerRange = (int)(attackDamage * 0.2);

        int hpUpperRange = (int)(hpBoost * 0.5);
        int hpLowerRange = (int)(hpBoost * 0.2);

        int attackUp = Random.Range(0, attackUpperRange);
        int attackDown = Random.Range(0, attackLowerRange);

        int hpUp = Random.Range(0, hpUpperRange);
        int hpDown = Random.Range(0, hpLowerRange);

        attackDamage = (int)(attackDamage * (1 + (attackUp - attackDown) * 0.01));
        hpBoost = (int)(hpBoost * (1 + (hpUp - hpDown) * 0.01));

        //add crit
        if (Random.Range(0, 100) > 75)
        {
            critBoost = Random.Range(1, 5);
        }
        //add evade
        if (Random.Range(0, 100) > 75)
        {
            evadeBoost = Random.Range(1, 5);
        }

        if (tier == 2)
        {
            attackDamage = (int)(attackDamage * 1.1);
            hpBoost = (int)(hpBoost * 1.1);
            if (critBoost > 0)
                critBoost += Random.Range(1, 2);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(1, 2);
        }
        if(tier == 3)
        {
            attackDamage = (int)(attackDamage * 1.2);
            hpBoost = (int)(hpBoost * 1.2);
            if (critBoost > 0)
                critBoost += Random.Range(2, 3);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(2, 3);
        }
        if(tier == 4)
        {
            attackDamage = (int)(attackDamage * 1.3);
            hpBoost = (int)(hpBoost * 1.3);
            if (critBoost > 0)
                critBoost += Random.Range(3, 5);
            if (evadeBoost > 0)
                evadeBoost += Random.Range(3, 5);
        }

        if(type == "weapon")
        {
            if (Random.Range(0, 100) < 25)
            {
                range = 3;
                attackDamage = (int)((double)attackDamage * 0.7);
            }

            if (Random.Range(0, 100) < 75)
            {
                hpBoost = 0;
            }else
            {
                hpBoost = (int)(hpBoost * 0.5);
            }
        }
        else
        {
            if (Random.Range(0,100) < 75)
            {
                attackDamage = 0;
            }
            else
            {
                attackDamage = (int)(attackDamage * 0.5);
            }
        }

        image = getEquipmentImage(tier, type, range);

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

        //if (type == "shield")
        //{
        //    return shieldGenerator.CreateRandomShield(level);
        //}
        //if (type == "boots")
        //{
        //    return bootsGenerator.CreateRandomBoots(level);
        //}
        //if (type == "chestplate")
        //{
        //    return chestGenerator.CreateRandomChest(level);
        //}
        //if (type == "helmet")
        //{
        //    return helmetGenerator.CreateRandomHelmet(level);
        //}
        //if (type == "gloves")
        //{
        //    return glovesGenerator.CreateRandomGloves(level);
        //}
        //return glovesGenerator.CreateRandomGloves(level);

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

    public GameObject CreatePickup(Equipment e, int r, int c)
    { 
        GameObject equipment = new GameObject("equipment");

        equipment.AddComponent<SpriteRenderer>();
        equipment.GetComponent<SpriteRenderer>().sprite = e.image;
        equipment.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

        equipment.AddComponent<BoxCollider2D>();
        equipment.GetComponent<BoxCollider2D>().isTrigger = true;

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(e);

        equipment.tag = "Equipment";

        equipment.transform.parent = GameManager.gmInstance.DunGen.itemContainer.transform;
        equipment.transform.position = new Vector2(c, r);
        equipment.GetComponent<Pickup>().SetLocation(r, c);
        GameManager.gmInstance.Dungeon.itemList.Add(new Vector2(r, c));

        return equipment;
    }

    //public Equipment CreateEquipment(Equipment e)
    //{
    //    if (e.etype == EquipType.HELMET)
    //    {
    //        Helm
    //        return new Helmet(e.level, e.type, e.tier, e.itemName, e.description, e.GetImage(), e.defense, e.attack, e.crit, e.evade, e)
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
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
            case "gloves":
                switch (tier)
                {
                    case 1:
                        image = LeatherGloves;
                        break;
                    case 2:
                        image = IronGloves;
                        break;
                    case 3:
                        image = DiamondGloves;
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

        equipment.AddComponent<Equipment>();
        equipment.GetComponent<Equipment>().setStats(level, type, tier, image, defNum, atkNum, intNum, crtNum, modifier); ;

        equipment.tag = "Equipment";

        //Debug.Log("Gen Equipment:" + equipment.GetComponent<Equipment>().name);

        return equipment;
    }

    public GameObject GenerateEquipment(int level)
    {
        //pick type
        int typeNum = Random.Range(1, 6);
        if (typeNum == 1) type = "helmet";
        else if (typeNum == 2) type = "chestplate";
        else if (typeNum == 3) type = "gloves";
        else if (typeNum == 4) type = "legs";
        else if (typeNum == 5) type = "boots";

        //pick tier
        int tierNum = Random.Range(1, 4);

        //pick defense stat
        //int defNum = Random.Range(1, 11);

        //pick modifier
        

        //create gameobject
        return GenerateEquipment(level, type, tierNum);
    }
}

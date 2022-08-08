using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageLookup", menuName = "ScriptableObjects/Image Lookup")]
public class ImageLookup : ScriptableObject
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

    public Sprite getImage(string id)
    {
        var images = new Dictionary<string, Sprite>();
        images["HealthPotion"] = redPotion;
        images["Skip Orb"] = images["Light Orb"] = images["Teleport Orb"] = blueOrb;
        images["Death Orb"] = blackOrb;
        images["MapFragment"] = map;
        
        return images[id];
    }

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

}

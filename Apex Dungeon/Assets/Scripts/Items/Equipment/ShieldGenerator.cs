using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static Shield;

[CreateAssetMenu(fileName = "ShieldGenerator", menuName = "ScriptableObjects/Shield Generator")]
public class ShieldGenerator : ScriptableObject
{
    public Sprite woodenBuckler;
    public Sprite studdedBuckler;
    public Sprite ironBuckler;
    public Sprite steelBuckler;
    public Sprite spikedBuckler;

    public Sprite towerShield;
    public Sprite woodenKnight;
    public Sprite ironKnight;
    public Sprite steelKnight;
    public Sprite royalKnight;

    public Sprite steelKite;
    public Sprite holyKite;
    public Sprite porkKite;
    public Sprite silverKite;
    public Sprite goldKite;

    public Sprite cyber;
    public Sprite winged;
    public Sprite marble;
    public Sprite target;

    public int GetTier()
    {
        //pick tier
        //5% legendary/diamond
        //10% unique/gold
        //25% rare/iron
        //70% common/leather
        int tierNum = 0;

        int dice = UnityEngine.Random.Range(1, 101);
        if (dice >= 95)
        {
            tierNum = 4;
        }
        else if (dice >= 85)
        {
            tierNum = 3;
        }
        else if (dice >= 60)
        {
            tierNum = 2;
        }
        else
        {
            tierNum = 1;
        }
        return tierNum;
    }

    public int GetBlock(int tier)
    {
        if (tier == 4) { return 20; }
        if(tier == 3) { return 15; }
        if(tier == 2) { return 10; }
        if(tier == 1) { return 5; }
        return 0;
    } 

    public GameObject CreateShield(string name, string description, Sprite image, int level, ShieldType st, int spriteIndex = 0)
    {
        int tier = GetTier();
        int attackDamage = 10;
        int hpBoost = 10;

        int critBoost = 0;
        int evadeBoost = 0;

        attackDamage = (int)(attackDamage + attackDamage * 0.1 * (level - 1));
        hpBoost = (int)(hpBoost + hpBoost * 0.1 * (level - 1));

        int attackUpperRange = (int)(attackDamage * 0.5);
        int attackLowerRange = (int)(attackDamage * 0.2);

        int hpUpperRange = (int)(hpBoost * 0.5);
        int hpLowerRange = (int)(hpBoost * 0.2);

        int attackUp = UnityEngine.Random.Range(0, attackUpperRange);
        int attackDown = UnityEngine.Random.Range(0, attackLowerRange);

        int hpUp = UnityEngine.Random.Range(0, hpUpperRange);
        int hpDown = UnityEngine.Random.Range(0, hpLowerRange);

        attackDamage = (int)(attackDamage * (1 + (attackUp - attackDown) * 0.01));
        hpBoost = (int)(hpBoost * (1 + (hpUp - hpDown) * 0.01));

        //add crit
        if (UnityEngine.Random.Range(0, 100) > 75)
        {
            critBoost = UnityEngine.Random.Range(1, 5);
        }
        //add evade
        if (UnityEngine.Random.Range(0, 100) > 75)
        {
            evadeBoost = UnityEngine.Random.Range(1, 5);
        }

        if (tier == 2)
        {
            attackDamage = (int)(attackDamage * 1.1);
            hpBoost = (int)(hpBoost * 1.1);
            if (critBoost > 0)
                critBoost += UnityEngine.Random.Range(1, 2);
            if (evadeBoost > 0)
                evadeBoost += UnityEngine.Random.Range(1, 2);
        }
        if (tier == 3)
        {
            attackDamage = (int)(attackDamage * 1.2);
            hpBoost = (int)(hpBoost * 1.2);
            if (critBoost > 0)
                critBoost += UnityEngine.Random.Range(2, 3);
            if (evadeBoost > 0)
                evadeBoost += UnityEngine.Random.Range(2, 3);
        }
        if (tier == 4)
        {
            attackDamage = (int)(attackDamage * 1.3);
            hpBoost = (int)(hpBoost * 1.3);
            if (critBoost > 0)
                critBoost += UnityEngine.Random.Range(3, 5);
            if (evadeBoost > 0)
                evadeBoost += UnityEngine.Random.Range(3, 5);
        }

        if (UnityEngine.Random.Range(0, 100) < 75)
        {
            attackDamage = 0;
        }
        else
        {
            attackDamage = (int)(attackDamage * 0.5);
        }

        GameObject equipment = new GameObject(name);
        equipment.AddComponent<SpriteRenderer>();
        equipment.GetComponent<SpriteRenderer>().sprite = image;
        equipment.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        equipment.AddComponent<BoxCollider2D>();
        equipment.GetComponent<BoxCollider2D>().isTrigger = true;

        equipment.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        Shield item = new Shield(level, "shield", tier, image, name, description, hpBoost, attackDamage, critBoost, evadeBoost, st, spriteIndex);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);
        equipment.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        equipment.tag = "Equipment";

        return equipment;
    }

    public GameObject WoodenBuckler(int level)
    {
        return CreateShield("Wooden Buckler Shield", "Standard issue wooden shield.", woodenBuckler, level, ShieldType.WoodenBuckler);
    }
    public GameObject StuddedWoodenBuckler(int level)
    {
        return CreateShield("Studded Wooden Buckler Shield", "Standard issue wooden shield reinforced with metal studs.", studdedBuckler, level, ShieldType.StuddedBuckler);
    }
    public GameObject IronBuckler(int level)
    {
        return CreateShield("Iron Buckler Shield", "Standard issue wooden shield reinforced with iron plating.", ironBuckler, level, ShieldType.IronBuckler);
    }
    public GameObject SteelBuckler(int level)
    {
        return CreateShield("Steel Buckler Shield", "Sturdy steel shield.", steelBuckler, level, ShieldType.SteelBuckler);
    }
    public GameObject SpikedBuckler(int level)
    {
        return CreateShield("Spiked Buckler Shield", "Reliable steel shield with spikes to deter attackers.", spikedBuckler, level, ShieldType.SpikedBuckler);
    }
    public GameObject SteelTower(int level)
    {
        return CreateShield("Steel Tower Shield", "A heavy yet imposing steel shield that towers over enemies.", towerShield, level, ShieldType.Tower);
    }
    public GameObject WoodenKnight(int level)
    {
        return CreateShield("Wooden Knight Shield", "Standard wooden shield for valiant knights.", woodenKnight, level, ShieldType.WoodenKnight);
    }
    public GameObject IronKnight(int level)
    {
        return CreateShield("Iron Knight Shield", "Sturdy iron shield for valiant knights.", ironKnight, level, ShieldType.IronKnight);
    }
    public GameObject SteelKnight(int level)
    {
        return CreateShield("Steel Knight Shield", "Reinforced steel shield for elite knights.", steelKnight, level, ShieldType.SteelKnight);
    }
    public GameObject RoyalKnight(int level)
    {
        return CreateShield("Royal Knight Shield", "Crested shield for loyal knights of the royal guard.", royalKnight, level, ShieldType.RoyalKnight);
    }
    public GameObject SteelKite(int level)
    {
        return CreateShield("Steel Kite Shield", "Sturdy steel kite shield.", steelKite, level, ShieldType.SteelKite);
    }
    public GameObject HolyKite(int level)
    {
        return CreateShield("Holy Kite Shield", "Holy steel kite shield for members of the faith.", holyKite, level, ShieldType.HolyKite);
    }
    public GameObject Boar(int level)
    {
        return CreateShield("Demonic Boar Kite Shield", "Imposing shield with a sigil of a demonic boar.", porkKite, level, ShieldType.PorkKite);
    }
    public GameObject SilverKite(int level)
    {
        return CreateShield("Silver Kite Shield", "Fancy silver kite shield for those concerned with their appearance.", silverKite, level, ShieldType.SilverKite);
    }
    public GameObject GoldKite(int level)
    {
        return CreateShield("Gold Kite Shield", "Resplendent golden kite shield for those with wealth to flaunt.", goldKite, level, ShieldType.GoldKite);
    }
    public GameObject Cyber(int level)
    {
        return CreateShield("Cyber Shield", "Seems almost as if it came from the future...", cyber, level, ShieldType.Cyber);
    }
    public GameObject Winged(int level)
    {
        return CreateShield("Winged Shield", "Gives wearers the impression they could soar unto the heavens.", winged, level, ShieldType.Winged);
    }
    public GameObject Marble(int level)
    {
        return CreateShield("Marble Shield", "Exquisite shield masterfully carved from solid marble.", marble, level, ShieldType.Marble);
    }
    public GameObject Hero(int level)
    {
        return CreateShield("Hero Shield", "A shield that reminds of a certain valiant hero...", target, level, ShieldType.Hero);
    }

    public GameObject CreateRandomShield(int level)
    {
        List<Expression<Func<GameObject>>> shields = new List<Expression<Func<GameObject>>>();
        shields.Add(() => WoodenBuckler(level));
        shields.Add(() => StuddedWoodenBuckler(level));
        shields.Add(() => IronBuckler(level));
        shields.Add(() => SteelBuckler(level));
        shields.Add(() => SpikedBuckler(level));
        shields.Add(() => WoodenKnight(level));
        shields.Add(() => IronKnight(level));
        shields.Add(() => SteelKnight(level));
        shields.Add(() => RoyalKnight(level));
        shields.Add(() => SteelKite(level));
        shields.Add(() => HolyKite(level));
        shields.Add(() => Boar(level));
        shields.Add(() => SilverKite(level));
        shields.Add(() => GoldKite(level));
        shields.Add(() => Cyber(level));
        shields.Add(() => Winged(level));
        shields.Add(() => Marble(level));
        shields.Add(() => Hero(level));
        shields.Add(() => SteelTower(level));

        Expression<Func<GameObject>> function = shields[UnityEngine.Random.Range(0, shields.Count)];
        return function.Compile().Invoke();
    }

}

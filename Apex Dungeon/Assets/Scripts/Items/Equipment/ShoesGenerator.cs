using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static Boots;

[CreateAssetMenu(fileName = "ShoesGenerator", menuName = "ScriptableObjects/Shoes Generator")]
public class ShoesGenerator : ScriptableObject
{
    public Sprite[] leatherBoots;
    public Sprite ironBoots;
    public Sprite steelBoots;
    public Sprite silverBoots;
    public Sprite goldBoots;
    public Sprite copperBoots;
    public Sprite bronzeBoots;
    public Sprite[] dyedClothBoots;
    public Sprite festiveBoots;
    public Sprite wingedBoots;
    public Sprite soldierBoots;
    public Sprite cyberBoots;
    public Sprite aristocratBoots;

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

    public GameObject CreateShoes(string name, string description, Sprite image, int level, BootType bt, int spriteIndex = 0)
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

        Boots item = new Boots(level, "boots", tier, image, name, description, hpBoost, attackDamage, critBoost, evadeBoost, bt, spriteIndex);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);

        equipment.tag = "Equipment";

        return equipment;
    }

    public GameObject LeatherBoots(int level)
    {
        int spriteIndex = UnityEngine.Random.Range(0, leatherBoots.Length);
        Sprite sprite = leatherBoots[spriteIndex];
        return CreateShoes("Leather Boots", "Descrption", sprite, level, BootType.Leather, spriteIndex);
    }
    public GameObject DyedClothBoots(int level)
    {
        int spriteIndex = UnityEngine.Random.Range(0, dyedClothBoots.Length);
        Sprite sprite = dyedClothBoots[spriteIndex];
        return CreateShoes("Dyed Cloth Boots", "", sprite, level, BootType.DyedCloth, spriteIndex);
    }
    public GameObject IronBoots(int level)
    {
        return CreateShoes("Iron Boots", "", ironBoots, level, BootType.Iron);
    }
    public GameObject SteelBoots(int level)
    {
        return CreateShoes("STeel Boots", "", steelBoots, level, BootType.Steel);
    }
    public GameObject SilverBoots(int level)
    {
        return CreateShoes("Silver Boots", "", silverBoots, level, BootType.Silver);
    }
    public GameObject GoldBoots(int level)
    {
        return CreateShoes("Gold Boots", "", goldBoots, level, BootType.Gold);
    }
    public GameObject CopperBoots(int level)
    {
        return CreateShoes("Copper Boots", "", copperBoots, level, BootType.Copper);
    }
    public GameObject BronzeBoots(int level)
    {
        return CreateShoes("Bronze Boots", "", bronzeBoots, level, BootType.Bronze);
    }
    public GameObject FestiveBoots(int level)
    {
        return CreateShoes("Festive Boots", "", festiveBoots, level, BootType.Festive);
    }
    public GameObject WingedBoots(int level)
    {
        return CreateShoes("Winged Boots", "", wingedBoots, level, BootType.Winged);
    }
    public GameObject SoldierBoots(int level)
    {
        return CreateShoes("Soldier Boots", "", soldierBoots, level, BootType.Soldier);
    }
    public GameObject CyberBoots(int level)
    {
        return CreateShoes("Cyber Boots", "", cyberBoots, level, BootType.Cyber);
    }
    public GameObject AristocratBoots(int level)
    {
        return CreateShoes("Aristocrat Boots", "", aristocratBoots, level, BootType.Aristocrat);
    }

    public GameObject CreateRandomBoots(int level)
    {
        List<Expression<Func<GameObject>>> boots = new List<Expression<Func<GameObject>>>();
        boots.Add(() => LeatherBoots(level));
        boots.Add(() => DyedClothBoots(level));
        boots.Add(() => IronBoots(level));
        boots.Add(() => SteelBoots(level));
        boots.Add(() => SilverBoots(level));
        boots.Add(() => GoldBoots(level));
        boots.Add(() => CopperBoots(level));
        boots.Add(() => BronzeBoots(level));
        boots.Add(() => FestiveBoots(level));
        boots.Add(() => WingedBoots(level));
        boots.Add(() => SoldierBoots(level));
        boots.Add(() => CyberBoots(level));
        boots.Add(() => AristocratBoots(level));

        Expression<Func<GameObject>> function = boots[UnityEngine.Random.Range(0, boots.Count)];
        return function.Compile().Invoke();
    }
}

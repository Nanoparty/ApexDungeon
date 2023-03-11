using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName = "GlovesGenerator", menuName = "ScriptableObjects/Gloves Generator")]
public class GlovesGenerator : ScriptableObject
{
    public Sprite leatherGloves;
    public Sprite studdedGloves;
    public Sprite ironGloves;
    public Sprite steelGloves;
    public Sprite silverGloves;
    public Sprite goldGloves;
    public Sprite bronzeGloves;
    public Sprite copperGloves;
    public Sprite[] dyedClothGloves;
    public Sprite[] dyedIronGloves;
    public Sprite cyberGloves;
    public Sprite soldierGloves;
    public Sprite adventurerGloves;
    public Sprite heroGloves;
    public Sprite aristocratGloves;
    public Sprite festiveGloves;

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

    public GameObject CreateGloves(string name, string description, Sprite image, int level)
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

        GameObject equipment = new GameObject("name");
        equipment.AddComponent<SpriteRenderer>();
        equipment.GetComponent<SpriteRenderer>().sprite = image;
        equipment.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        equipment.AddComponent<BoxCollider2D>();
        equipment.GetComponent<BoxCollider2D>().isTrigger = true;

        Equipment item = new Equipment(level, "gloves", tier, image, hpBoost, attackDamage, critBoost, evadeBoost);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);

        equipment.tag = "Equipment";

        return equipment;
    }

    public GameObject DyedClothGauntlets(int level)
    {
        Sprite sprite = dyedClothGloves[UnityEngine.Random.Range(0, dyedClothGloves.Length)];
        return CreateGloves("Dyed Cloth Gloves", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject DyedIronGauntlets(int level)
    {
        Sprite sprite = dyedIronGloves[UnityEngine.Random.Range(0, dyedIronGloves.Length)];
        return CreateGloves("Dyed Iron Gauntlets", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject LeatherGloves(int level)
    {
        return CreateGloves("Leather Gloves", "Standard issue wooden shield.", leatherGloves, level);
    }
    public GameObject StuddedLeatherGloves(int level)
    {
        return CreateGloves("Studded Leather Gloves", "Standard issue wooden shield.", studdedGloves, level);
    }
    public GameObject IronGauntlets(int level)
    {
        return CreateGloves("Iron Gauntlets", "Standard issue wooden shield.", ironGloves, level);
    }
    public GameObject SteelGauntlets(int level)
    {
        return CreateGloves("Steel Gauntlets", "Standard issue wooden shield.", steelGloves, level);
    }
    public GameObject SilverGauntlets(int level)
    {
        return CreateGloves("Silver Gauntlets", "Standard issue wooden shield.", silverGloves, level);
    }
    public GameObject GoldGauntlets(int level)
    {
        return CreateGloves("Gold Gauntlets", "Standard issue wooden shield.", goldGloves, level);
    }
    public GameObject BronzeGauntlets(int level)
    {
        return CreateGloves("Bronze Gauntlets", "Standard issue wooden shield.", bronzeGloves, level);
    }
    public GameObject CopperGauntlets(int level)
    {
        return CreateGloves("Copper Gauntlets", "Standard issue wooden shield.", copperGloves, level);
    }
    public GameObject CyberGauntlets(int level)
    {
        return CreateGloves("Cyber Gauntlets", "Standard issue wooden shield.", cyberGloves, level);
    }
    public GameObject SoldierGauntlets(int level)
    {
        return CreateGloves("Soldier Gauntlets", "Standard issue wooden shield.", soldierGloves, level);
    }
    public GameObject AdventurerGauntlets(int level)
    {
        return CreateGloves("Adventurer Gauntlets", "Standard issue wooden shield.", adventurerGloves, level);
    }
    public GameObject HeroGauntlets(int level)
    {
        return CreateGloves("Hero Gauntlets", "Standard issue wooden shield.", heroGloves, level);
    }
    public GameObject AristocratGloves(int level)
    {
        return CreateGloves("Aristocrat Gloves", "Standard issue wooden shield.", aristocratGloves, level);
    }

    public GameObject CreateRandomGloves(int level)
    {
        List<Expression<Func<GameObject>>> gloves = new List<Expression<Func<GameObject>>>();
        gloves.Add(() => DyedClothGauntlets(level));
        gloves.Add(() => DyedIronGauntlets(level));
        gloves.Add(() => LeatherGloves(level));
        gloves.Add(() => StuddedLeatherGloves(level));
        gloves.Add(() => IronGauntlets(level));
        gloves.Add(() => SteelGauntlets(level));
        gloves.Add(() => SilverGauntlets(level));
        gloves.Add(() => GoldGauntlets(level));
        gloves.Add(() => BronzeGauntlets(level));
        gloves.Add(() => CopperGauntlets(level));
        gloves.Add(() => CyberGauntlets(level));
        gloves.Add(() => SoldierGauntlets(level));
        gloves.Add(() => AdventurerGauntlets(level));
        gloves.Add(() => HeroGauntlets(level));
        gloves.Add(() => AristocratGloves(level));
        

        Expression<Func<GameObject>> function = gloves[UnityEngine.Random.Range(0, gloves.Count)];
        return function.Compile().Invoke();
    }
}

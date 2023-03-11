using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName = "HelmetGenerator", menuName = "ScriptableObjects/Helmet Generator")]
public class HelmetGenerator : ScriptableObject
{
    public Sprite[] leatherHelm;
    public Sprite[] ironHelm;
    public Sprite goldHelm;
    public Sprite bronzeHelm;
    public Sprite copperHelm;
    public Sprite[] dyedWizardHat;
    public Sprite merchantCap;
    public Sprite leatherCap;
    public Sprite monsterMask;
    public Sprite magicianHat;
    public Sprite darkWizardHat;
    public Sprite catEars;
    public Sprite bunnyEars;
    public Sprite roseHeadband;
    public Sprite crown;
    public Sprite arisocratCap;
    public Sprite fishingCap;
    public Sprite[] dyedIronHelm;
    public Sprite[] dyedClothHeadband;
    public Sprite[] dyedRibbon;

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

    public GameObject CreateHelmet(string name, string description, Sprite image, int level)
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

        Equipment item = new Equipment(level, "helmet", tier, image, hpBoost, attackDamage, critBoost, evadeBoost);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);

        equipment.tag = "Equipment";

        return equipment;
    }

    public GameObject LeatherHelm(int level)
    {
        Sprite sprite = leatherHelm[UnityEngine.Random.Range(0, leatherHelm.Length)];
        return CreateHelmet("Leather Helmet", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject IronHelm(int level)
    {
        Sprite sprite = ironHelm[UnityEngine.Random.Range(0, ironHelm.Length)];
        return CreateHelmet("Iron Helmet", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject GoldHelm(int level)
    {
        return CreateHelmet("Gold Helmet", "Standard issue wooden shield.", goldHelm, level);
    }
    public GameObject BronzeHelm(int level)
    {
        return CreateHelmet("Bronze Helmet", "Standard issue wooden shield.", bronzeHelm, level);
    }
    public GameObject CopperHelm(int level)
    {
        return CreateHelmet("Copper Helmet", "Standard issue wooden shield.", copperHelm, level);
    }
    public GameObject DyedWizardHat(int level)
    {
        Sprite sprite = dyedWizardHat[UnityEngine.Random.Range(0, dyedWizardHat.Length)];
        return CreateHelmet("Dyed Wizard Hat", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject MerchantCap(int level)
    {
        return CreateHelmet("Merchant Cap", "Standard issue wooden shield.", merchantCap, level);
    }
    public GameObject LeatherCap(int level)
    {
        return CreateHelmet("Leath Cap", "Standard issue wooden shield.", leatherCap, level);
    }
    public GameObject MonsterMask(int level)
    {
        return CreateHelmet("Monster Mask", "Standard issue wooden shield.", monsterMask, level);
    }
    public GameObject MagicianHat(int level)
    {
        return CreateHelmet("Magician Hat", "Standard issue wooden shield.", magicianHat, level);
    }
    public GameObject DarkWizardHat(int level)
    {
        return CreateHelmet("Dark Wizard Hat", "Standard issue wooden shield.", darkWizardHat, level);
    }
    public GameObject CatEars(int level)
    {
        return CreateHelmet("Cat Ears", "Standard issue wooden shield.", catEars, level);
    }
    public GameObject BunnyEars(int level)
    {
        return CreateHelmet("Bunny Ears", "Standard issue wooden shield.", bunnyEars, level);
    }
    public GameObject RoseHeadband(int level)
    {
        return CreateHelmet("Rose Headband", "Standard issue wooden shield.", roseHeadband, level);
    }
    public GameObject Crown(int level)
    {
        return CreateHelmet("Crown", "Standard issue wooden shield.", crown, level);
    }
    public GameObject AristocratCap(int level)
    {
        return CreateHelmet("Aristocrat Cap", "Standard issue wooden shield.", arisocratCap, level);
    }
    public GameObject FishingCap(int level)
    {
        return CreateHelmet("Fishing Cap", "Standard issue wooden shield.", fishingCap, level);
    }
    public GameObject DyedClothHeadband(int level)
    {
        Sprite sprite = dyedClothHeadband[UnityEngine.Random.Range(0, dyedClothHeadband.Length)];
        return CreateHelmet("Dyed Cloth Headband", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject DyedIronHelm(int level)
    {
        Sprite sprite = dyedIronHelm[UnityEngine.Random.Range(0, dyedIronHelm.Length)];
        return CreateHelmet("Dyed Iron Helm", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject DyedRibbon(int level)
    {
        Sprite sprite = dyedRibbon[UnityEngine.Random.Range(0, dyedRibbon.Length)];
        return CreateHelmet("Dyed Ribbon", "Standard issue wooden shield.", sprite, level);
    }

    public GameObject CreateRandomHelmet(int level)
    {
        List<Expression<Func<GameObject>>> helmets = new List<Expression<Func<GameObject>>>();
        helmets.Add(() => LeatherHelm(level));
        helmets.Add(() => IronHelm(level));
        helmets.Add(() => GoldHelm(level));
        helmets.Add(() => BronzeHelm(level));
        helmets.Add(() => CopperHelm(level));
        helmets.Add(() => DyedWizardHat(level));
        helmets.Add(() => MerchantCap(level));
        helmets.Add(() => LeatherCap(level));
        helmets.Add(() => MonsterMask(level));
        helmets.Add(() => MagicianHat(level));
        helmets.Add(() => DarkWizardHat(level));
        helmets.Add(() => CatEars(level));
        helmets.Add(() => BunnyEars(level));
        helmets.Add(() => RoseHeadband(level));
        helmets.Add(() => Crown(level));
        helmets.Add(() => AristocratCap(level));
        helmets.Add(() => FishingCap(level));
        helmets.Add(() => DyedClothHeadband(level));
        helmets.Add(() => DyedIronHelm(level));
        helmets.Add(() => DyedRibbon(level));

        Expression<Func<GameObject>> function = helmets[UnityEngine.Random.Range(0, helmets.Count)];
        return function.Compile().Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static Equipment;
using static Helmet;

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
        //5% legendary
        //10% unique
        //25% rare
        //70% common
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

    public GameObject CreateHelmet(string name, string description, Sprite image, int level, HelmetType ht, int spriteIndex = 0)
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

        Helmet item = new Helmet(level, "helmet", tier, image, name, description, hpBoost, attackDamage, critBoost, evadeBoost, ht, spriteIndex);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);

        equipment.tag = "Equipment";

        return equipment;
    }

    public GameObject LeatherHelm(int level)
    {
        int spriteIndex = UnityEngine.Random.Range(0, leatherHelm.Length);
        Sprite sprite = leatherHelm[spriteIndex];
        return CreateHelmet("Leather Helmet", "", sprite, level, HelmetType.LeatherHelmet, spriteIndex);
    }
    public GameObject IronHelm(int level)
    {

        int spriteIndex = UnityEngine.Random.Range(0, ironHelm.Length);
        Sprite sprite = ironHelm[spriteIndex];
        return CreateHelmet("Iron Helmet", "", sprite, level, HelmetType.IronHelmet);
    }
    public GameObject GoldHelm(int level)
    {
        return CreateHelmet("Gold Helmet", "", goldHelm, level, HelmetType.GoldHelmet);
    }
    public GameObject BronzeHelm(int level)
    {
        return CreateHelmet("Bronze Helmet", "", bronzeHelm, level, HelmetType.BronzeHelmet);
    }
    public GameObject CopperHelm(int level)
    {
        return CreateHelmet("Copper Helmet", "", copperHelm, level, HelmetType.CopperHelmet);
    }
    public GameObject DyedWizardHat(int level)
    {
        int spriteIndex = UnityEngine.Random.Range(0, dyedWizardHat.Length);
        Sprite sprite = dyedWizardHat[spriteIndex];
        return CreateHelmet("Dyed Wizard Hat", "", sprite, level, HelmetType.DyedWizardHat);
    }
    public GameObject MerchantCap(int level)
    {
        return CreateHelmet("Merchant Cap", "", merchantCap, level, HelmetType.MerchantCap);
    }
    public GameObject LeatherCap(int level)
    {
        return CreateHelmet("Leather Cap", "", leatherCap, level, HelmetType.LeatherCap);
    }
    public GameObject MonsterMask(int level)
    {
        return CreateHelmet("Monster Mask", "", monsterMask, level, HelmetType.MonsterMask);
    }
    public GameObject MagicianHat(int level)
    {
        return CreateHelmet("Magician Hat", "", magicianHat, level, HelmetType.MagicianHat);
    }
    public GameObject DarkWizardHat(int level)
    {
        return CreateHelmet("Dark Wizard Hat", "", darkWizardHat, level, HelmetType.DarkWizardHat);
    }
    public GameObject CatEars(int level)
    {
        return CreateHelmet("Cat Ears", "", catEars, level, HelmetType.CatEars);
    }
    public GameObject BunnyEars(int level)
    {
        return CreateHelmet("Bunny Ears", "", bunnyEars, level, HelmetType.BunnyEars);
    }
    public GameObject RoseHeadband(int level)
    {
        return CreateHelmet("Rose Headband", "", roseHeadband, level, HelmetType.RoseHeadband);
    }
    public GameObject Crown(int level)
    {
        return CreateHelmet("Crown", "", crown, level, HelmetType.Crown);
    }
    public GameObject AristocratCap(int level)
    {
        return CreateHelmet("Aristocrat Cap", "", arisocratCap, level, HelmetType.AristocratCap);
    }
    public GameObject FishingCap(int level)
    {
        return CreateHelmet("Fishing Cap", "", fishingCap, level, HelmetType.FishingCap);
    }
    public GameObject DyedClothHeadband(int level)
    {
        int spriteIndex = UnityEngine.Random.Range(0, dyedClothHeadband.Length);
        Sprite sprite = dyedClothHeadband[spriteIndex];
        return CreateHelmet("Dyed Cloth Headband", "", sprite, level, HelmetType.DyedClothHeadband);
    }
    public GameObject DyedIronHelm(int level)
    {
        int spriteIndex = UnityEngine.Random.Range(0, dyedIronHelm.Length);
        Sprite sprite = dyedIronHelm[spriteIndex];
        return CreateHelmet("Dyed Iron Helm", "", sprite, level, HelmetType.DyedIronHelmet);
    }
    public GameObject DyedRibbon(int level)
    {
        int spriteIndex = UnityEngine.Random.Range(0, dyedRibbon.Length);
        Sprite sprite = dyedRibbon[spriteIndex];
        return CreateHelmet("Dyed Ribbon", "", sprite, level, HelmetType.DyedRibbon);
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

        return DyedRibbon(level);

        Expression<Func<GameObject>> function = helmets[UnityEngine.Random.Range(0, helmets.Count)];
        return function.Compile().Invoke();
    }
}

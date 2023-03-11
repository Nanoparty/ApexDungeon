using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestGenerator", menuName = "ScriptableObjects/Chest Generator")]
public class ChestGenerator : ScriptableObject
{
    public Sprite clothTunic;
    public Sprite leatherTunic;
    public Sprite clothRobe;
    public Sprite leatherRobe;
    public Sprite leatherJacket;
    public Sprite leatherBreastplate;
    public Sprite ironBreastplate;
    public Sprite steelCuirass;
    public Sprite silverCuirass;
    public Sprite goldCuirass;
    public Sprite brassCuirass;
    public Sprite copperCuirass;
    public Sprite ironProtector;
    public Sprite scholarRobe;
    public Sprite merchantRobe;
    public Sprite fighterRobe;
    public Sprite adventurerTunic;
    public Sprite soldierTunic;
    public Sprite knightTunic;
    public Sprite commanderCuirass;
    public Sprite heavySteelBreastplate;
    public Sprite oricalcumCuirass;
    public Sprite darkKnightCuirass;
    public Sprite heroCuirass;
    public Sprite[] dyedCape;
    public Sprite[] dyedJacket;
    public Sprite[] dyedBreastplate;
    public Sprite[] dyedRobe;
    public Sprite[] dyedTunic;

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

    public GameObject CreateChest(string name, string description, Sprite image, int level)
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

        Equipment item = new Equipment(level, "chestplate", tier, image, hpBoost, attackDamage, critBoost, evadeBoost);

        equipment.AddComponent<Pickup>();
        equipment.GetComponent<Pickup>().SetItem(item);

        equipment.tag = "Equipment";

        return equipment;
    }

    public GameObject DyedCape(int level)
    {
        Sprite sprite = dyedCape[UnityEngine.Random.Range(0, dyedCape.Length)];
        return CreateChest("Dyed Cape", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject DyedJacket(int level)
    {
        Sprite sprite = dyedJacket[UnityEngine.Random.Range(0, dyedJacket.Length)];
        return CreateChest("Dyed Iron Gauntlets", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject DyedBreastplate(int level)
    {
        Sprite sprite = dyedBreastplate[UnityEngine.Random.Range(0, dyedBreastplate.Length)];
        return CreateChest("Dyed Iron Gauntlets", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject DyedRobe(int level)
    {
        Sprite sprite = dyedRobe[UnityEngine.Random.Range(0, dyedRobe.Length)];
        return CreateChest("Dyed Iron Gauntlets", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject DyedTunic(int level)
    {
        Sprite sprite = dyedTunic[UnityEngine.Random.Range(0, dyedTunic.Length)];
        return CreateChest("Dyed Iron Gauntlets", "Standard issue wooden shield.", sprite, level);
    }
    public GameObject ClothTunic(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", clothTunic, level);
    }
    public GameObject LeatherTunic(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", leatherTunic, level);
    }
    public GameObject ClothRobe(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", clothRobe, level);
    }
    public GameObject LeatherRobe(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", leatherRobe, level);
    }
    public GameObject LeatherJacket(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", leatherJacket, level);
    }
    public GameObject LeatherBreastplate(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", leatherBreastplate, level);
    }
    public GameObject IronBreastplate(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", ironBreastplate, level);
    }
    public GameObject SteelCuirass(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", steelCuirass, level);
    }
    public GameObject SilverCuirass(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", silverCuirass, level);
    }
    public GameObject GoldCuirass(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", goldCuirass, level);
    }
    public GameObject BronzeCuirass(int level)
    {
        return CreateChest("Leather Gloves", "Standard issue wooden shield.", brassCuirass, level);
    }

    public GameObject CreateRandomChest(int level)
    {
        List<Expression<Func<GameObject>>> chestplates = new List<Expression<Func<GameObject>>>();
        chestplates.Add(() => DyedCape(level));
        chestplates.Add(() => DyedJacket(level));
        chestplates.Add(() => DyedBreastplate(level));
        chestplates.Add(() => DyedRobe(level));
        chestplates.Add(() => DyedTunic(level));
        chestplates.Add(() => ClothTunic(level));
        chestplates.Add(() => LeatherTunic(level));
        chestplates.Add(() => ClothRobe(level));
        chestplates.Add(() => LeatherRobe(level));
        chestplates.Add(() => LeatherJacket(level));
        chestplates.Add(() => LeatherBreastplate(level));
        chestplates.Add(() => IronBreastplate(level));
        chestplates.Add(() => SteelCuirass(level));
        chestplates.Add(() => SilverCuirass(level));
        chestplates.Add(() => GoldCuirass(level));
        chestplates.Add(() => BronzeCuirass(level));

        Expression<Func<GameObject>> function = chestplates[UnityEngine.Random.Range(0, chestplates.Count)];
        return function.Compile().Invoke();
    }
}

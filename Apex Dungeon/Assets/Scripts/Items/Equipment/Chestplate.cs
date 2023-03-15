using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helmet;

public class Chestplate : Equipment
{
    public ChestplateType chestplateType;

    public enum ChestplateType
    {
        ClothTunic,
        LeatherTunic,
        ClothRobe,
        LeatherRobe,
        LeatherJacket,
        LeatherBreastplate,
        IronBreastplate,
        SteelCuirass,
        SilverCuirass,
        GoldCuirass,
        BrassCuirass,
        CopperCuirass,
        IronProtector,
        ScholarRobe,
        MerchantRobe,
        FighterRobe,
        AdventurerTunic,
        SoldierTunic,
        KnightTunic,
        CommanderCuirass,
        HeavySteelBreastplate,
        OricalcumCuirass,
        DarkKnightCuirass,
        HeroCuirass,
        DyedCape,
        DyedJacket,
        DyedBreastplate,
        DyedRobe,
        DyedTunic,
    }

    public Chestplate(int lvl, string type, int tier, Sprite image, string name, string description, int hpBoost, int attackBoost, int critBoost, int evadeBoost, ChestplateType ct, int si)
        : base(lvl, type, tier, image, name, description, hpBoost, attackBoost, critBoost, evadeBoost)
    {
        this.chestplateType = ct;
        this.spriteIndex = si;
        this.image = GetImage();
        etype = EquipType.CHESTPLATE;
    }

    public Chestplate(SaveGear gear)
    {
        attack = gear.attack;
        defense = gear.hpBoost;
        type = gear.type;
        tier = gear.tier;
        level = gear.itemLevel;
        itemName = gear.itemName;
        description = gear.description;
        flavorText = gear.rank;
        spriteIndex = gear.spriteIndex;
        etype = Enum.Parse<EquipType>(gear.equipType);
        chestplateType = Enum.Parse<ChestplateType>(gear.subType);
        image = GetImage();
    }

    public override Sprite GetImage()
    {
        ChestGenerator cg = Resources.Load<ChestGenerator>("ScriptableObjects/ChestplateGenerator");

        if (chestplateType == ChestplateType.ClothTunic) return cg.clothTunic;
        if (chestplateType == ChestplateType.LeatherTunic) return cg.leatherTunic;
        if (chestplateType == ChestplateType.ClothRobe) return cg.clothRobe;
        if (chestplateType == ChestplateType.LeatherRobe) return cg.leatherRobe;
        if (chestplateType == ChestplateType.LeatherJacket) return cg.leatherJacket;
        if (chestplateType == ChestplateType.LeatherBreastplate) return cg.leatherBreastplate;
        if (chestplateType == ChestplateType.IronBreastplate) return cg.ironBreastplate;
        if (chestplateType == ChestplateType.SteelCuirass) return cg.steelCuirass;
        if (chestplateType == ChestplateType.SilverCuirass) return cg.silverCuirass;
        if (chestplateType == ChestplateType.GoldCuirass) return cg.goldCuirass;
        if (chestplateType == ChestplateType.BrassCuirass) return cg.brassCuirass;
        if (chestplateType == ChestplateType.CopperCuirass) return cg.copperCuirass;
        if (chestplateType == ChestplateType.IronProtector) return cg.ironProtector;
        if (chestplateType == ChestplateType.ScholarRobe) return cg.scholarRobe;
        if (chestplateType == ChestplateType.MerchantRobe) return cg.merchantRobe;
        if (chestplateType == ChestplateType.FighterRobe) return cg.fighterRobe;
        if (chestplateType == ChestplateType.AdventurerTunic) return cg.adventurerTunic;
        if (chestplateType == ChestplateType.SoldierTunic) return cg.soldierTunic;
        if (chestplateType == ChestplateType.KnightTunic) return cg.knightTunic;
        if (chestplateType == ChestplateType.CommanderCuirass) return cg.commanderCuirass;
        if (chestplateType == ChestplateType.HeavySteelBreastplate) return cg.heavySteelBreastplate;
        if (chestplateType == ChestplateType.OricalcumCuirass) return cg.oricalcumCuirass;
        if (chestplateType == ChestplateType.DarkKnightCuirass) return cg.darkKnightCuirass;
        if (chestplateType == ChestplateType.HeroCuirass) return cg.heroCuirass;
        if (chestplateType == ChestplateType.DyedCape) return cg.dyedCape[spriteIndex];
        if (chestplateType == ChestplateType.DyedJacket) return cg.dyedJacket[spriteIndex];
        if (chestplateType == ChestplateType.DyedBreastplate) return cg.dyedBreastplate[spriteIndex];
        if (chestplateType == ChestplateType.DyedRobe) return cg.dyedRobe[spriteIndex];
        if (chestplateType == ChestplateType.DyedTunic) return cg.dyedTunic[spriteIndex];

        return null;
    }
}


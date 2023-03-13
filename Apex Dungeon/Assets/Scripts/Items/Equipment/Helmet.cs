using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Equipment
{
    public HelmetType helmetType;
    public enum HelmetType
    {
        LeatherHelmet,
        IronHelmet,
        GoldHelmet,
        BronzeHelmet,
        CopperHelmet,
        DyedWizardHat,
        MerchantCap,
        LeatherCap,
        MonsterMask,
        MagicianHat,
        DarkWizardHat,
        CatEars,
        BunnyEars,
        RoseHeadband,
        Crown,
        AristocratCap,
        FishingCap,
        DyedIronHelmet,
        DyedClothHeadband,
        DyedRibbon,
    }

    public Helmet(int lvl, string type, int tier, Sprite image, string name, string description, int hpBoost, int attackBoost, int critBoost, int evadeBoost, HelmetType ht, int si) 
        : base(lvl, type, tier, image, name, description, hpBoost, attackBoost, critBoost, evadeBoost)
    {
        this.helmetType = ht;
        this.spriteIndex = si;
        image = GetImage();
    }

    public Helmet(SaveGear gear)
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
        helmetType = Enum.Parse<HelmetType>(gear.subType);
        image = GetImage();
    }

    public override Sprite GetImage()
    {
        HelmetGenerator hg = GameManager.gmInstance.equipmentGenerator.helmetGenerator;
        if (helmetType == HelmetType.LeatherHelmet) return hg.leatherHelm[spriteIndex];
        if (helmetType == HelmetType.IronHelmet) return hg.ironHelm[spriteIndex];
        if (helmetType == HelmetType.GoldHelmet) return hg.goldHelm;
        if (helmetType == HelmetType.BronzeHelmet) return hg.bronzeHelm;
        if (helmetType == HelmetType.CopperHelmet) return hg.copperHelm;
        if (helmetType == HelmetType.DyedWizardHat) return hg.dyedWizardHat[spriteIndex];
        if (helmetType == HelmetType.MerchantCap) return hg.merchantCap;
        if (helmetType == HelmetType.LeatherCap) return hg.leatherCap;
        if (helmetType == HelmetType.MonsterMask) return hg.monsterMask;
        if (helmetType == HelmetType.MagicianHat) return hg.magicianHat;
        if (helmetType == HelmetType.DarkWizardHat) return hg.darkWizardHat;
        if (helmetType == HelmetType.CatEars) return hg.catEars;
        if (helmetType == HelmetType.BunnyEars) return hg.bunnyEars;
        if (helmetType == HelmetType.RoseHeadband) return hg.roseHeadband;
        if (helmetType == HelmetType.Crown) return hg.crown;
        if (helmetType == HelmetType.AristocratCap) return hg.arisocratCap;
        if (helmetType == HelmetType.FishingCap) return hg.fishingCap;
        if (helmetType == HelmetType.DyedIronHelmet) return hg.dyedIronHelm[spriteIndex];
        if (helmetType == HelmetType.DyedClothHeadband) return hg.dyedClothHeadband[spriteIndex];
        if (helmetType == HelmetType.DyedRibbon) return hg.dyedRibbon[spriteIndex];

        return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chestplate;
using static Gloves;

public class Boots : Equipment
{
    public BootType bootType;

    public enum BootType
    {
        Leather,
        Iron,
        Steel,
        Silver,
        Gold,
        Bronze,
        Copper,
        DyedCloth,
        Festive,
        Winged,
        Soldier,
        Cyber,
        Aristocrat
    }

    public Boots(int lvl, string type, int tier, Sprite image, string name, string description, int hpBoost, int attackBoost, int critBoost, int evadeBoost, BootType bt, int si)
        : base(lvl, type, tier, image, name, description, hpBoost, attackBoost, critBoost, evadeBoost)
    {
        this.bootType = bt;
        this.spriteIndex = si;
        this.image = GetImage();
    }

    public Boots(SaveGear gear)
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
        bootType = Enum.Parse<BootType>(gear.subType);
        image = GetImage();
    }

    public override Sprite GetImage()
    {
        ShoesGenerator bg = GameManager.gmInstance.equipmentGenerator.bootsGenerator;

        if (bootType == BootType.Leather) return bg.leatherBoots[spriteIndex];
        if (bootType == BootType.DyedCloth) return bg.dyedClothBoots[spriteIndex];
        if (bootType == BootType.Iron) return bg.ironBoots;
        if (bootType == BootType.Steel) return bg.steelBoots;
        if (bootType == BootType.Silver) return bg.silverBoots;
        if (bootType == BootType.Gold) return bg.goldBoots;
        if (bootType == BootType.Copper) return bg.copperBoots;
        if (bootType == BootType.Bronze) return bg.bronzeBoots;
        if (bootType == BootType.Festive) return bg.festiveBoots;
        if (bootType == BootType.Winged) return bg.wingedBoots;
        if (bootType == BootType.Soldier) return bg.soldierBoots;
        if (bootType == BootType.Cyber) return bg.cyberBoots;
        if (bootType == BootType.Aristocrat) return bg.aristocratBoots;

        return null;
    }
}

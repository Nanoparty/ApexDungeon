using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chestplate;

public class Gloves : Equipment
{
    public GloveType gloveType;

    public enum GloveType
    {
        Leather,
        Studded,
        Iron,
        Steel,
        Silver,
        Gold,
        Bronze,
        Copper,
        DyedCloth,
        DyedIron,
        Cyber,
        Soldier,
        Adventurer,
        Hero,
        Aristocrat,
        Festive,
    }

    public Gloves(int lvl, string type, int tier, Sprite image, string name, string description, int hpBoost, int attackBoost, int critBoost, int evadeBoost, GloveType gt, int si)
        : base(lvl, type, tier, image, name, description, hpBoost, attackBoost, critBoost, evadeBoost)
    {
        this.gloveType = gt;
        this.spriteIndex = si;
        this.image = GetImage();
    }

    public override Sprite GetImage()
    {
        GlovesGenerator gg = GameManager.gmInstance.equipmentGenerator.glovesGenerator;

        if (gloveType == GloveType.Leather) return gg.leatherGloves;
        if (gloveType == GloveType.Studded) return gg.studdedGloves;
        if (gloveType == GloveType.Iron) return gg.ironGloves;
        if (gloveType == GloveType.Steel) return gg.steelGloves;
        if (gloveType == GloveType.Silver) return gg.silverGloves;
        if (gloveType == GloveType.Gold) return gg.goldGloves;
        if (gloveType == GloveType.Bronze) return gg.bronzeGloves;
        if (gloveType == GloveType.Copper) return gg.copperGloves;
        if (gloveType == GloveType.DyedCloth) return gg.dyedClothGloves[spriteIndex];
        if (gloveType == GloveType.DyedIron) return gg.dyedIronGloves[spriteIndex];
        if (gloveType == GloveType.Cyber) return gg.cyberGloves;
        if (gloveType == GloveType.Soldier) return gg.soldierGloves;
        if (gloveType == GloveType.Adventurer) return gg.adventurerGloves;
        if (gloveType == GloveType.Hero) return gg.heroGloves;
        if (gloveType == GloveType.Aristocrat) return gg.aristocratGloves;
        if (gloveType == GloveType.Festive) return gg.festiveGloves;

        return null;
    }
}

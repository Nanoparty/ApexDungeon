using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helmet;

public class Shield : Equipment
{
    public ShieldType shieldType;

    public enum ShieldType
    {
        WoodenBuckler,
        StuddedBuckler,
        IronBuckler,
        SteelBuckler,
        SpikedBuckler,
        Tower,
        WoodenKnight,
        IronKnight,
        SteelKnight,
        RoyalKnight,
        SteelKite,
        HolyKite,
        PorkKite,
        SilverKite,
        GoldKite,
        Cyber,
        Winged,
        Marble,
        Hero
    }

    public Shield(int lvl, string type, int tier, Sprite image, string name, string description, int hpBoost, int attackBoost, int critBoost, int evadeBoost, ShieldType st, int si)
        : base(lvl, type, tier, image, name, description, hpBoost, attackBoost, critBoost, evadeBoost)
    {
        this.shieldType = st;
        this.spriteIndex = si;
        image = GetImage();
    }

    public override Sprite GetImage()
    {
        ShieldGenerator sg = GameManager.gmInstance.equipmentGenerator.shieldGenerator;

        if (shieldType == ShieldType.WoodenBuckler) return sg.woodenBuckler;
        if (shieldType == ShieldType.StuddedBuckler) return sg.studdedBuckler;
        if (shieldType == ShieldType.IronBuckler) return sg.ironBuckler;
        if (shieldType == ShieldType.SteelBuckler) return sg.steelBuckler;
        if (shieldType == ShieldType.SpikedBuckler) return sg.spikedBuckler;
        if (shieldType == ShieldType.Tower) return sg.towerShield;
        if (shieldType == ShieldType.WoodenKnight) return sg.woodenKnight;
        if (shieldType == ShieldType.IronKnight) return sg.ironKnight;
        if (shieldType == ShieldType.SteelKnight) return sg.steelKnight;
        if (shieldType == ShieldType.RoyalKnight) return sg.royalKnight;
        if (shieldType == ShieldType.SteelKite) return sg.steelKite;
        if (shieldType == ShieldType.HolyKite) return sg.holyKite;
        if (shieldType == ShieldType.SilverKite) return sg.silverKite;
        if (shieldType == ShieldType.GoldKite) return sg.goldKite;
        if (shieldType == ShieldType.PorkKite) return sg.porkKite;
        if (shieldType == ShieldType.Cyber) return sg.cyber;
        if (shieldType == ShieldType.Winged) return sg.winged;
        if (shieldType == ShieldType.Marble) return sg.marble;
        if (shieldType == ShieldType.Hero) return sg.target;

        return null;
    }
}

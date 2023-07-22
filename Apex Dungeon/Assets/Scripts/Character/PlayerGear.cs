using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerGear
{
    public Equipment Chestplate;
    public Equipment Helmet;
    public Equipment Legs;
    public Equipment Feet;
    public Equipment Weapon;
    public Equipment Secondary;
    public Equipment Necklace;
    public Equipment Ring;

    public PlayerGear(){
        Chestplate = null;
        Helmet = null;
        Legs = null;
        Feet = null;
        Weapon = null;
        Secondary = null;
        Necklace = null;
        Ring = null;
    }

    public PlayerGear(Equipment c, Equipment H, Equipment l, Equipment f, Equipment w, Equipment s, Equipment n, Equipment r)
    {
        Chestplate = c;
        Helmet = H;
        Legs = l;
        Feet = f;
        Weapon = w;
        Secondary = s;
        Necklace = n;
        Ring = r;
    }

    public PlayerGear(SavePlayer p, ImageLookup il)
    {
        Helmet = null;
        Chestplate = null;
        Legs = null;
        Feet = null;
        Weapon = null;
        Secondary = null;
        Necklace = null;
        Ring = null;

        if (!p.helmet.empty) Helmet = new Equipment(p.helmet, il);
        if (!p.chestplate.empty) Chestplate = new Equipment(p.chestplate, il);
        if (!p.legs.empty) Legs = new Equipment(p.legs, il);
        if (!p.feet.empty) Feet = new Equipment(p.feet, il);
        if (!p.weapon.empty) Weapon = new Equipment(p.weapon, il);
        if (!p.shield.empty) Secondary = new Equipment(p.shield, il);
        if (!p.necklace.empty) Necklace = new Equipment(p.necklace, il);
        if (!p.ring.empty) Ring = new Equipment(p.ring, il);


    }

    public void EquipGear(Equipment e, Player player)
    {
        if (e.type == "helmet")
        {
            Helmet = e;
        }
        if (e.type == "chestplate")
        {
            Chestplate = e;
        }
        if (e.type == "legs")
        {
            Legs = e;
        }
        if (e.type == "boots")
        {
            Feet = e;
        }
        if (e.type == "weapon")
        {
            Weapon = e;
            player.attackRange = e.range;
            Debug.Log("Range:" + e.range);
        }
        if (e.type == "shield")
        {
            Secondary = e;
        }
        if (e.type == "necklace")
        {
            Necklace = e;
        }
        if (e.type == "ring")
        {
            Ring = e;
        }

        int att = e.attack;
        int hp = e.defense;
        int crit = e.crit;
        int intel = e.intelligence;
        int evade = e.evade;

        player.AddAttack(att);
        player.AddBaseHP(hp);
        player.AddCrit(crit);
        player.AddIntelligence(intel);
        player.AddEvade(evade);
    }
    
}

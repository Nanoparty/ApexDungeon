using System.Collections;
using System.Collections.Generic;
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
    
}

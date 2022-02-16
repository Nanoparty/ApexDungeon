using UnityEngine;
using System.Collections;

public class Equipment : Item
{
    //inherited

    //Parameters
    public int level;
    public string type;
    public int tier;

    //stats

    public int defense;
    public int attack;
    public int intelligence;
    public int crit;

    public string modifier;

    public void setStats(int lvl, string type, int tier, Sprite img, int def, int atk, int intel, int crit, string mod)
    {
        this.level = lvl;
        this.type = type;
        this.tier = tier;
        this.defense = def;
        this.attack = def;
        this.intelligence = intel;
        this.crit = crit;
        this.modifier = mod;

        //setInfo(img, "name", "flavor", "details");
        this.img = img;
        this.name = "name";
        this.flavor = "flavor";
        this.details = "details";
    }

    public override void useItem()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {

    }

    public new string getName()
    {
        return name;
    }
}

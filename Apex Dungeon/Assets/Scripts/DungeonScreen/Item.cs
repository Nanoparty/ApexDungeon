using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public Sprite img;
    protected int hp;
    protected int mp;
    protected int defense;
    protected int damage;

    protected string type;
    protected string modifier;

    protected string name = "UNNAMED";
    protected string flavor = "Vanilla";
    protected string details = "Nothing";

    public Item()
    {

    }
    public Item(Sprite img)
    {
        this.img = img;
    }

    public void Update()
    {
        
    }

    public Sprite getImage()
    {
        return img;
    }

    public string getName()
    {
        return name;
    }
    public string getFlavor()
    {
        return flavor;
    }
    public string getDetails()
    {
        return details;
    }

    public abstract void useItem();
}

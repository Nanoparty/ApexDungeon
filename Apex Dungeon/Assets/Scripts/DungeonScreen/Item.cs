using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public Sprite img;

    protected string name = "UNNAMED";
    protected string flavor = "Vanilla";
    protected string details = "Nothing";

    public Item(Sprite img, string name, string flavor, string details)
    {
        this.img = img;
        this.name = name;
        this.flavor = flavor;
        this.details = details;
    }

    public Item(Sprite img)
    {
        this.img = img;
    }

    public Item()
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

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

    public Item()
    {

    }
    public Item(Sprite img)
    {
        this.img = img;
    }

    public Sprite getImage()
    {
        return img;
    }

    public abstract void useItem();
}

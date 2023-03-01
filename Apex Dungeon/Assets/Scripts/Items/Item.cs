﻿using UnityEngine;

public abstract class Item
{
    public string itemName;
    public string description;
    public string flavorText;
    public Sprite image;
    public int level;
    public int tier;
    public string id;
    
    public abstract void Create();

    public abstract void UseItem();

}

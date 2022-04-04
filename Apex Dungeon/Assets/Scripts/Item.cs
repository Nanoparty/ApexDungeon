using UnityEngine;

public abstract class Item
{
    public string itemName;
    public string description;
    public string flavorText;
    public Sprite image;
    
    public abstract void Create();

    public abstract void UseItem();

}

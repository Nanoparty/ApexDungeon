using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    public string flavorText;
    public Sprite image;

    public abstract void Create();

    public abstract void UseItem();

}
